using AhorroLand.Shared.Domain.Abstractions.Results;

namespace AhorroLand.Domain.Errors;

public static class CuentaErrors
{
    public static Error NombreDuplicado(string nombre) => Error.Conflict(
        $"Ya existe una cuenta con el nombre '{nombre}'."
    );
}
