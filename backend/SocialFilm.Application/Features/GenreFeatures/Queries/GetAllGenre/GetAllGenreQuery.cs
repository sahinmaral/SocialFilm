using MediatR;

using SocialFilm.Domain.Entities;

namespace SocialFilm.Application.Features.GenreFeatures.Queries.GetAllGenre;

public sealed record GetAllGenreQuery() : IRequest<List<Genre>>;
