using AutoMapper;

using MediatR;

using Microsoft.EntityFrameworkCore;

using SocialFilm.Application.Common;
using SocialFilm.Domain.DTOs;
using SocialFilm.Domain.Entities;
using SocialFilm.Domain.Exceptions;
using SocialFilm.Domain.RequestFeatures;

namespace SocialFilm.Application.Features.CommentFeatures.Queries.GetAllByParentCommentId;

public sealed record GetAllByParentCommentIdCommand(
string Id, int PageSize = 5, int PageNumber = 1
) : IRequest<PaginationResult<ReadSubCommentDTO>>;

public sealed class GetAllByParentCommentIdCommandHandler : IRequestHandler<GetAllByParentCommentIdCommand, PaginationResult<ReadSubCommentDTO>>
{
    private readonly IMapper _mapper;
    private readonly IRepositoryManager _repositoryManager;

    public GetAllByParentCommentIdCommandHandler(IMapper mapper, IRepositoryManager repositoryManager)
    {
        _mapper = mapper;
        _repositoryManager = repositoryManager;
    }

    public async Task<PaginationResult<ReadSubCommentDTO>> Handle(GetAllByParentCommentIdCommand request, CancellationToken cancellationToken)
    {
        Comment? parentComment = await _repositoryManager
            .CommentRepository
            .GetByIdAsync(request.Id)
            ?? throw new EntityNullException($"{request.Id} ID sahip yorum bulunamadı");

        var subCommentsQuery = _repositoryManager.CommentRepository
            .GetAll()
            .Where(x => x.PreviousCommentId == request.Id)
            .Include(x => x.User)
            .OrderByDescending(x => x.CreatedAt);

        var pagedSubComments = PagedList<Comment>.ToPagedList(subCommentsQuery, request.PageNumber, request.PageSize);

        var mappedSubComments = _mapper.Map<List<ReadSubCommentDTO>>(pagedSubComments);

        return new PaginationResult<ReadSubCommentDTO>(Data: mappedSubComments, MetaData: pagedSubComments.MetaData);
    }
}
