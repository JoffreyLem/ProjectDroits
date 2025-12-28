using System.Collections.Specialized;

namespace ProjectDroit.Domain.Entities.Legifrance;

public class SearchResult
{
    public string TextTitle { get; set; }
    public string TextId { get; set; }
    
    public string Origin { get; set; }
    public IEnumerable<Sections> Sections { get; set; }
}