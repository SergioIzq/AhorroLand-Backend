using AhorroLand.Domain;
using AhorroLand.Shared.Application.Abstractions.Messaging.Abstracts.Queries;
using AhorroLand.Shared.Application.Abstractions.Servicies;
using AhorroLand.Shared.Application.Dtos;
using AhorroLand.Shared.Domain.Interfaces.Repositories;
using AhorroLand.Shared.Domain.ValueObjects.Ids;

namespace AhorroLand.Application.Features.Conceptos.Queries;

/// <summary>
/// Maneja la creación de una nueva entidad Concepto.
/// </summary>
public sealed class GetConceptoByIdQueryHandler
    : GetByIdQueryHandler<Concepto, ConceptoId, ConceptoDto, GetConceptoByIdQuery>
{
    public GetConceptoByIdQueryHandler(
        ICacheService cacheService,
        IReadRepositoryWithDto<Concepto, ConceptoDto, ConceptoId> readOnlyRepository
        )
        : base(readOnlyRepository, cacheService)
    {
    }
}