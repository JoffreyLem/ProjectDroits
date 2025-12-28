namespace ProjectDroit.Core.Dto;

public class CodeWithArticles
{
    public string Cid { get; set; }
    public string CodeTitle { get; set; }
    public List<Article> Articles { get; set; } = new();
}