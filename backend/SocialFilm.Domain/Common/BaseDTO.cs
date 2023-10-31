namespace SocialFilm.Domain.Common
{
    public abstract class BaseDTO
    {
        public BaseDTO()
        {
            Id = Guid.NewGuid().ToString();
        }
        public string Id { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}