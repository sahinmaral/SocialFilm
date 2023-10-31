using MediatR;

using SocialFilm.Domain.DTOs;
using SocialFilm.Domain.Enums;

namespace SocialFilm.Application.Features.FilmFeatures.Commands;

public sealed record SaveFilmCommand(string FilmId, string UserId, SavedFilmStatus Status) : IRequest<MessageResponse>;

