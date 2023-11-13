using MediatR;

using Microsoft.EntityFrameworkCore;

using SocialFilm.Application.Common;
using SocialFilm.Domain.DTOs;
using SocialFilm.Domain.Exceptions;

namespace SocialFilm.Application.Features.UserFeatures.Commands.AcceptFriendRequest;

public sealed record AcceptFriendRequestCommand(string UserId, string FriendId) : IRequest<MessageResponse>;

public sealed class AcceptFriendRequestCommandHandler : IRequestHandler<AcceptFriendRequestCommand, MessageResponse>
{

    private readonly IRepositoryManager _repositoryManager;

    public AcceptFriendRequestCommandHandler(IRepositoryManager repositoryManager)
    {
        _repositoryManager = repositoryManager;
    }

    public async Task<MessageResponse> Handle(AcceptFriendRequestCommand request, CancellationToken cancellationToken)
    {
        var existedUser = await _repositoryManager
                                        .UserRepository
                                        .GetWhere(x => x.Id == request.FriendId)
                                        .Include(x => x.UserFriends)
                                        .FirstOrDefaultAsync(cancellationToken) 
        ?? throw new EntityNullException($"{request.FriendId} ID sahip kullanıcı bulunamadı");

        var existedFriend = await _repositoryManager
                                        .UserRepository
                                        .GetByExpressionAsync(x => x.Id == request.UserId,cancellationToken)
        ?? throw new EntityNullException($"{request.UserId} ID sahip kullanıcı bulunamadı");

        _repositoryManager.UserRepository.UpdateRequestState(existedUser, existedFriend);
        await _repositoryManager.SaveChangesAsync(cancellationToken);

        return new MessageResponse("Basarili");

    }
}
