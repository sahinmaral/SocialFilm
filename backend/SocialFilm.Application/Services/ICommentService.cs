using SocialFilm.Domain.Entities;

namespace SocialFilm.Application.Services;

public interface ICommentService
{
    Task AddAsync(Comment comment, CancellationToken cancellationToken);
    Task<Comment?> GetByIdAsync(string commentId, CancellationToken cancellationToken);
    Task<Comment?> GetByIdDetailAsync(string commentId, CancellationToken cancellationToken);
    void Delete(Comment deletedComment);
}