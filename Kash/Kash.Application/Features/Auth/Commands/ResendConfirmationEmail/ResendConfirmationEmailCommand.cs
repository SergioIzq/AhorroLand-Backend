using Kash.Shared.Application.Abstractions.Messaging;

namespace Kash.Application.Features.Auth.Commands.ResendConfirmationEmail;

public sealed record ResendConfirmationEmailCommand(string Correo) : ICommand;