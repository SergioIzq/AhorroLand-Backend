using AhorroLand.Domain;
using AhorroLand.Shared.Application.Abstractions.Messaging.Abstracts.Queries;
using AhorroLand.Shared.Application.Abstractions.Servicies;
using AhorroLand.Shared.Application.Dtos;
using AhorroLand.Shared.Domain.Interfaces.Repositories;

namespace AhorroLand.Application.Features.Clientes.Queries.Search;

/// <summary>
/// Handler para búsqueda rápida de clientes (autocomplete).
/// </summary>
public sealed class SearchClientesQueryHandler
    : SearchForAutocompleteQueryHandler<Cliente, ClienteDto, SearchClientesQuery>
{
    public SearchClientesQueryHandler(
        IReadRepositoryWithDto<Cliente, ClienteDto> repository,
   ICacheService cacheService)
  : base(repository, cacheService)
    {
    }
}
