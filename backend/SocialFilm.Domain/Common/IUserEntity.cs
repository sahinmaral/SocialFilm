using SocialFilm.Domain.Entities;

namespace SocialFilm.Domain.Common;

public interface IUserEntity : IEntity
{
    string FirstName { get; set; }
    string? MiddleName { get; set; }
    string LastName { get; set; }
    DateTime BirthDate { get; set; }
    string? ProfilePhotoURL { get; set; }
    string? RefreshToken { get; set; }
    DateTime? RefreshTokenExpires { get; set; }

    List<SavedFilm> SavedFilms { get; set; }
    List<Post> Posts { get; set; }
    List<Comment> Comments { get; set; }
    List<UserFriend> UserFriends { get; set; }
}
