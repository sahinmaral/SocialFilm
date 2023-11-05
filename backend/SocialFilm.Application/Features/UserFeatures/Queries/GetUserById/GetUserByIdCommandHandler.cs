using AutoMapper;

using MediatR;

using SocialFilm.Application.Services;
using SocialFilm.Domain.DTOs;
using SocialFilm.Domain.Entities;
using SocialFilm.Domain.Exceptions;

namespace SocialFilm.Application.Features.UserFeatures.Queries.GetUserById;

public sealed class GetUserByIdCommandHandler : IRequestHandler<GetUserByIdCommand, ReadUserDTO>
{
    private readonly IUserService _userService;
    private readonly IMapper _mapper;

    public GetUserByIdCommandHandler(IUserService userService, IMapper mapper)
    {
        _userService = userService;
        _mapper = mapper;
    }

    public async Task<ReadUserDTO> Handle(GetUserByIdCommand request, CancellationToken cancellationToken)
    {
        User? userEntity = await _userService.GetByIdAsync(request.UserId, cancellationToken);

        if (userEntity is null)
            throw new EntityNullException($"{request.UserId} ID sahip kullanıcı bulunamadı");

        return _mapper.Map<ReadUserDTO>(userEntity);
    }
}
