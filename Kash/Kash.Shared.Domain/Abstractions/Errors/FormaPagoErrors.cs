using Kash.Shared.Domain.Abstractions.Results;

namespace Kash.Domain.Errors;

public static class FormaPagoErrors
{
    public static Error NombreDuplicado(string nombre) => Error.Conflict(
        $"Ya existe una forma de pago con el nombre '{nombre}'."
    );
}
