using Microsoft.AspNetCore.Identity;

using SocialFilm.Application.Services;
using SocialFilm.Domain.Entities;
using SocialFilm.Domain.Exceptions;
using SocialFilm.Domain.Repositories;

namespace SocialFilm.Persistance.Services;

public sealed class PostService : IPostService
{
    private readonly IPostRepository _postRepository;
    private readonly IFilmDetailRepository _filmDetailRepository;
    private readonly UserManager<User> _userManager;
    public PostService(IPostRepository postRepository, IFilmDetailRepository filmDetailRepository, UserManager<User> userManager)
    {
        _postRepository = postRepository;
        _filmDetailRepository = filmDetailRepository;
        _userManager = userManager;
    }

    private async Task<FilmDetail?> CheckIfFilmDetailIsExistedAndReturn(string filmDetailId)
    {
        return await _filmDetailRepository.GetByExpressionAsync(x => x.Id == filmDetailId);
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
        FilmDetail? existedFilmDetail = await CheckIfFilmDetailIsExistedAndReturn(post.FilmId);

        if (existedFilmDetail == null)
            throw new EntityNullException($"{post.FilmId} ID sahip film bulunamadı.");

        User? existedUser = await CheckIfUserIsExistedAndReturn(post.UserId);

        if (existedUser == null)
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
}

