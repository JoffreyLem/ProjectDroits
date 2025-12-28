using System.Text;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using ProjectDroit.Core.Interfaces.Infrastructure;
using ProjectDroit.Domain.Entities.Legifrance;
using ProjectDroit.Infrastructure.Http.Gemini.Configuration;
using ProjectDroit.Infrastructure.Http.Gemini.Dao.Request;
using ProjectDroit.Infrastructure.Http.Gemini.Dao.Response;

namespace ProjectDroit.Infrastructure.Http.Gemini.Repositories;

public class GeminiRepository(
    IHttpClientFactory httpClientFactory,
    ILogger<GeminiRepository> logger,
    IMapper mapper,
    IOptions<GeminiSettings> geminiSettings) : IGeminiRepository
{
    private readonly GeminiSettings _geminiSettings = geminiSettings.Value;
    private readonly HttpClient _httpClient = httpClientFactory.CreateClient("Gemini");

    private string Url => $"v1beta/models/{_geminiSettings.ModelId}:generateContent?key={_geminiSettings.ApiKey}";

    public async Task<List<string>> ExtractKeywordsAsync(string prompt)
    {
        var keywordExtractionPrompt =
            $"""
             À partir de cette question : '{prompt}', extrais uniquement les mots-clés importants sous forme d'une liste JSON.
             Chaque élément de la liste doit contenir un seul mot-clé, sans doublons.
             Réponds uniquement avec la liste, sans texte autour ni explication. Exemple de réponse attendue: ["motclé1", "avocat", "procédure"]
             """;

        var geminiRequest = new GeminiGenerationRequest
        {
            Contents = new List<GeminiContent>
            {
                new GeminiContent
                {
                    Parts = new List<GeminiPart> { new GeminiPart { Text = keywordExtractionPrompt } }
                }
            },
            GenerationConfig = new GenerationConfig()
            {
                ResponseMimeType = "application/json",
            }
        };

        try
        {
            var jsonRequest = JsonConvert.SerializeObject(geminiRequest, new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                NullValueHandling = NullValueHandling.Ignore
            });
            var content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(Url, content);

            if (!response.IsSuccessStatusCode)
            {
                var errorBody = await response.Content.ReadAsStringAsync();
                logger.LogError("Erreur de l'API Gemini ({StatusCode}): {ErrorBody}", response.StatusCode, errorBody);

                return new List<string>();
            }

            var jsonResponse = await response.Content.ReadAsStringAsync();
            var geminiResponse = JsonConvert.DeserializeObject<GeminiGenerationResponse>(jsonResponse);

            string? generatedText = geminiResponse?.Candidates?.FirstOrDefault()?.Content?.Parts?.FirstOrDefault()?.Text
                ?.Trim();

            if (string.IsNullOrWhiteSpace(generatedText))
            {
                logger.LogWarning(
                    "L'API Gemini n'a retourné aucun texte pour l'extraction de mots-clés. Prompt: {Prompt}", prompt);
                return new List<string>();
            }

            logger.LogDebug("Texte brut retourné par Gemini pour keywords: {GeneratedText}", generatedText);

            try
            {
                if (generatedText.StartsWith("```json"))
                {
                    generatedText = generatedText.Replace("```json", "").TrimStart();
                }

                if (generatedText.StartsWith("```"))
                {
                    generatedText = generatedText.Substring(3);
                }

                if (generatedText.EndsWith("```"))
                {
                    generatedText = generatedText.Substring(0, generatedText.Length - 3);
                }

                generatedText = generatedText.Trim();


                List<string>? keywords = JsonConvert.DeserializeObject<List<string>>(generatedText);
                return keywords ?? new List<string>();
            }
            catch (JsonReaderException jsonEx)
            {
                logger.LogError(jsonEx,
                    "Impossible de parser la réponse de Gemini comme une liste JSON. Réponse brute: {GeneratedText}",
                    generatedText);
                return new List<string>();
            }
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Erreur inattendue lors de l'extraction de mots-clés via Gemini.");
            return new List<string>();
        }
    }

    public async Task<string?> GetResponse(string prompt, List<SearchResult> searchResults)
    {
        var jsonPayload = JsonConvert.SerializeObject(searchResults);


var promptToAsk =
    $"""
    CONTEXTE : Tu es un assistant virtuel serviable. Un utilisateur a posé une question et tu as récupéré des informations potentiellement pertinentes issues de sources juridiques officielles (via une API).

    QUESTION UTILISATEUR :
    '{prompt}'

    INFORMATIONS RÉCUPÉRÉES (Format JSON - contient typiquement des éléments avec 'TextId', 'Origin', et potentiellement une liste ('Sections' ou 'extracts') contenant des items avec 'Num', 'Type') :
    '{jsonPayload}'

    TA MISSION :
    1.  Analyse attentivement la QUESTION UTILISATEUR et les INFORMATIONS RÉCUPÉRÉES.
    2.  Rédige une réponse synthétique et claire à la question en te basant **exclusivement** sur les informations fournies dans le JSON (`INFORMATIONS RÉCUPÉRÉES`). Ne cherche pas d'informations externes.
    3.  **Exigence de Citation Détaillée :** Pour chaque élément d'information spécifique que tu utilises dans ta réponse et qui provient directement des `INFORMATIONS RÉCUPÉRÉES`, tu **dois impérativement identifier la source précise** dans le JSON et ajouter une citation à la fin de la phrase ou de l'élément concerné.
        * Efforce-toi d'utiliser le format suivant : `[Source : (Origin) - (ID ou Numéro)]`.
            * Remplace `(Origin)` par la valeur du champ `Origin` (ex: CODE, LEGI, KALI, JORF).
            * Remplace `(ID ou Numéro)` par la valeur du champ `TextId` si l'information est générale au texte principal, OU par la valeur du champ `Num` (ex: L1234-9) si l'information provient d'un article ou d'une section spécifique listé(e) dans le JSON. Si les deux sont disponibles pour l'extrait spécifique, privilégie le `Num`.
        * Si, pour une information tirée du JSON, tu ne parviens pas à déterminer l'`Origin` et l'`ID ou Numéro` spécifiques, utilise une citation générique : `[Source : JSON fourni]`.
        * L'objectif est de lier clairement chaque information à son origine DANS les données fournies.
    4.  Adopte un ton **conversationnel, neutre et précis**. Adresse-toi directement à l'utilisateur (utilise "vous" ou "tu", sois cohérent). Évite le jargon juridique excessif si possible, mais reste précis.
    5.  **Gestion de l'Incapacité à Répondre :**
        * Si les `INFORMATIONS RÉCUPÉRÉES` sont vides ou sans information pertinente : Explique poliment (ex: "Ma recherche dans les textes officiels n'a pas permis de trouver d'information directe sur ce point. Pourriez-vous essayer de reformuler votre question ?"). Pas de citation.
        * Si les informations sont pertinentes mais **insuffisantes ou ambiguës** : Explique ce qui manque ou est ambigu, en **citant la source de l'information partielle mentionnée** en utilisant le format détaillé ci-dessus (ex: "Le texte trouvé [Source : CODE - L1234-9] mentionne X, mais ne précise pas Y..."). Propose de reformuler ou de consulter un professionnel.
    6.  **Format de la Réponse :** Ne fournis **que** la réponse synthétique (avec citations détaillées) ou l'explication de l'incapacité à répondre. N'ajoute **aucune** phrase d'introduction ou de conclusion génériques. N'invente **jamais** d'informations.
    """;

        var geminiRequest = new GeminiGenerationRequest
        {
            Contents = new List<GeminiContent>
            {
                new GeminiContent
                {
                    Parts = new List<GeminiPart> { new GeminiPart { Text = promptToAsk } }
                }
            },
            GenerationConfig = new GenerationConfig()
            {
                ResponseMimeType = "text/plain",
            }
        };

        try
        {
            var jsonRequest = JsonConvert.SerializeObject(geminiRequest, new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                NullValueHandling = NullValueHandling.Ignore
            });
            var content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(Url, content);

            if (!response.IsSuccessStatusCode)
            {
                var errorBody = await response.Content.ReadAsStringAsync();
                logger.LogError("Erreur de l'API Gemini ({StatusCode}): {ErrorBody}", response.StatusCode, errorBody);

                return string.Empty;
            }

            var jsonResponse = await response.Content.ReadAsStringAsync();
            var geminiResponse = JsonConvert.DeserializeObject<GeminiGenerationResponse>(jsonResponse);

            string? generatedText = geminiResponse?.Candidates?.FirstOrDefault()?.Content?.Parts?.FirstOrDefault()?.Text
                ?.Trim();

            if (string.IsNullOrWhiteSpace(generatedText))
            {
                logger.LogWarning(
                    "L'API Gemini n'a retourné aucun texte pour l'extraction de mots-clés. Prompt: {Prompt}", prompt);
                return string.Empty;
            }

            logger.LogDebug("Texte brut retourné par Gemini pour keywords: {GeneratedText}", generatedText);

            return generatedText;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Erreur inattendue lors de l'extraction de mots-clés via Gemini.");
            return string.Empty;
        }
    }
}