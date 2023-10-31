using SocialFilm.Application.Services;
using SocialFilm.Domain.Entities;
using SocialFilm.Domain.Repositories;

namespace SocialFilm.Persistance.Services;

public sealed class CommentService : ICommentService
{
    private readonly ICommentRepository _commentRepository;

    public CommentService(ICommentRepository commentRepository)
    {
        _commentRepository = commentRepository;
    }

    public async Task AddAsync(Comment comment, CancellationToken cancellationToken)
    {
        await _commentRepository.AddAsync(comment, cancellationToken);
    }

    public void Delete(Comment deletedComment)
    {
        _commentRepository.Delete(deletedComment);
    }

    public async Task<Comment?> GetByIdAsync(string commentId, CancellationToken cancellationToken)
    {
        return await _commentRepository.GetByExpressionAsync(x => x.Id == commentId,cancellationToken);
    }

    public async Task<Comment?> GetByIdDetailAsync(string commentId, CancellationToken cancellationToken)
    {
        return await _commentRepository.GetByIdDetailAsync(commentId, cancellationToken);
    }
}

