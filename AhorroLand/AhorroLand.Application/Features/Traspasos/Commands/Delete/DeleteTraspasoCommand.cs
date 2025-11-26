using AhorroLand.Domain;
using AhorroLand.Shared.Application.Abstractions.Messaging.Abstracts.Commands;
using AhorroLand.Shared.Domain.ValueObjects.Ids;

namespace AhorroLand.Application.Features.Traspasos.Commands;

public sealed record DeleteTraspasoCommand(Guid Id) : AbsDeleteCommand<Traspaso, TraspasoId>(Id);
