using AutoMapper;

using SocialFilm.Application.Features.AuthFeatures.Commands.Register;
using SocialFilm.Application.Features.CommentFeatures.Commands.CreateComment;
using SocialFilm.Application.Features.FilmFeatures.Commands;
using SocialFilm.Application.Features.PostFeatures.Commands.CreatePost;
using SocialFilm.Domain.DTOs;
using SocialFilm.Domain.Entities;

namespace SocialFilm.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<RegisterUserCommand, User>();
            CreateMap<CreatePostCommand, Post>();
            CreateMap<CreateCommentCommand, Comment>();

            CreateMap<SaveFilmCommand, SavedFilm>();
            CreateMap<SavedFilm, ReadSavedFilmDto>();
            CreateMap<FilmDetail, ReadFilmDetailDTO>();
        }
    }
}
