using AhorroLand.Domain;
using AhorroLand.Shared.Application.Abstractions.Messaging.Abstracts.Queries;
using AhorroLand.Shared.Application.Abstractions.Servicies;
using AhorroLand.Shared.Application.Dtos;
using AhorroLand.Shared.Domain.Interfaces.Repositories;

namespace AhorroLand.Application.Features.Clientes.Queries.Recent;

public sealed class GetRecentClientesQueryHandler
  : GetRecentQueryHandler<Cliente, ClienteDto, GetRecentClientesQuery>
{
    public GetRecentClientesQueryHandler(
        IReadRepositoryWithDto<Cliente, ClienteDto> repository,
        ICacheService cacheService)
        : base(repository, cacheService)
    {
    }
}
