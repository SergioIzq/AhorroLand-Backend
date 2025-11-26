using AhorroLand.Domain;
using AhorroLand.Shared.Application.Abstractions.Messaging.Abstracts.Queries;
using AhorroLand.Shared.Application.Abstractions.Servicies;
using AhorroLand.Shared.Application.Dtos;
using AhorroLand.Shared.Domain.Interfaces.Repositories;
using AhorroLand.Shared.Domain.ValueObjects.Ids;

namespace AhorroLand.Application.Features.Clientes.Queries.Search;

/// <summary>
/// Handler para búsqueda rápida de clientes (autocomplete).
/// </summary>
public sealed class SearchClientesQueryHandler
    : SearchForAutocompleteQueryHandler<Cliente, ClienteDto, SearchClientesQuery, ClienteId>
{
    public SearchClientesQueryHandler(
        IReadRepositoryWithDto<Cliente, ClienteDto, ClienteId> repository,
   ICacheService cacheService)
  : base(repository, cacheService)
    {
    }
}
