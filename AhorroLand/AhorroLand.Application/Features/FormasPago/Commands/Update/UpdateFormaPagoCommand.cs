using AhorroLand.Domain;
using AhorroLand.Shared.Application.Abstractions.Messaging.Abstracts.Commands;
using AhorroLand.Shared.Application.Dtos;
using AhorroLand.Shared.Domain.ValueObjects.Ids;

namespace AhorroLand.Application.Features.FormasPago.Commands;

/// <summary>
/// Representa la solicitud para actualizar una nueva cuenta.
/// </summary>
// Hereda de AbsUpadteCommand<Entidad, DTO de Respuesta>
public sealed record UpdateFormaPagoCommand : AbsUpdateCommand<FormaPago, FormaPagoId, FormaPagoDto>
{
    /// <summary>
    /// Nombre de la nueva cuenta.
    /// </summary>
    public required string Nombre { get; init; }
}