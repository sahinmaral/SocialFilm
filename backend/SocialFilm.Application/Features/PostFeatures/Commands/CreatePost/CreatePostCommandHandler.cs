using AutoMapper;

using MediatR;

using SocialFilm.Application.FileStorage;
using SocialFilm.Application.Services;
using SocialFilm.Domain.DTOs;
using SocialFilm.Domain.Entities;
using SocialFilm.Domain.Repositories;

namespace SocialFilm.Application.Features.PostFeatures.Commands.CreatePost;

public sealed class CreatePostCommandHandler : IRequestHandler<CreatePostCommand, MessageResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPostService _postService;
    private readonly ICloudinaryService _cloudinaryService;

    private readonly IMapper _mapper;
    public CreatePostCommandHandler(IMapper mapper, IUnitOfWork unitOfWork, IPostService postService, ICloudinaryService cloudinaryService)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _postService = postService;
        _cloudinaryService = cloudinaryService;
    }

    public async Task<MessageResponse> Handle(CreatePostCommand request, CancellationToken cancellationToken)
    {
        if (request.Files.Count == 0 || request.Files.First().Length == 0)
            throw new Exception("Gönderi için herhangi bir resim yüklemediniz");

        //TODO: Eger veritabaninda bir problem olursa yuklenen resimlerin silinmesi gerekir.

        var uploadTasks = request.Files.Select(file => _cloudinaryService.UploadImageAsync(file, cancellationToken));
        var uploadedImageResults = await Task.WhenAll(uploadTasks);

        var uploadedImageUrls = uploadedImageResults.Select(ir => ir.PublicId).ToList();

        Post newPost = _mapper.Map<Post>(request);
        uploadedImageUrls.ForEach((ir) =>
        {
            newPost.PostPhotos.Add(new PostPhoto()
            {
                PhotoPath = ir
            });
        });

        await _postService.AddAsync(newPost, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return new MessageResponse("Basarili");
    }
}