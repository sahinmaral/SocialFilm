using Microsoft.EntityFrameworkCore;

using SocialFilm.Domain.Entities;
using SocialFilm.Domain.Repositories;
using SocialFilm.Persistance.Context;

namespace SocialFilm.Infrastructure.Repositories;

public sealed class PostRepository : GenericRepository<Post, AppDbContext>, IPostRepository
{
    private readonly AppDbContext _context;

    public PostRepository(AppDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<Post?> GetByIdDetailedAsync(string postId,CancellationToken cancellationToken)
    {
        return await _context.Set<Post>()
            .Include(x => x.PostPhotos)
            .SingleOrDefaultAsync(x => x.Id == postId,cancellationToken);
    }

}
