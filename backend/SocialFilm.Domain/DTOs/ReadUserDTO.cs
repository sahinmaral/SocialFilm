using SocialFilm.Domain.Common;

namespace SocialFilm.Domain.DTOs;

public class ReadUserDTO : BaseDTO
{
    public string FirstName { get; set; } = null!;
    public string MiddleName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public DateTime BirthDate { get; set; }
    public string ProfilePhotoURL { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string UserName { get; set; } = null!;
}
