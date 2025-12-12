using Kash.Domain;
using Kash.Shared.Application.Abstractions.Messaging.Abstracts.Commands;
using Kash.Shared.Domain.ValueObjects.Ids;

namespace Kash.Application.Features.TraspasosProgramados.Commands;

public sealed record DeleteTraspasoProgramadoCommand(Guid Id) : AbsDeleteCommand<TraspasoProgramado, TraspasoProgramadoId>(Id);
