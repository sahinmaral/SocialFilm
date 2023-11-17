using AutoMapper;

using SocialFilm.Application.Features.AuthFeatures.Commands.Register;
using SocialFilm.Application.Features.CommentFeatures.Commands.CreateComment;
using SocialFilm.Application.Features.FilmFeatures.Commands.SaveFilm;
using SocialFilm.Application.Features.PostFeatures.Commands.CreatePost;
using SocialFilm.Application.Models;
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

        CreateMap<Comment, ReadSubCommentDTO>();
        CreateMap<Comment, ReadCommentDTO>()
            .ForMember(dest => dest.SubComments, opt => opt.Ignore());

        CreateMap<Post,ReadPostDetailedDTO>()
            .ForMember(dest => dest.Photos, opt => opt.MapFrom(src => src.PostPhotos));
        CreateMap<Post, ReadPostDTO>()
            .ForMember(dest => dest.Photos, opt => opt.MapFrom(src => src.PostPhotos));
        CreateMap<PostPhoto, ReadPostPhotoDTO>();
        CreateMap<Genre, ReadGenreDTO>();
        CreateMap<FilmDetail, ReadFilmDetailDTO>()
               .ForMember(dest => dest.Genres, opt => opt.MapFrom(src => src.FilmDetailGenres.Select(x => x.Genre)));
        CreateMap<User, ReadUserDTO>();

        CreateMap<FilmBaseResponseModel, ReadFilmDetailDTO>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id.ToString()))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Title))
            .ForMember(dest => dest.PosterPath, opt => opt.MapFrom(src => src.Poster_path))
            .ForMember(dest => dest.BackdropPath, opt => opt.MapFrom(src => src.Backdrop_path))
            .ForMember(dest => dest.ReleaseYear, opt => opt.MapFrom(src => src.Release_Date));
    }
}
