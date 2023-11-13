using AutoMapper;

using MediatR;

using Microsoft.AspNetCore.Http;

using SocialFilm.Application.Common;
using SocialFilm.Application.FileStorage;
using SocialFilm.Domain.DTOs;
using SocialFilm.Domain.Entities;
using SocialFilm.Domain.Repositories;

namespace SocialFilm.Application.Features.PostFeatures.Commands.CreatePost;

public sealed record CreatePostCommand(string FilmId, string UserId, string Content, List<IFormFile> Files) : IRequest<MessageResponse>;
public sealed class CreatePostCommandHandler : IRequestHandler<CreatePostCommand, MessageResponse>
{
    private readonly ICloudinaryService _cloudinaryService;
    private readonly IRepositoryManager _repositoryManager;
    private readonly IMapper _mapper;
    public CreatePostCommandHandler(IMapper mapper,ICloudinaryService cloudinaryService, IRepositoryManager repositoryManager)
    {
        _mapper = mapper;
        _cloudinaryService = cloudinaryService;
        _repositoryManager = repositoryManager;
    }

    public async Task<MessageResponse> Handle(CreatePostCommand request, CancellationToken cancellationToken)
    {
        if (request.Files.Count == 0 || request.Files.First().Length == 0)
            throw new Exception("Gönderi için herhangi bir resim yüklemediniz");

        //TODO: Eger veritabaninda bir problem olursa yuklenen resimlerin silinmesi gerekir.

        var uploadTasks = request.Files.Select(file => _cloudinaryService.UploadImageAsync(file, cancellationToken));
        var uploadedImageResults = await Task.WhenAll(uploadTasks);

        var uploadedImageUrls = uploadedImageResults.Select(ir => $"{ir.FullyQualifiedPublicId}.{ir.Format}").ToList();

        Post newPost = _mapper.Map<Post>(request);
        uploadedImageUrls.ForEach((ir) =>
        {
            newPost.PostPhotos.Add(new PostPhoto()
            {
                PhotoPath = ir
            });
        });

        await _repositoryManager.PostRepository.AddAsync(newPost, cancellationToken);
        await _repositoryManager.SaveChangesAsync(cancellationToken);

        return new MessageResponse("Basarili");
    }
}