using AhorroLand.Domain;
using AhorroLand.Shared.Application.Abstractions.Messaging.Abstracts.Queries;
using AhorroLand.Shared.Application.Abstractions.Servicies;
using AhorroLand.Shared.Application.Dtos;
using AhorroLand.Shared.Domain.Interfaces.Repositories;
using AhorroLand.Shared.Domain.ValueObjects.Ids;

namespace AhorroLand.Application.Features.Gastos.Queries;

/// <summary>
/// Maneja la creación de una nueva entidad Gasto.
/// </summary>
public sealed class GetGastoByIdQueryHandler
    : GetByIdQueryHandler<Gasto, GastoId, GastoDto, GetGastoByIdQuery>
{
    public GetGastoByIdQueryHandler(
        ICacheService cacheService,
        IReadRepositoryWithDto<Gasto, GastoDto, GastoId> readOnlyRepository
        )
        : base(readOnlyRepository, cacheService)
    {
    }
}