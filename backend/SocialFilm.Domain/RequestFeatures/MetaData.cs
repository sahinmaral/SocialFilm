namespace SocialFilm.Domain.RequestFeatures;

public class MetaData
{
    public int CurrentPage { get; set; }
    public int PageSize { get; set; }
    public int TotalPages { get; set; }
    public int TotalRecords { get; set; }
    public bool IsFirstPage { get; set; }
    public bool IsLastPage { get; set; }
}
