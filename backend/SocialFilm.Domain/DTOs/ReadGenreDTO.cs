using SocialFilm.Domain.Common;

namespace SocialFilm.Domain.DTOs
{
    public sealed class ReadGenreDTO : BaseDTO
    {
        public string Name { get; set; } = null!;
    }
}
