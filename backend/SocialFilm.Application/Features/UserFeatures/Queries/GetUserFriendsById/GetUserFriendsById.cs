using AutoMapper;

using MediatR;

using Microsoft.EntityFrameworkCore;

using SocialFilm.Application.Common;
using SocialFilm.Domain.DTOs;
using SocialFilm.Domain.Entities;
using SocialFilm.Domain.Enums;
using SocialFilm.Domain.Exceptions;
using SocialFilm.Domain.RequestFeatures;

namespace SocialFilm.Application.Features.UserFeatures.Queries.GetUserFriendsById;

public record GetUserFriendsByIdCommand(string UserId, UserFriendParameters? Parameters = null) : IRequest<PaginationResult<ReadUserDTO>>;
public sealed class GetUserFriendsByIdCommandHandler : IRequestHandler<GetUserFriendsByIdCommand, PaginationResult<ReadUserDTO>>
{
    private readonly IRepositoryManager _repositoryManager;
    private readonly IMapper _mapper;
    public GetUserFriendsByIdCommandHandler(IRepositoryManager repositoryManager, IMapper mapper)
    {
        _repositoryManager = repositoryManager;
        _mapper = mapper;
    }
    public async Task<PaginationResult<ReadUserDTO>> Handle(GetUserFriendsByIdCommand request, CancellationToken cancellationToken)
    {
        if (request.Parameters == null)
        {
            request = new GetUserFriendsByIdCommand(UserId: request.UserId)
            {
                Parameters = new UserFriendParameters()
            };
        }

        User? userEntity = await _repositoryManager
            .UserRepository
            .GetByIdAsync(request.UserId, cancellationToken)
            ?? throw new EntityNullException($"{request.UserId} ID sahip kullanıcı bulunamadı");

        var userFriends = _repositoryManager
            .UserRepository
            .GetUserFriendsById(request.UserId)
            .Include(x => x.Friend)
            .Where(x => x.Status == FriendRequestStatus.ACCEPTED)
            .Select(x => x.Friend)
            .OrderBy(x => x.UserName);


        var pagedSortedUserFriends = PagedList<User>
            .ToPagedList(userFriends,
                            request.Parameters.PaginationParameters.CurrentPage,
                            request.Parameters.PaginationParameters.PageSize
            );

        var mappedUserFriends = _mapper.Map<List<ReadUserDTO>>(pagedSortedUserFriends);

        return new PaginationResult<ReadUserDTO>(Data: mappedUserFriends, MetaData: pagedSortedUserFriends.MetaData);
    }
}
