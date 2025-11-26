using AhorroLand.Domain;
using AhorroLand.Shared.Application.Abstractions.Messaging.Abstracts.Queries;
using AhorroLand.Shared.Application.Abstractions.Servicies;
using AhorroLand.Shared.Application.Dtos;
using AhorroLand.Shared.Domain.Interfaces.Repositories;
using AhorroLand.Shared.Domain.ValueObjects.Ids;

namespace AhorroLand.Application.Features.Cuentas.Queries;

/// <summary>
/// Maneja la creación de una nueva entidad Cuenta.
/// </summary>
public sealed class GetCuentaByIdQueryHandler
    : GetByIdQueryHandler<Cuenta, CuentaId, CuentaDto, GetCuentaByIdQuery>
{
    public GetCuentaByIdQueryHandler(
        ICacheService cacheService,
        IReadRepositoryWithDto<Cuenta, CuentaDto, CuentaId> readOnlyRepository
        )
        : base(readOnlyRepository, cacheService)
    {
    }
}