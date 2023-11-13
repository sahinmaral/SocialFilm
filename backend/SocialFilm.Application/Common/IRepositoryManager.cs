using SocialFilm.Domain.Repositories;

namespace SocialFilm.Application.Common
{
    public interface IRepositoryManager
    {
        public ISavedFilmRepository SavedFilmRepository { get; }
        public IGenreRepository GenreRepository { get; }
        public ICommentRepository CommentRepository { get; }
        public IFilmDetailRepository FilmDetailRepository { get; }
        public IPostRepository PostRepository { get; }
        public IUserRepository UserRepository { get; }
        int SaveChanges();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
