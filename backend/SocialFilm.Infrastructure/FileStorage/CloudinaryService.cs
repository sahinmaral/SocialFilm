using CloudinaryDotNet;
using CloudinaryDotNet.Actions;

using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

using SocialFilm.Application.FileStorage;

namespace SocialFilm.Infrastructure.FileStorage;

public class CloudinaryService : ICloudinaryService
{
    Cloudinary cloudinary;
    public CloudinaryService(IOptions<CloudinaryOptions> cloudinaryOptions)
    {
        CloudinaryOptions cloudinaryOptionsValue = cloudinaryOptions.Value;

        Account account = new Account(
          cloudinaryOptionsValue.CloudName,
          cloudinaryOptionsValue.APIKey,
          cloudinaryOptionsValue.APISecret
        );

        cloudinary = new Cloudinary(account);
    }

    public async Task<ImageUploadResult> UploadImageAsync(IFormFile file, CancellationToken cancellationToken)
    {
        var uploadParams = new ImageUploadParams
        {
            File = new FileDescription(file.FileName, file.OpenReadStream()),
            Folder = "postImages",
            PublicId = Guid.NewGuid().ToString()
        };

        return await cloudinary.UploadAsync(uploadParams);
    }

    public async Task<DeletionResult?> DeleteImageAsync(string publicId, CancellationToken cancellationToken)
    {
        var deletionParams = new DeletionParams(publicId)
        {
            ResourceType = ResourceType.Image
        };

        return await cloudinary.DestroyAsync(deletionParams);
    }
}
