using MediatR;

using SocialFilm.Application.Models;

namespace SocialFilm.Application.Features.FilmFeatures.Commands;
public sealed record SearchFilmsQuery(string Name, string? ReleaseYear, int Page = 1) : IRequest<SearchFilmResponseModel>;