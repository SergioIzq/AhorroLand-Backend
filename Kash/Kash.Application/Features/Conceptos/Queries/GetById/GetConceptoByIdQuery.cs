using Kash.Domain;
using Kash.Shared.Application.Abstractions.Messaging.Abstracts.Queries;
using Kash.Shared.Application.Dtos;
using Kash.Shared.Domain.ValueObjects.Ids;

namespace Kash.Application.Features.Conceptos.Queries;

/// <summary>
/// Representa la solicitud para crear un nuevo Concepto.
/// </summary>
// Hereda de AbsCreateCommand<Entidad, DTO de Respuesta>
public sealed record GetConceptoByIdQuery(Guid Id) : AbsGetByIdQuery<Concepto, ConceptoId, ConceptoDto>(Id)
{
}