namespace SocialFilm.Domain.DTOs;

public sealed class UserProfileStatisticsDTO
{
    public int FriendCount { get; set; }
    public int PostCount { get; set; }
    public int WatchedFilmCount { get; set; }
}
