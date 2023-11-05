using SocialFilm.Domain.Enums;

namespace SocialFilm.Domain.Entities;

public class UserFriend
{
    public string UserId { get; set; } = null!;
    public User User { get; set; } = new User();

    public string FriendId { get; set; } = null!;
    public User Friend { get; set; } = new User();

    public FriendRequestStatus Status { get; set; }
}