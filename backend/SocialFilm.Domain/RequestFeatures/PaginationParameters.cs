namespace SocialFilm.Domain.RequestFeatures;

public class PaginationParameters
{
    public int CurrentPage { get; init; } = 1;
    public int PageSize { get; init; } = 10;
}