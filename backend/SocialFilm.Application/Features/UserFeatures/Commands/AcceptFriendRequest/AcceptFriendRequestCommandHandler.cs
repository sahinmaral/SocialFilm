using MediatR;

using Microsoft.EntityFrameworkCore;

using SocialFilm.Application.Services;
using SocialFilm.Domain.DTOs;
using SocialFilm.Domain.Exceptions;
using SocialFilm.Domain.Repositories;

namespace SocialFilm.Application.Features.UserFeatures.Commands.AcceptFriendRequest;

public sealed class AcceptFriendRequestCommandHandler : IRequestHandler<AcceptFriendRequestCommand, MessageResponse>
{
    private readonly IAuthService _authService;
    private readonly IUserService _userService;
    private readonly IUnitOfWork _unitOfWork;

    public AcceptFriendRequestCommandHandler(IAuthService authService, IUserService userService, IUnitOfWork unitOfWork)
    {
        _authService = authService;
        _userService = userService;
        _unitOfWork = unitOfWork;
    }

    public async Task<MessageResponse> Handle(AcceptFriendRequestCommand request, CancellationToken cancellationToken)
    {
        var existedUser = await _userService.GetWhere(x => x.Id == request.FriendId)
                                        .Include(x => x.UserFriends)
                                        .FirstOrDefaultAsync(cancellationToken);
                                        
        if (existedUser is null)
            throw new EntityNullException($"{request.FriendId} ID sahip kullanıcı bulunamadı");

        var existedFriend = await _authService.GetUserByIdAsync(request.UserId);
        if (existedFriend is null)
            throw new EntityNullException($"{request.UserId} ID sahip kullanıcı bulunamadı");

        _userService.AcceptFriendRequest(existedUser, existedFriend);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return new MessageResponse("Basarili");

    }
}
