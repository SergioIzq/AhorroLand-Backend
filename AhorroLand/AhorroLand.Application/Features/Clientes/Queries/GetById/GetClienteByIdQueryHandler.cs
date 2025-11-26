using AhorroLand.Domain;
using AhorroLand.Shared.Application.Abstractions.Messaging.Abstracts.Queries;
using AhorroLand.Shared.Application.Abstractions.Servicies;
using AhorroLand.Shared.Application.Dtos;
using AhorroLand.Shared.Domain.Interfaces.Repositories;
using AhorroLand.Shared.Domain.ValueObjects.Ids;

namespace AhorroLand.Application.Features.Clientes.Queries;

/// <summary>
/// Maneja la creación de una nueva entidad Categoria.
/// </summary>
public sealed class GetClienteByIdQueryHandler
    : GetByIdQueryHandler<Cliente, ClienteId, ClienteDto, GetClienteByIdQuery>
{
    public GetClienteByIdQueryHandler(
        ICacheService cacheService,
        IReadRepositoryWithDto<Cliente, ClienteDto, ClienteId> readOnlyRepository
        )
        : base(readOnlyRepository, cacheService)
    {
    }
}