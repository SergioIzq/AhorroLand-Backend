using AhorroLand.Domain;
using AhorroLand.Shared.Application.Abstractions.Messaging.Abstracts.Commands;
using AhorroLand.Shared.Domain.ValueObjects.Ids;

namespace AhorroLand.Application.Features.FormasPago.Commands;

/// <summary>
/// Representa la solicitud para eliminar una FormaPago por su identificador.
/// </summary>
// Hereda de AbsDeleteCommand<Entidad>
public sealed record DeleteFormaPagoCommand(Guid Id)
    : AbsDeleteCommand<FormaPago, FormaPagoId>(Id);