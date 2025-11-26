using AhorroLand.Domain;
using AhorroLand.Shared.Application.Abstractions.Messaging.Abstracts.Commands;
using AhorroLand.Shared.Domain.ValueObjects.Ids;

namespace AhorroLand.Application.Features.TraspasosProgramados.Commands;

public sealed record DeleteTraspasoProgramadoCommand(Guid Id) : AbsDeleteCommand<TraspasoProgramado, TraspasoProgramadoId>(Id);
