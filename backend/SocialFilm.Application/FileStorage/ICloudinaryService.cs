using CloudinaryDotNet.Actions;

using Microsoft.AspNetCore.Http;

namespace SocialFilm.Application.FileStorage;

public interface ICloudinaryService
{
    Task<ImageUploadResult> UploadImageAsync(IFormFile file, CancellationToken cancellationToken);
    Task<DeletionResult?> DeleteImageAsync(string publicId, CancellationToken cancellationToken);
}