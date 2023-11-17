using MediatR;

using Microsoft.EntityFrameworkCore;

using SocialFilm.Application.Common;
using SocialFilm.Domain.DTOs;
using SocialFilm.Domain.Entities;
using SocialFilm.Domain.Enums;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialFilm.Application.Features.UserFeatures.Queries.GetUserProfileStatistics;


public record GetUserProfileStatisticsCommand(string UserId) : IRequest<UserProfileStatisticsDTO>;
public class GetUserProfileStatisticsCommandHandler : IRequestHandler<GetUserProfileStatisticsCommand, UserProfileStatisticsDTO>
{
    private readonly IRepositoryManager _repositoryManager;

    public GetUserProfileStatisticsCommandHandler(IRepositoryManager repositoryManager)
    {
        _repositoryManager = repositoryManager;
    }

    public async Task<UserProfileStatisticsDTO> Handle(GetUserProfileStatisticsCommand request, CancellationToken cancellationToken)
    {
        User? user = await _repositoryManager
            .UserRepository
            .GetByIdAsync(request.UserId, cancellationToken)
            ?? throw new Exception($"{request.UserId} ID sahip kullanıcı bulunamadı");

        int friendCount = await _repositoryManager
            .UserRepository
            .GetUserFriendsById(request.UserId)
            .Where(x => x.Status == FriendRequestStatus.ACCEPTED)
            .CountAsync(cancellationToken);

        int watchedFilmCount = await _repositoryManager
            .SavedFilmRepository
            .GetAll()
            .Where(x => x.UserId == request.UserId && x.Status == SavedFilmStatus.WATCHED)
            .CountAsync(cancellationToken);

        int postCount = await _repositoryManager
            .PostRepository
            .GetAll()
            .Where(x => x.UserId == request.UserId)
            .CountAsync(cancellationToken);

        return new UserProfileStatisticsDTO()
        {
            FriendCount = friendCount,
            WatchedFilmCount = watchedFilmCount,
            PostCount = postCount
        };
    }
}
