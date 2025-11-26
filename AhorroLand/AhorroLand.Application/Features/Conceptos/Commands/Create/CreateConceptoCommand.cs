using AhorroLand.Domain;
using AhorroLand.Shared.Application.Abstractions.Messaging.Abstracts.Commands;
using AhorroLand.Shared.Application.Dtos;
using AhorroLand.Shared.Domain.ValueObjects.Ids;

namespace AhorroLand.Application.Features.Conceptos.Commands;

/// <summary>
/// Representa la solicitud para crear un nuevo Concepto.
/// </summary>
public sealed record CreateConceptoCommand : AbsCreateCommand<Concepto, ConceptoId>
{
    public required string Nombre { get; init; }
    public required Guid CategoriaId { get; init; }
    public required Guid UsuarioId { get; init; }
}
