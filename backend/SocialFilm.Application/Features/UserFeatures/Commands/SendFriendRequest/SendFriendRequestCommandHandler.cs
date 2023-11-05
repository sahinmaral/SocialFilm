using MediatR;

using SocialFilm.Application.Services;
using SocialFilm.Domain.DTOs;
using SocialFilm.Domain.Exceptions;
using SocialFilm.Domain.Repositories;

namespace SocialFilm.Application.Features.UserFeatures.Commands.SendFriendRequest;

public sealed class SendFriendRequestCommandHandler : IRequestHandler<SendFriendRequestCommand, MessageResponse>
{
    private readonly IAuthService _authService;
    private readonly IUserService _userService;
    private readonly IUnitOfWork _unitOfWork;

    public SendFriendRequestCommandHandler(IAuthService authService, IUserService userService, IUnitOfWork unitOfWork)
    {
        _authService = authService;
        _userService = userService;
        _unitOfWork = unitOfWork;
    }

    public async Task<MessageResponse> Handle(SendFriendRequestCommand request, CancellationToken cancellationToken)
    {
        var existedUser = await _authService.GetUserByIdAsync(request.UserId);
        if (existedUser is null)
            throw new EntityNullException($"{request.UserId} ID sahip kullanıcı bulunamadı");

        var existedFriend = await _authService.GetUserByIdAsync(request.FriendId);
        if (existedFriend is null)
            throw new EntityNullException($"{request.FriendId} ID sahip kullanıcı bulunamadı");

        _userService.SendFriendRequest(existedUser, existedFriend);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return new MessageResponse("Basarili");

    }
}
