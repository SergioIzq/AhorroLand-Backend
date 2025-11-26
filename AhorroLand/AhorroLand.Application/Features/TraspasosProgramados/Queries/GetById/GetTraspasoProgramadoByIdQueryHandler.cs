using AhorroLand.Domain;
using AhorroLand.Shared.Application.Abstractions.Messaging.Abstracts.Queries;
using AhorroLand.Shared.Application.Abstractions.Servicies;
using AhorroLand.Shared.Application.Dtos;
using AhorroLand.Shared.Domain.Interfaces.Repositories;
using AhorroLand.Shared.Domain.ValueObjects.Ids;

namespace AhorroLand.Application.Features.TraspasosProgramados.Queries;

public sealed class GetTraspasoProgramadoByIdQueryHandler
    : GetByIdQueryHandler<TraspasoProgramado, TraspasoProgramadoId, TraspasoProgramadoDto, GetTraspasoProgramadoByIdQuery>
{
    public GetTraspasoProgramadoByIdQueryHandler(
        ICacheService cacheService,
        IReadRepositoryWithDto<TraspasoProgramado, TraspasoProgramadoDto, TraspasoProgramadoId> readOnlyRepository
    )
    : base(readOnlyRepository, cacheService)
    {
    }
}
