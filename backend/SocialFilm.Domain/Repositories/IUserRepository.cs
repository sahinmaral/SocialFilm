using SocialFilm.Domain.Entities;

using System.Runtime.CompilerServices;

namespace SocialFilm.Domain.Repositories;

public interface IUserRepository : IRepository<User>
{
    void UpdateRequestState(User userForAcceptRequest, User userForSendRequest);
}