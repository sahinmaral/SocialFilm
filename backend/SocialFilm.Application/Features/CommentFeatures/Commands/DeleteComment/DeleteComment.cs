using MediatR;

using SocialFilm.Application.Common;
using SocialFilm.Domain.DTOs;
using SocialFilm.Domain.Entities;

namespace SocialFilm.Application.Features.CommentFeatures.Commands.DeleteComment;

public sealed record DeleteCommentCommand(string CommentId) : IRequest<MessageResponse>;
public sealed class DeleteCommentHandler : IRequestHandler<DeleteCommentCommand, MessageResponse>
{
    private readonly IRepositoryManager _repositoryManager;

    public DeleteCommentHandler(IRepositoryManager repositoryManager)
    {
        _repositoryManager = repositoryManager;
    }

    public async Task<MessageResponse> Handle(DeleteCommentCommand request, CancellationToken cancellationToken)
    {
        //TODO: Komple silmek yerine Status adli bir property olusturup Query Filter ile silinmis ozelligi saglayabiliriz.

        Comment? deletedComment = await _repositoryManager
            .CommentRepository
            .GetByIdDetailAsync(request.CommentId, cancellationToken) 
            ?? throw new Exception($"{request.CommentId} ID ye sahip yorum bulunamadı.");

        deletedComment.SubComments.ForEach(_repositoryManager.CommentRepository.Delete);

        _repositoryManager.CommentRepository.Delete(deletedComment);
        await _repositoryManager.SaveChangesAsync(cancellationToken);

        return new MessageResponse("Basarili");
    }
}
