using AutoMapper;

using MediatR;

using SocialFilm.Application.Features.CommentFeatures.Commands.DeleteComment;
using SocialFilm.Application.FileStorage;
using SocialFilm.Application.Services;
using SocialFilm.Domain.DTOs;
using SocialFilm.Domain.Entities;
using SocialFilm.Domain.Repositories;

namespace SocialFilm.Application.Features.CommentFeatures.Commands.CreateComment;

public sealed class DeleteCommentCommandHandler : IRequestHandler<DeleteCommentCommand, MessageResponse>
{
    private readonly ICommentService _commentService;
    private readonly IUnitOfWork _unitOfWork;
    public DeleteCommentCommandHandler(ICommentService commentService,IUnitOfWork unitOfWork)
    {
        _commentService = commentService;
        _unitOfWork = unitOfWork;
    }

    public async Task<MessageResponse> Handle(DeleteCommentCommand request, CancellationToken cancellationToken)
    {
        //TODO: Komple silmek yerine Status adli bir property olusturup Query Filter ile silinmis ozelligi saglayabiliriz.

        Comment? deletedComment = await _commentService.GetByIdDetailAsync(request.CommentId, cancellationToken);
        if (deletedComment == null)
            throw new Exception($"{request.CommentId} ID ye sahip yorum bulunamadı.");

        deletedComment.SubComments.ForEach(_commentService.Delete);

        _commentService.Delete(deletedComment);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return new MessageResponse("Basarili");
    }
}
