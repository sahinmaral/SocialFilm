using AutoMapper;

using SocialFilm.Application.Features.AuthFeatures.Commands.Register;
using SocialFilm.Application.Features.CommentFeatures.Commands.CreateComment;
using SocialFilm.Application.Features.FilmFeatures.Commands;
using SocialFilm.Application.Features.PostFeatures.Commands.CreatePost;
using SocialFilm.Domain.DTOs;
using SocialFilm.Domain.Entities;

namespace SocialFilm.Application.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<RegisterUserCommand, User>();
        CreateMap<CreatePostCommand, Post>();
        CreateMap<CreateCommentCommand, Comment>();

        CreateMap<SaveFilmCommand, SavedFilm>();
        CreateMap<SavedFilm, ReadSavedFilmDTO>();

        CreateMap<Post, ReadPostDTO>()
            .ForMember(dest => dest.Photos, opt => opt.MapFrom(src => src.PostPhotos));
        CreateMap<PostPhoto, ReadPostPhotoDTO>();
        CreateMap<Genre, ReadGenreDTO>();
        CreateMap<FilmDetail, ReadFilmDetailDTO>()
               .ForMember(dest => dest.Genres, opt => opt.MapFrom(src => src.FilmDetailGenres.Select(x => x.Genre)));
        CreateMap<User, ReadUserDTO>();
    }
}
