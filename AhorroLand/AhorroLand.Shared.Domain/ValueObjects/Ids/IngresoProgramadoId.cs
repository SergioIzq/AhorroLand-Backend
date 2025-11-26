using AhorroLand.Shared.Domain.Interfaces;

namespace AhorroLand.Shared.Domain.ValueObjects.Ids;

public readonly record struct IngresoProgramadoId : IGuidValueObject
{
    // Constructor primario sin lógica
    public Guid Value { get; init; }

    // Constructor secundario con validación
    public IngresoProgramadoId(Guid value)
    {
        if (value == Guid.Empty)
            throw new ArgumentException(nameof(value));

        Value = value;
    }
}