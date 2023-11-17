using Microsoft.EntityFrameworkCore;

using SocialFilm.Domain.Entities;
using SocialFilm.Domain.Enums;
using SocialFilm.Domain.Repositories;
using SocialFilm.Infrastructure.Repositories;
using SocialFilm.Persistance.Context;

namespace SocialFilm.Persistance.Repositories;

public sealed class UserRepository : GenericRepository<User, AppDbContext>, IUserRepository
{
    private readonly AppDbContext _context;

    public UserRepository(AppDbContext context) : base(context)
    {
        _context = context;
    }

    public void UpdateRequestState(User userForAcceptRequest, User userForSendRequest)
    {
        var userFriendWhoIsWaitingToAcceptRequest = userForAcceptRequest.UserFriends.FirstOrDefault(x => x.FriendId == userForSendRequest.Id);
        if (userFriendWhoIsWaitingToAcceptRequest is null)
            throw new InvalidOperationException($"{userForSendRequest} ID li kullanici , size arkadaslik istegi yollamadi");

        userFriendWhoIsWaitingToAcceptRequest.Status = FriendRequestStatus.ACCEPTED;

        var entry = _context.Entry(userFriendWhoIsWaitingToAcceptRequest);
        entry.State = EntityState.Modified;
    }

    public IQueryable<UserFriend> GetUserFriendsById(string id)
    {
        var userFriendEntity = _context.Set<UserFriend>();
        return userFriendEntity.Where(x => x.UserId == id);
    }

}
