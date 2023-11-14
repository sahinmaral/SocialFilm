using SocialFilm.Domain.Entities;
using SocialFilm.Domain.Enums;

namespace SocialFilm.Domain.Extensions;

public static class SavedFilmRepositoryExtensions
{
    public static IQueryable<SavedFilm> SearchByFilmName(this IQueryable<SavedFilm> savedFilms, string? searchTerm)
    {
        if (string.IsNullOrEmpty(searchTerm))
            return savedFilms;

        return savedFilms.Where(sf => sf.Film.Name.ToLower().Contains(searchTerm.ToLower()));
    }

    public static IQueryable<SavedFilm> FilterByStatus(this IQueryable<SavedFilm> savedFilms, SavedFilmStatus? savedFilmStatus)
    {
        if (savedFilmStatus is null)
            return savedFilms;

        return savedFilms.Where(sf => sf.Status == savedFilmStatus);
    }
}
