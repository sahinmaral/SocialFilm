using SocialFilm.Domain.DTOs;
using SocialFilm.Domain.Entities;

namespace SocialFilm.Application.Services;

public interface IPostService
{
    Task AddAsync(Post post, CancellationToken cancellationToken);
    void Delete(Post deletedPost);
    Task<Post?> GetByIdAsync(string postId, CancellationToken cancellationToken);
    Task<Post?> GetByIdDetailedAsync(string postId, CancellationToken cancellationToken);
    Task<PaginationResult<ReadPostDTO>> GetAllByUserIdAsPaginatedAsync(string userId, int pageSize, int pageNumber);
}
