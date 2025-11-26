using AhorroLand.Domain;
using AhorroLand.Shared.Application.Abstractions.Messaging.Abstracts.Queries;
using AhorroLand.Shared.Application.Abstractions.Servicies;
using AhorroLand.Shared.Application.Dtos;
using AhorroLand.Shared.Domain.Interfaces.Repositories;
using AhorroLand.Shared.Domain.ValueObjects.Ids;

namespace AhorroLand.Application.Features.Traspasos.Queries;

public sealed class GetTraspasoByIdQueryHandler
    : GetByIdQueryHandler<Traspaso, TraspasoId, TraspasoDto, GetTraspasoByIdQuery>
{
    public GetTraspasoByIdQueryHandler(
     ICacheService cacheService,
        IReadRepositoryWithDto<Traspaso, TraspasoDto, TraspasoId> readOnlyRepository
      )
        : base(readOnlyRepository, cacheService)
    {
    }
}
