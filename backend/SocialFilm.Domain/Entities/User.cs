using Microsoft.AspNetCore.Identity;

using SocialFilm.Domain.Common;

namespace SocialFilm.Domain.Entities
{
    public class User : IdentityUser<string>, IUserEntity
    {
        public User()
        {
            Id = Guid.NewGuid().ToString();
        }
        public override string Id { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string? MiddleName { get; set; }
        public string LastName { get; set; } = null!;
        public DateTime BirthDate { get; set; }
        public string? ProfilePhotoURL { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpires { get; set; }

        public List<SavedFilm> SavedFilms { get; set; } = new List<SavedFilm>();
        public List<Post> Posts { get; set; } = new List<Post>();
        public List<UserFriend> UserFriends { get; set; } = new List<UserFriend>();
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}