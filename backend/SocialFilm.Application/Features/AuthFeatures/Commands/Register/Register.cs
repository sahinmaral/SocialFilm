using AutoMapper;

using MediatR;

using Microsoft.AspNetCore.Identity;

using SocialFilm.Domain.DTOs;
using SocialFilm.Domain.Entities;

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

public sealed class RegisterCommandHandler : IRequestHandler<RegisterUserCommand, MessageResponse>
{
    private readonly IMapper _mapper;
    private readonly UserManager<User> _userManager;

    public RegisterCommandHandler(IMapper mapper, UserManager<User> userManager)
    {
        _mapper = mapper;
        _userManager = userManager;
    }

    public async Task<MessageResponse> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        var newUser = _mapper.Map<User>(request);

        var result = await _userManager.CreateAsync(newUser, request.Password);
        if (!result.Succeeded)
        {
            string errorMessage = string.Join(", ", result.Errors.Select(error => error.Description));
            throw new Exception(errorMessage);
        }

        await _userManager.AddToRoleAsync(newUser, "User");

        return new MessageResponse("Kullanıcı başarıyla oluşturuldu");
    }
}

