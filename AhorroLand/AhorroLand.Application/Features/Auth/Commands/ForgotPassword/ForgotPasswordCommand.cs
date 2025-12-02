using AhorroLand.Shared.Application.Abstractions.Messaging;

namespace AhorroLand.Application.Features.Auth.Commands.ForgotPassword;

public record ForgotPasswordCommand(string Email) : ICommand;