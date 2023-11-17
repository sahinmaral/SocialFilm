using SocialFilm.Domain.Enums;

namespace SocialFilm.Domain.RequestFeatures;

public class UserFriendParameters
{
    public string? SearchTerm { get; init; }
    public FriendRequestStatus? Status { get; init; } = FriendRequestStatus.ACCEPTED;
    public PaginationParameters PaginationParameters { get; init; } = new PaginationParameters();
}
