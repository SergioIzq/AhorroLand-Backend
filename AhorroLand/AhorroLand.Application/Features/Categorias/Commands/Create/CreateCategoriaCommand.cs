using AhorroLand.Domain;
using AhorroLand.Shared.Application.Abstractions.Messaging.Abstracts.Commands;
using AhorroLand.Shared.Application.Dtos;
using AhorroLand.Shared.Domain.ValueObjects.Ids;

namespace AhorroLand.Application.Features.Categorias.Commands;

/// <summary>
/// Representa la solicitud para crear una nueva Categoría.
/// </summary>
// Hereda de AbsCreateCommand<Entidad, DTO de Respuesta>
public sealed record CreateCategoriaCommand : AbsCreateCommand<Categoria, CategoriaId>
{
    /// <summary>
    /// Nombre de la nueva categoría.
    /// </summary>
    public required string Nombre { get; init; }
    public required Guid UsuarioId { get; init; }

    /// <summary>
    /// Descripción opcional de la categoría.
    /// </summary>
    public string? Descripcion { get; init; }
}