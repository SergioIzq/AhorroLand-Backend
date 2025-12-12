using Kash.Shared.Domain.Abstractions.Results;

namespace Kash.Shared.Domain.Abstractions.Errors;

public static class SystemErrors
{
    public static readonly Error InternalServerError = new(
        "System.InternalServerError",
        "Error del Servidor",
        "Ocurrió un error inesperado. Por favor, contacte al soporte si el problema persiste."
    );
}