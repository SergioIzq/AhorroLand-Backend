using AhorroLand.Shared.Domain.Abstractions.Results;

namespace AhorroLand.Domain.Errors;

public static class ProveedorErrors
{
    public static Error NombreDuplicado(string nombre) => Error.Conflict(
        $"Ya existe un proveedor con el nombre '{nombre}'."
    );
}
