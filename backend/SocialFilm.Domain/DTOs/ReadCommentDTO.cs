using SocialFilm.Domain.Common;

namespace SocialFilm.Domain.DTOs;

public class ReadCommentDTO : BaseDTO
{
    public ReadUserDTO User { get; set; } = null!;
    public string Message { get; set; } = null!;
    public PaginationResult<ReadSubCommentDTO> SubComments { get; set; } = null!;
}

public class ReadSubCommentDTO : BaseDTO
{
    public ReadUserDTO User { get; set; } = null!;
    public string Message { get; set; } = null!;
}
