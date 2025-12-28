using System.Text;
using System.Threading.RateLimiting;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using ProjectDroit.Core.Exceptions.Infrastructure;
using ProjectDroit.Core.Interfaces.Infrastructure;
using ProjectDroit.Domain.Entities.Legifrance;
using ProjectDroit.Domain.Entities.Legifrance.Enum;
using ProjectDroit.Infrastructure.Http.Legifrance.Daos.Request;
using ProjectDroit.Infrastructure.Http.Legifrance.Daos.Request.Enums;
using ProjectDroit.Infrastructure.Http.Legifrance.Daos.Response;

namespace ProjectDroit.Infrastructure.Http.Legifrance.Repositories;

public class LegifranceRepository(
    IHttpClientFactory httpClientFactory, 
    ILogger<LegifranceRepository> logger,
    IMapper mapper) : ILegifranceRepository
{
    private static readonly FixedWindowRateLimiter RateLimiterWindow = new FixedWindowRateLimiter(
        new FixedWindowRateLimiterOptions
        {
            PermitLimit = 2,                   
            Window = TimeSpan.FromSeconds(1),  
            QueueProcessingOrder = QueueProcessingOrder.OldestFirst,
            QueueLimit = 10,                   
            AutoReplenishment = true           
        }); //TODO : Configurer aussi pour la prod
    
    private readonly HttpClient _httpClient = httpClientFactory.CreateClient("Legifrance");

    public async Task<List<SearchResult>> SpecificSearchLawAsync(List<string> keywordsToSearch, IEnumerable<FondApiName> fonds)
    {
        try
        {
            var tasks = new List<Task<List<SearchResult>>>();
            foreach (var fond in fonds)
            {
                switch (fond)
                {
                    case FondApiName.Code:
                        tasks.Add(SearchLawsAsync(RechercheRequestBuilder.BuildCodeOrLodaOrKaliResearchRequest(keywordsToSearch,FondEnum.CODE_ETAT)));
                        break;
                    case FondApiName.Legi:
                        tasks.Add(SearchLawsAsync(RechercheRequestBuilder.BuildCodeOrLodaOrKaliResearchRequest(keywordsToSearch,FondEnum.LODA_ETAT)));
                        break;
                    case FondApiName.Kali:
                        tasks.Add(SearchLawsAsync(RechercheRequestBuilder.BuildCodeOrLodaOrKaliResearchRequest(keywordsToSearch,FondEnum.KALI)));
                        break;
                    case FondApiName.Jorf:
                        tasks.Add(SearchLawsAsync(RechercheRequestBuilder.BuildJorfResearchRequest(keywordsToSearch)));
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }

            try
            {
                await Task.WhenAll(tasks);
            }
            catch (Exception)
            {
                // ignored
            }


            List<SearchResult> searchResults = new List<SearchResult>();
            foreach (var task in tasks)
            {
                if (task is { IsCompletedSuccessfully: true, Result: not null })
                {
                    searchResults.AddRange(task.Result);
                }
                else if (task.IsFaulted)
                {
                    var innerException = task.Exception?.InnerExceptions.FirstOrDefault() ?? task.Exception;
                    logger.LogError(innerException ?? task.Exception, "Erreur lors d'une recherche spécifique sur Légifrance (Tâche échouée).");
                }
            }
            
            return searchResults;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Erreur lors de la recherche sur Légifrance.");
            return [];
        }
   
    }

    public async Task<List<SearchResult>> GlobalSearchLawsAsync(List<string> keywordsToSearch)
    {
        try
        {
            RechercheRequest request = RechercheRequestBuilder.BuildGlobalResearchRequest(keywordsToSearch);
            return await SearchLawsAsync(request);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Erreur lors de la recherche sur Légifrance.");
            return [];
        }
    }

    private async Task<List<SearchResult>> SearchLawsAsync(RechercheRequest request)
    {
        RateLimitLease lease = await RateLimiterWindow.AcquireAsync(permitCount: 1);
        using (lease)
        {
            if (lease.IsAcquired)
            {
                try
                {
                    var json = JsonConvert.SerializeObject(request, new JsonSerializerSettings
                    {
                        ContractResolver = new CamelCasePropertyNamesContractResolver(),
                        Formatting = Formatting.None
                    });
                    var content = new StringContent(json, Encoding.UTF8, "application/json");

                    var response = await _httpClient.PostAsync("search", content);
                    response.EnsureSuccessStatusCode();
        
                    var resultJson = await response.Content.ReadAsStringAsync();

                    var result = JsonConvert.DeserializeObject<RechercheResponse>(resultJson);
            
                    List<SearchResult>? searchResult = mapper.Map<List<SearchResult>>(result?.Results);
            
                    return searchResult;
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "Erreur lors de la recherche sur Légifrance.");
                    return new List<SearchResult>();
                }
             
            }
            else
            {
                throw new RateLimiterException();
            }
        }
    }

    public async Task<string> GetLawAsync(string id)
    {
        RateLimitLease lease = await RateLimiterWindow.AcquireAsync(permitCount: 1);

        using (lease)
        {
            if (lease.IsAcquired)
            {
                try
                {
                    var getArticleRequest = new GetArticleRequest()
                    {
                        Id = id,
                    };
                    var json = JsonConvert.SerializeObject(getArticleRequest, new JsonSerializerSettings
                    {
                        ContractResolver = new CamelCasePropertyNamesContractResolver(),
                        Formatting = Formatting.None
                    });
                    var content = new StringContent(json, Encoding.UTF8, "application/json");
                    var response = await _httpClient.PostAsync("consult/getArticle", content);
                    response.EnsureSuccessStatusCode();
        
                    var resultJson = await response.Content.ReadAsStringAsync();
        
                    var result = JsonConvert.DeserializeObject<GetArticleResponse>(resultJson);

                    return result.Article.Texte;
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "Erreur lors de la recherche sur Légifrance.");
                    return string.Empty;
                }
            }
            else
            {
                throw new RateLimiterException();
            }
        }

     

    }

    
    
}