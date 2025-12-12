using AhorroLand.Domain;
using AhorroLand.Shared.Application.Abstractions.Messaging.Abstracts.Queries;
using AhorroLand.Shared.Application.Abstractions.Servicies;
using AhorroLand.Shared.Application.Dtos;
using AhorroLand.Shared.Domain.Interfaces.Repositories;
using AhorroLand.Shared.Domain.ValueObjects.Ids;

namespace AhorroLand.Application.Features.IngresosProgramados.Queries;

public sealed class GetIngresoProgramadoByIdQueryHandler
    : GetByIdQueryHandler<IngresoProgramado, IngresoProgramadoId, IngresoProgramadoDto, GetIngresoProgramadoByIdQuery>
{
    public GetIngresoProgramadoByIdQueryHandler(
  IReadRepositoryWithDto<IngresoProgramado, IngresoProgramadoDto, IngresoProgramadoId> repository,
    ICacheService cacheService)
        : base(repository, cacheService)
    {
    }
}
