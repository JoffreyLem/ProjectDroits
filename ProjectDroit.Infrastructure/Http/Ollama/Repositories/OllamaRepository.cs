using System.Globalization;
using System.Text;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using ProjectDroit.Core.Interfaces;
using ProjectDroit.Core.Interfaces.Infrastructure;
using ProjectDroit.Domain.Entities.Legifrance;
using ProjectDroit.Domain.Entities.LLM;
using ProjectDroit.Infrastructure.Http.Ollama.Daos;

namespace ProjectDroit.Infrastructure.Http.Ollama.Repositories;

public class OllamaRepository(IHttpClientFactory httpClientFactory, ILogger<OllamaRepository> logger, IMapper mapper) : IOllamaRepository
{
    private readonly HttpClient _httpClient = httpClientFactory.CreateClient("Ollama");
    
    private async Task<string?> GenerateTextAsync(OllamaInfraRequestDao request)
    {
        try
        {
            request.Prompt = CleanText(request.Prompt);

            var jsonPayload = JsonConvert.SerializeObject(request);
            var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("api/generate", content);
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadAsStringAsync();
            return result;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Erreur lors de l'appel à Ollama.");
            return null;
        }
    }

    public async Task<string?> GetResponse(string prompt, List<SearchResult> searchResults)
    {
        var jsonPayload = JsonConvert.SerializeObject(searchResults);
        var promptToAsk =
            $"""
             À partir de cette question : '{prompt}', et de ces résultats : '{jsonPayload}'";" 
             Synthétise moi une réponse.
             """;
        
        var request = new OllamaInfraRequestDao
        {
            Prompt = promptToAsk,
            Options = new OllamaInfraOptionsDao
            {
                Temperature = 0.4f,
                TopP = 0.85f
            }
        };
        return await GenerateTextAsync(request);
    }

    public async Task<List<string>> ExtractKeywordsAsync(string prompt)
    {
        var keywordPrompt =
            $"""
             À partir de cette question : '{prompt}', extrais uniquement les mots-clés importants sous forme d'une liste JSON. 
             Chaque élément de la liste doit contenir un seul mot-clé, sans doublons. 
             Réponds uniquement avec la liste, sans texte autour ni explication.
             """;
        
        var request = new OllamaInfraRequestDao
        {
            Prompt = keywordPrompt,
            Options = new OllamaInfraOptionsDao
            {
                Temperature = 0.4f,
                TopP = 0.85f
            }
        };

        var jsonResponse = await GenerateTextAsync(request);

        if (string.IsNullOrWhiteSpace(jsonResponse))
        {
            logger.LogError("Réponse vide reçue de Ollama lors de l'extraction des mots-clés.");
            return [];
        }

        try
        {
            var raw = JsonConvert.DeserializeObject<RawOllamaResponseDao>(jsonResponse);
            var keywords = JsonConvert.DeserializeObject<List<string>>(raw?.Response ?? string.Empty);
            return keywords ?? [];
        }
        catch (JsonException ex)
        {
            logger.LogError(ex, "Erreur lors du parsing JSON de la réponse de Ollama.");
            return [];
        }
    }

    private static string CleanText(string input)
    {
        if (string.IsNullOrWhiteSpace(input)) return input;

        var normalized = input.Normalize(NormalizationForm.FormD);
        var filtered = normalized.Where(c => CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark);
        return new string(filtered.ToArray()).Normalize(NormalizationForm.FormC);
    }
}
