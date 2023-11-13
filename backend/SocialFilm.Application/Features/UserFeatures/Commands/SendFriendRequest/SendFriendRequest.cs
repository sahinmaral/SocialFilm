using MediatR;

using SocialFilm.Application.Common;
using SocialFilm.Domain.DTOs;
using SocialFilm.Domain.Entities;
using SocialFilm.Domain.Enums;
using SocialFilm.Domain.Exceptions;

namespace SocialFilm.Application.Features.UserFeatures.Commands.SendFriendRequest;

public sealed record SendFriendRequestCommand(string UserId, string FriendId) : IRequest<MessageResponse>;
public sealed class SendFriendRequestCommandHandler : IRequestHandler<SendFriendRequestCommand, MessageResponse>
{
    private readonly IRepositoryManager _repositoryManager;
    public SendFriendRequestCommandHandler(IRepositoryManager repositoryManager)
    {
        _repositoryManager = repositoryManager;
    }

    public async Task<MessageResponse> Handle(SendFriendRequestCommand request, CancellationToken cancellationToken)
    {
        var existedUser = await _repositoryManager.UserRepository.GetByExpressionAsync(x => x.Id == request.UserId) 
            ?? throw new EntityNullException($"{request.UserId} ID sahip kullanıcı bulunamadı");

        var existedFriend = await _repositoryManager.UserRepository.GetByExpressionAsync(x => x.Id == request.FriendId) 
            ?? throw new EntityNullException($"{request.FriendId} ID sahip kullanıcı bulunamadı");

        existedUser.UserFriends.Add(new UserFriend()
        {
            User = existedUser,
            Friend = existedFriend,
            Status = FriendRequestStatus.WAITING
        });

        await _repositoryManager.SaveChangesAsync(cancellationToken);

        return new MessageResponse("Basarili");

    }
}
