using SocialFilm.Domain.Entities;

namespace SocialFilm.Domain.Repositories;

public interface ICommentRepository : IRepository<Comment>
{
    Task<Comment?> GetByIdDetailAsync(string commentId, CancellationToken cancellationToken);
}
