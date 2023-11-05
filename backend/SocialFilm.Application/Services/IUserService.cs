using SocialFilm.Domain.Common;
using SocialFilm.Domain.Entities;

using System.Linq.Expressions;

namespace SocialFilm.Application.Services;

public interface IUserService
{
    void SendFriendRequest(User userForSendRequest, User friend);
    void AcceptFriendRequest(User userForAcceptRequest, User userForSendRequest);
    IQueryable<User> GetWhere(Expression<Func<User, bool>> expression);
    Task<User?> GetByIdAsync(string id,CancellationToken cancellationToken);
}