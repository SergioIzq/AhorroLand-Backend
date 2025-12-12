using AhorroLand.Shared.Domain.Abstractions.Results;

namespace AhorroLand.Domain.Errors;

public static class FormaPagoErrors
{
    public static Error NombreDuplicado(string nombre) => Error.Conflict(
        $"Ya existe una forma de pago con el nombre '{nombre}'."
    );
}
