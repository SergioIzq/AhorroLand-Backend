using AhorroLand.Domain;
using AhorroLand.Shared.Application.Abstractions.Messaging.Abstracts.Commands;
using AhorroLand.Shared.Domain.ValueObjects.Ids;

namespace AhorroLand.Application.Features.GastosProgramados.Commands;

public sealed record DeleteGastoProgramadoCommand(Guid Id) : AbsDeleteCommand<GastoProgramado, GastoProgramadoId>(Id);
