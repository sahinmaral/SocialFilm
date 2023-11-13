using AutoMapper;

using MediatR;

using Microsoft.EntityFrameworkCore;

using SocialFilm.Application.Common;
using SocialFilm.Domain.DTOs;
using SocialFilm.Domain.Entities;
using SocialFilm.Domain.Exceptions;

namespace SocialFilm.Application.Features.PostFeatures.Queries.GetPostById;

public record GetPostByIdCommand(string Id) : IRequest<ReadPostDetailedDTO>;
public sealed class GetPostByIdCommandHandler : IRequestHandler<GetPostByIdCommand, ReadPostDetailedDTO>
{
    private readonly IRepositoryManager _repositoryManager;
    private readonly IMapper _mapper;
    public GetPostByIdCommandHandler(IMapper mapper, IRepositoryManager repositoryManager)
    {
        _mapper = mapper;
        _repositoryManager = repositoryManager;
    }

    public async Task<ReadPostDetailedDTO> Handle(GetPostByIdCommand request, CancellationToken cancellationToken)
    {
        Post? post = await _repositoryManager.PostRepository.GetByIdDetailedAsync(request.Id, cancellationToken);
        if (post is null)
            throw new EntityNullException($"{request.Id} ID sahip gönderi bulunamadı");

        //FIX : Film detaylarinda tur kisminin gelmesi gerekiyor.
        User user = await _repositoryManager.UserRepository.GetByIdAsync( post.UserId, cancellationToken);
        FilmDetail filmDetail = await _repositoryManager.FilmDetailRepository.GetByIdAsync(post.FilmId, cancellationToken);
        int commentCount = await _repositoryManager.CommentRepository.GetWhere(x => x.PostId == request.Id).CountAsync();

        ReadPostDetailedDTO mappedPost = _mapper.Map<ReadPostDetailedDTO>(post);
        mappedPost.User = _mapper.Map<ReadUserDTO>(user);
        mappedPost.FilmDetail = _mapper.Map<ReadFilmDetailDTO>(filmDetail);
        mappedPost.CommentCount = commentCount;

        return mappedPost;
    }
}
