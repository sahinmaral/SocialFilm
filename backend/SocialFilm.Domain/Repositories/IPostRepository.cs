using SocialFilm.Domain.Entities;

namespace SocialFilm.Domain.Repositories;

public interface IPostRepository : IRepository<Post>
{
    Task<Post?> GetByIdDetailedAsync(string postId, CancellationToken cancellationToken);
}