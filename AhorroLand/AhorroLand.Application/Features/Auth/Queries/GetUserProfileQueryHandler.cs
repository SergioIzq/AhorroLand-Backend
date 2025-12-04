using AhorroLand.Domain;
using AhorroLand.Shared.Application.Abstractions.Messaging;
using AhorroLand.Shared.Application.Dtos;
using AhorroLand.Shared.Domain.Abstractions.Results;
using AhorroLand.Shared.Domain.Interfaces.Repositories;
using AhorroLand.Shared.Domain.ValueObjects.Ids;

namespace AhorroLand.Application.Features.Auth.Queries
{
    public sealed class GetUserProfileQueryHandler : IQueryHandler<GetUserProfileQuery, UsuarioDto>
    {
        private readonly IReadRepositoryWithDto<Usuario, UsuarioDto, UsuarioId> _usuarioReadRepository;

        public GetUserProfileQueryHandler(IReadRepositoryWithDto<Usuario, UsuarioDto, UsuarioId> usuarioReadRepository)
        {
            _usuarioReadRepository = usuarioReadRepository;
        }

        public async Task<Result<UsuarioDto>> Handle(GetUserProfileQuery request, CancellationToken cancellationToken)
        {
            var usuario = await _usuarioReadRepository.GetReadModelByIdAsync(request.UserId, cancellationToken);

            if (usuario is null)
            {
                return Result.Failure<UsuarioDto>(Error.NotFound("Usuario no encontrado"));
            }

            return Result.Success(usuario);
        }
    }
}
