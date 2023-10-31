using Microsoft.EntityFrameworkCore;

using SocialFilm.Domain.Entities;
using SocialFilm.Domain.Repositories;
using SocialFilm.Persistance.Context;

namespace SocialFilm.Infrastructure.Repositories;

public sealed class CommentRepository : GenericRepository<Comment, AppDbContext>, ICommentRepository
{
    private readonly AppDbContext _context;

    public CommentRepository(AppDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<Comment?> GetByIdDetailAsync(string commentId, CancellationToken cancellationToken)
    {
        return await _context
            .Set<Comment>()
            .Include(x => x.SubComments)
            .FirstOrDefaultAsync(x => x.Id == commentId, cancellationToken);
    }
}