using AhorroLand.Domain;
using AhorroLand.Shared.Application.Abstractions.Messaging.Abstracts.Queries;
using AhorroLand.Shared.Application.Abstractions.Servicies;
using AhorroLand.Shared.Application.Dtos;
using AhorroLand.Shared.Domain.Interfaces.Repositories;
using AhorroLand.Shared.Domain.ValueObjects.Ids;

namespace AhorroLand.Application.Features.IngresosProgramados.Queries;

public sealed class GetIngresosProgramadosPagedListQueryHandler
    : GetPagedListQueryHandler<IngresoProgramado, IngresoProgramadoId, IngresoProgramadoDto, GetIngresosProgramadosPagedListQuery>
{
    public GetIngresosProgramadosPagedListQueryHandler(
        IReadRepositoryWithDto<IngresoProgramado, IngresoProgramadoDto, IngresoProgramadoId> repository,
     ICacheService cacheService)
   : base(repository, cacheService)
    {
    }
}
