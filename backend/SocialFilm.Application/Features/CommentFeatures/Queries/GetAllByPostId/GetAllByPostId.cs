using AutoMapper;

using MediatR;

using Microsoft.EntityFrameworkCore;

using SocialFilm.Application.Common;
using SocialFilm.Domain.DTOs;
using SocialFilm.Domain.Entities;
using SocialFilm.Domain.Exceptions;
using SocialFilm.Domain.RequestFeatures;

namespace SocialFilm.Application.Features.CommentFeatures.Queries.GetAllByPostId;

public record GetAllByPostIdCommand(
    string PostId, int ParentPageSize = 10, int ChildPageSize = 5, int PageNumber = 1
    ) : IRequest<PaginationResult<ReadCommentDTO>>;

public class GetAllByPostIdCommandHandler : IRequestHandler<GetAllByPostIdCommand, PaginationResult<ReadCommentDTO>>
{
    private readonly IMapper _mapper;
    private readonly IRepositoryManager _repositoryManager;

    public GetAllByPostIdCommandHandler(IMapper mapper, IRepositoryManager repositoryManager)
    {
        _mapper = mapper;
        _repositoryManager = repositoryManager;
    }

    public async Task<PaginationResult<ReadCommentDTO>> Handle(GetAllByPostIdCommand request, CancellationToken cancellationToken)
    {
        Post? postToCheckIfExisted = await _repositoryManager
            .PostRepository
            .GetByIdAsync(request.PostId, cancellationToken) 
            ?? throw new EntityNullException($"{request.PostId} ID sahip gönderi bulunamadı");

        var mainCommentsQuery = _repositoryManager.CommentRepository
            .GetAll()
            .Where(x => x.PostId == request.PostId && x.PreviousCommentId == null)
            .Include(x => x.User)
            .OrderBy(x => x.CreatedAt);

        var pagedMainComments = PagedList<Comment>.ToPagedList(mainCommentsQuery, request.PageNumber, request.ParentPageSize);

        var mappedMainComments = _mapper.Map<List<ReadCommentDTO>>(pagedMainComments);

        foreach (var mainComment in mappedMainComments)
        {
            var subCommentsQuery = _repositoryManager.CommentRepository
                .GetWhere(sc => sc.PreviousCommentId == mainComment.Id)
                .Include(x => x.User)
                .OrderBy(x => x.CreatedAt);

            var pagedSubComments = PagedList<Comment>.ToPagedList(subCommentsQuery, 1, request.ChildPageSize);

            var mappedSubComments = _mapper.Map<List<ReadSubCommentDTO>>(pagedSubComments);

            mainComment.SubComments = new PaginationResult<ReadSubCommentDTO>(
                Data: mappedSubComments,
                MetaData: pagedSubComments.MetaData
            );
        }


        return new PaginationResult<ReadCommentDTO>(Data: mappedMainComments, MetaData: pagedMainComments.MetaData);
    }
}
