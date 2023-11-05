using AutoMapper;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

using SocialFilm.Application.Services;
using SocialFilm.Domain.DTOs;
using SocialFilm.Domain.Entities;
using SocialFilm.Domain.Exceptions;
using SocialFilm.Domain.Repositories;
using SocialFilm.Persistance.Extensions;

namespace SocialFilm.Persistance.Services;

public sealed class PostService : IPostService
{
    private readonly IPostRepository _postRepository;
    private readonly IFilmDetailRepository _filmDetailRepository;
    private readonly UserManager<User> _userManager;
    private readonly IMapper _mapper;
    public PostService(IPostRepository postRepository, IFilmDetailRepository filmDetailRepository, UserManager<User> userManager, IMapper mapper)
    {
        _postRepository = postRepository;
        _filmDetailRepository = filmDetailRepository;
        _userManager = userManager;
        _mapper = mapper;
    }

    private async Task<FilmDetail?> CheckIfFilmDetailIsExistedAndReturn(string filmDetailId, CancellationToken cancellationToken)
    {
        return await _filmDetailRepository.GetByExpressionAsync(x => x.Id == filmDetailId,cancellationToken);
    }

    private async Task<User?> CheckIfUserIsExistedAndReturn(string userId)
    {
        return await _userManager.FindByIdAsync(userId);
    }

    public void Delete(Post deletedPost)
    {
        _postRepository.Delete(deletedPost);
    }

    public async Task AddAsync(Post post, CancellationToken cancellationToken)
    {
        FilmDetail? existedFilmDetail = await CheckIfFilmDetailIsExistedAndReturn(post.FilmId,cancellationToken);

        if (existedFilmDetail is null)
            throw new EntityNullException($"{post.FilmId} ID sahip film bulunamadı.");

        User? existedUser = await CheckIfUserIsExistedAndReturn(post.UserId);

        if (existedUser is null)
            throw new EntityNullException($"{post.UserId} ID sahip kullanıcı bulunamadı.");

        await _postRepository.AddAsync(post, cancellationToken);
    }

    public async Task<Post?> GetByIdAsync(string postId, CancellationToken cancellationToken)
    {
        return await _postRepository.GetByExpressionAsync(x => x.Id == postId);
    }

    public async Task<Post?> GetByIdDetailedAsync(string postId, CancellationToken cancellationToken)
    {
        return await _postRepository.GetByIdDetailedAsync(postId,cancellationToken);
    }

    public async Task<PaginationResult<ReadPostDTO>> GetAllByUserIdAsPaginatedAsync(string userId,int pageSize, int pageNumber)
    {
        User? existedUser = await CheckIfUserIsExistedAndReturn(userId);

        if (existedUser is null)
            throw new EntityNullException($"{userId} ID sahip kullanıcı bulunamadı.");

        return await _postRepository
            .GetAll()
            .Where(x => x.UserId == userId)
            .Include(x => x.PostPhotos)
        .Select(x => _mapper.Map<ReadPostDTO>(x))
        .ToPagedListAsync(pageSize, pageNumber);
    }
}

