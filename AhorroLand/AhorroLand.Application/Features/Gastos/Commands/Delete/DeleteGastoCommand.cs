using AhorroLand.Domain;
using AhorroLand.Shared.Application.Abstractions.Messaging.Abstracts.Commands;
using AhorroLand.Shared.Domain.ValueObjects.Ids;

namespace AhorroLand.Application.Features.Gastos.Commands;

/// <summary>
/// Representa la solicitud para eliminar una Gasto por su identificador.
/// </summary>
// Hereda de AbsDeleteCommand<Entidad>
public sealed record DeleteGastoCommand(Guid Id)
    : AbsDeleteCommand<Gasto, GastoId>(Id);