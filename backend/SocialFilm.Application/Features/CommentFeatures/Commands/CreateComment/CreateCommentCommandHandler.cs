using AutoMapper;

using MediatR;

using SocialFilm.Application.Services;
using SocialFilm.Domain.DTOs;
using SocialFilm.Domain.Entities;
using SocialFilm.Domain.Repositories;

namespace SocialFilm.Application.Features.CommentFeatures.Commands.CreateComment;

public sealed class CreateCommentCommandHandler : IRequestHandler<CreateCommentCommand, MessageResponse>
{
    private readonly ICommentService _commentService;
    private readonly IPostService _postService;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    public CreateCommentCommandHandler(IPostService postService, ICommentService commentService, IMapper mapper, IUnitOfWork unitOfWork)
    {
        _commentService = commentService;
        _postService = postService;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<MessageResponse> Handle(CreateCommentCommand request, CancellationToken cancellationToken)
    {
        Post? postToAddedComment = await _postService.GetByIdAsync(request.PostId,cancellationToken);
        if (postToAddedComment == null)
            throw new Exception($"{request.PostId} ID ye sahip gönderi bulunamadı.");

        if(request.PreviousCommentId != null){
            Comment? previousComment = await _commentService.GetByIdAsync(request.PreviousCommentId, cancellationToken);

            if (previousComment == null)
                throw new Exception($"{request.PreviousCommentId} ID ye sahip yorum bulunamadı.");
        }

        Comment newComment = _mapper.Map<Comment>(request);

        await _commentService.AddAsync(newComment, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return new MessageResponse("Basarili");
    }
}
