using AutoMapper;

using MediatR;

using SocialFilm.Application.Common;
using SocialFilm.Domain.DTOs;
using SocialFilm.Domain.Entities;

namespace SocialFilm.Application.Features.CommentFeatures.Commands.CreateComment;

public sealed record CreateCommentCommand(string PostId, string UserId, string Message, string? PreviousCommentId) : IRequest<MessageResponse>;
public sealed class CreateCommentHandler : IRequestHandler<CreateCommentCommand, MessageResponse>
{
    private readonly IMapper _mapper;
    private readonly IRepositoryManager _repositoryManager;
    public CreateCommentHandler(IMapper mapper, IRepositoryManager repositoryManager)
    {
        _mapper = mapper;
        _repositoryManager = repositoryManager;
    }

    public async Task<MessageResponse> Handle(CreateCommentCommand request, CancellationToken cancellationToken)
    {
        Post? postToAddedComment = await _repositoryManager
            .PostRepository
            .GetByIdAsync(request.PostId, cancellationToken)
            ?? throw new Exception($"{request.PostId} ID ye sahip gönderi bulunamadı.");

        if (request.PreviousCommentId != null)
        {
            Comment? previousComment = await _repositoryManager
                .CommentRepository
                .GetByIdAsync(request.PostId, cancellationToken)
                ?? throw new Exception($"{request.PreviousCommentId} ID ye sahip yorum bulunamadı.");
        }

        Comment newComment = _mapper.Map<Comment>(request);

        await _repositoryManager.CommentRepository.AddAsync(newComment, cancellationToken);
        await _repositoryManager.SaveChangesAsync(cancellationToken);

        return new MessageResponse("Basarili");
    }
}
