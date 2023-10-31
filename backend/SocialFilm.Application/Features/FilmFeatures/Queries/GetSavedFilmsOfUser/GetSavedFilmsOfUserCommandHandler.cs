using MediatR;

using SocialFilm.Application.Services;
using SocialFilm.Domain.DTOs;

namespace SocialFilm.Application.Features.FilmFeatures.Queries.GetSavedFilmsOfUser
{
    public sealed class GetSavedFilmsOfUserCommandHandler : IRequestHandler<GetSavedFilmsOfUserCommand, PaginationResult<ReadSavedFilmDto>>
    {
        private readonly ISavedFilmService _savedFilmService;

        public GetSavedFilmsOfUserCommandHandler(ISavedFilmService savedFilmService)
        {
            _savedFilmService = savedFilmService;
        }

        public async Task<PaginationResult<ReadSavedFilmDto>> Handle(GetSavedFilmsOfUserCommand request, CancellationToken cancellationToken)
        {
            return await _savedFilmService.GetSavedFilmsByUserIdDetailedAsQueryableAsync(request);
        }
    }
}
