namespace SocialFilm.Domain.Common;

public abstract class BaseEntity : IEntity
{
    public BaseEntity()
    {
        Id = Guid.NewGuid().ToString();
    }
    public string Id { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}