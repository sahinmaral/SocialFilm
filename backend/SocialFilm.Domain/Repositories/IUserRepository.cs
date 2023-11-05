using SocialFilm.Domain.Entities;

namespace SocialFilm.Domain.Repositories;

public interface IUserRepository : IRepository<User>
{
    void UpdateRequestState(User userForAcceptRequest, User userForSendRequest);
}