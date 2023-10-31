using MediatR;

using SocialFilm.Domain.DTOs;

namespace SocialFilm.Application.Features.AuthFeatures.Commands.Register;

public sealed record RegisterUserCommand
    (
    string FirstName,
    string LastName,
    DateTime BirthDate,
    string Email,
    string Username,
    string Password,
    string? Middlename) : IRequest<MessageResponse>;
