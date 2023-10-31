using Newtonsoft.Json;

namespace SocialFilm.Presentation.Middlewares;

public sealed class ErrorResult : ErrorStatusCode
{
    [JsonProperty("message")]
    public string Message { get; set; } = null!;
}


public class ErrorStatusCode
{
    [JsonProperty("statusCode")]
    public int StatusCode { get; set; }
    public override string ToString()
    {
        return JsonConvert.SerializeObject(this);
    }
}

public sealed class ValidationErrorDetails : ErrorStatusCode
{
    public IEnumerable<string> Errors { get; set; } = new List<string>();
}
