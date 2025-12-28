
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.WebUtilities;

namespace ProjectDroit.Infrastructure.Http 
{
    public static class UriExtensions
    {
        public static string RemoveQueryStringByKey(string? uriString, string keyToRemove)
        {
            if (string.IsNullOrEmpty(uriString) || string.IsNullOrEmpty(keyToRemove))
                return uriString ?? string.Empty; // Gérer null

            bool likelyContainsKey = uriString.Contains("?" + keyToRemove + "=", StringComparison.OrdinalIgnoreCase) ||
                                     uriString.Contains("&" + keyToRemove + "=", StringComparison.OrdinalIgnoreCase);

            if (!uriString.Contains('?') || !likelyContainsKey)
            {
                return uriString;
            }

            try
            {
                var uri = new Uri(uriString);
                var baseUri = uri.GetLeftPart(UriPartial.Path);
                var fragment = uri.Fragment;
                var queryString = uri.Query.Length > 0 ? uri.Query.Substring(1) : string.Empty;

                if (string.IsNullOrEmpty(queryString)) return uriString;

                var queryParameters = QueryHelpers.ParseQuery(queryString);

                var filteredParameters = queryParameters
                    .Where(kvp => !kvp.Key.Equals(keyToRemove, StringComparison.OrdinalIgnoreCase))
                    .ToList();

                if (!filteredParameters.Any())
                {
                    return baseUri + fragment; 
                }

                var sb = new StringBuilder();
                foreach (var kvp in filteredParameters)
                {
                    foreach (var value in kvp.Value)
                    {
                        if (sb.Length > 0) sb.Append('&');
                        sb.Append(UrlEncoder.Default.Encode(kvp.Key));
                        sb.Append('=');
                        if (value != null) sb.Append(UrlEncoder.Default.Encode(value));
                    }
                }

                var uriBuilder = new UriBuilder(uri) { Query = sb.ToString(), Fragment = "" };

                 return uriBuilder.Uri.GetLeftPart(UriPartial.Path) + uriBuilder.Uri.Query + fragment;


            }
            catch (UriFormatException ex)
            {
                Console.WriteLine($"URI Invalide lors de la suppression de la clé '{keyToRemove}': {ex.Message}"); 
                return uriString; 
            }
            catch (Exception ex)
            {
                 Console.WriteLine($"Erreur inattendue lors de la suppression de la clé '{keyToRemove}': {ex.Message}");
                 return uriString;
            }
        }
    }
}