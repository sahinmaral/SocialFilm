using MediatR;

using SocialFilm.Application.FileStorage;
using SocialFilm.Application.Services;
using SocialFilm.Domain.DTOs;
using SocialFilm.Domain.Entities;
using SocialFilm.Domain.Repositories;

namespace SocialFilm.Application.Features.PostFeatures.Commands.DeletePost
{
    public class DeletePostCommandHandler : IRequestHandler<DeletePostCommand, MessageResponse>
    {
        private readonly ICloudinaryService _cloudinaryService;
        private readonly IPostService _postService;
        private readonly IUnitOfWork _unitOfWork;

        public DeletePostCommandHandler(ICloudinaryService cloudinaryService, IPostService postService, IUnitOfWork unitOfWork)
        {
            _cloudinaryService = cloudinaryService;
            _postService = postService;
            _unitOfWork = unitOfWork;
        }

        public async Task<MessageResponse> Handle(DeletePostCommand request, CancellationToken cancellationToken)
        {
            Post? deletedPost = await _postService.GetByIdDetailedAsync(request.PostId,cancellationToken);
            if (deletedPost == null)
                throw new Exception($"{request.PostId} ID ye sahip gönderi bulunamadı.");

            //TODO: Fotograflardan bazilari eger bulunmazsa bu islemin iptal edilmesi gerekir. Transaction gerekebilir.
            //TODO: Veritabanindan silerken hata alirsak fotograf silme islemlerinin geri alinmasi gerekir. Transaction gerekebilir.

            var deleteTasks = deletedPost.PostPhotos.Select(pf => _cloudinaryService.DeleteImageAsync(pf.PhotoPath, cancellationToken));
            var deletedImageTaskResult = await Task.WhenAll(deleteTasks);

            _postService.Delete(deletedPost);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return new MessageResponse("Basarili");
        }
    }
}
