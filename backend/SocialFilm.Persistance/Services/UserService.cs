using SocialFilm.Application.Services;
using SocialFilm.Domain.Entities;
using SocialFilm.Domain.Enums;
using SocialFilm.Domain.Repositories;

using System.Linq.Expressions;

namespace SocialFilm.Persistance.Services;

public sealed class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public void SendFriendRequest(User userForSendRequest,User friend)
    {
        userForSendRequest.UserFriends.Add(new UserFriend()
        {
            User = userForSendRequest,
            Friend = friend,
            Status = FriendRequestStatus.WAITING
        });
    }

    public void AcceptFriendRequest(User userForAcceptRequest, User userForSendRequest)
    {
        _userRepository.UpdateRequestState(userForAcceptRequest, userForSendRequest);
    }

    public IQueryable<User> GetWhere(Expression<Func<User, bool>> expression)
    {
        return _userRepository.GetWhere(expression);
    }

    public async Task<User?> GetByIdAsync(string id, CancellationToken cancellationToken)
    {
        return await _userRepository.GetByExpressionAsync(x => x.Id == id, cancellationToken);
    }
}
