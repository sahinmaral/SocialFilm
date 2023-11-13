using MediatR;

using SocialFilm.Application.Common;
using SocialFilm.Application.FileStorage;
using SocialFilm.Domain.DTOs;
using SocialFilm.Domain.Entities;

namespace SocialFilm.Application.Features.PostFeatures.Commands.DeletePost
{

    public sealed record DeletePostCommand(string PostId) : IRequest<MessageResponse>;
    public class DeletePostCommandHandler : IRequestHandler<DeletePostCommand, MessageResponse>
    {
        private readonly ICloudinaryService _cloudinaryService;
        private readonly IRepositoryManager _repositoryManager;
        public DeletePostCommandHandler(ICloudinaryService cloudinaryService, IRepositoryManager repositoryManager)
        {
            _cloudinaryService = cloudinaryService;
            _repositoryManager = repositoryManager;
        }

        public async Task<MessageResponse> Handle(DeletePostCommand request, CancellationToken cancellationToken)
        {
            Post? deletedPost = await _repositoryManager
                .PostRepository
                .GetByIdDetailedAsync(request.PostId, cancellationToken) 
                ?? throw new Exception($"{request.PostId} ID ye sahip gönderi bulunamadı.");

            //TODO: Fotograflardan bazilari eger bulunmazsa bu islemin iptal edilmesi gerekir. Transaction gerekebilir.
            //TODO: Veritabanindan silerken hata alirsak fotograf silme islemlerinin geri alinmasi gerekir. Transaction gerekebilir.

            var deleteTasks = deletedPost.PostPhotos.Select(pf => _cloudinaryService.DeleteImageAsync(pf.PhotoPath, cancellationToken));
            var deletedImageTaskResult = await Task.WhenAll(deleteTasks);

            _repositoryManager.PostRepository.Delete(deletedPost);
            await _repositoryManager.SaveChangesAsync(cancellationToken);

            return new MessageResponse("Basarili");
        }
    }
}
