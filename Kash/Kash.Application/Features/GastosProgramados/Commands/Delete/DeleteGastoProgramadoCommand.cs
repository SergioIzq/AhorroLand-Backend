using Kash.Domain;
using Kash.Shared.Application.Abstractions.Messaging.Abstracts.Commands;
using Kash.Shared.Domain.ValueObjects.Ids;

namespace Kash.Application.Features.GastosProgramados.Commands;

public sealed record DeleteGastoProgramadoCommand(Guid Id) : AbsDeleteCommand<GastoProgramado, GastoProgramadoId>(Id);
