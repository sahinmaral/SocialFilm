using Microsoft.EntityFrameworkCore;

using SocialFilm.Application.Common;
using SocialFilm.Domain.Repositories;
using SocialFilm.Persistance.Context;

namespace SocialFilm.Persistance.Repositories
{
    public class RepositoryManager : IRepositoryManager
    {
        private readonly AppDbContext _context;
        private readonly ISavedFilmRepository _savedFilmRepository;
        private readonly IGenreRepository _genreRepository;
        private readonly ICommentRepository _commentRepository;
        private readonly IFilmDetailRepository _filmDetailRepository;
        private readonly IPostRepository _postRepository;
        private readonly IUserRepository _userRepository;

        public RepositoryManager(AppDbContext context,
            ISavedFilmRepository savedFilmRepository,
            IGenreRepository genreRepository,
            ICommentRepository commentRepository,
            IFilmDetailRepository filmDetailRepository,
            IPostRepository postRepository,
            IUserRepository userRepository)
        {
            _context = context;
            _savedFilmRepository = savedFilmRepository;
            _genreRepository = genreRepository;
            _commentRepository = commentRepository;
            _filmDetailRepository = filmDetailRepository;
            _postRepository = postRepository;
            _userRepository = userRepository;
        }

        public ISavedFilmRepository SavedFilmRepository => _savedFilmRepository;
        public IGenreRepository GenreRepository => _genreRepository;
        public ICommentRepository CommentRepository => _commentRepository;
        public IFilmDetailRepository FilmDetailRepository => _filmDetailRepository;
        public IPostRepository PostRepository => _postRepository;
        public IUserRepository UserRepository => _userRepository;

        public int SaveChanges()
        {
            return _context.SaveChanges();
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            return await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
