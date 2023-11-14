using SocialFilm.Domain.Enums;

namespace SocialFilm.Domain.RequestFeatures;

public class SavedFilmParameters 
{
    public string? SearchTerm { get; init; }
    public SavedFilmStatus? Status { get; init; }
    public PaginationParameters PaginationParameters { get; init; } = new PaginationParameters();
}
