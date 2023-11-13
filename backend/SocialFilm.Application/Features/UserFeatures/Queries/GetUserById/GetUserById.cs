using AutoMapper;

using MediatR;

using SocialFilm.Application.Common;
using SocialFilm.Domain.DTOs;
using SocialFilm.Domain.Entities;
using SocialFilm.Domain.Exceptions;

namespace SocialFilm.Application.Features.UserFeatures.Queries.GetUserById;

public record GetUserByIdCommand(string UserId) : IRequest<ReadUserDTO>;
public sealed class GetUserByIdCommandHandler : IRequestHandler<GetUserByIdCommand, ReadUserDTO>
{
    private readonly IRepositoryManager _repositoryManager;
    private readonly IMapper _mapper;
    public GetUserByIdCommandHandler(IRepositoryManager repositoryManager, IMapper mapper)
    {
        _repositoryManager = repositoryManager;
        _mapper = mapper;
    }
    public async Task<ReadUserDTO> Handle(GetUserByIdCommand request, CancellationToken cancellationToken)
    {
        User? userEntity = await _repositoryManager
            .UserRepository
            .GetByExpressionAsync(x => x.Id == request.UserId, cancellationToken) 
            ?? throw new EntityNullException($"{request.UserId} ID sahip kullanıcı bulunamadı");

        var mappedUser = _mapper.Map<ReadUserDTO>(userEntity);

        return mappedUser;
    }
}
