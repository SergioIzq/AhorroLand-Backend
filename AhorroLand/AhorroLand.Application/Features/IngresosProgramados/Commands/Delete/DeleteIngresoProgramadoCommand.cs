using AhorroLand.Domain;
using AhorroLand.Shared.Application.Abstractions.Messaging.Abstracts.Commands;
using AhorroLand.Shared.Domain.ValueObjects.Ids;

namespace AhorroLand.Application.Features.IngresosProgramados.Commands;

public sealed record DeleteIngresoProgramadoCommand(Guid Id) : AbsDeleteCommand<IngresoProgramado, IngresoProgramadoId>(Id);
