using AhorroLand.Shared.Domain.Abstractions.Results;

namespace AhorroLand.Shared.Domain.ValueObjects;

public readonly record struct Apellido
{
    public const int MaxLength = 150;
    public string Value { get; init; }

    public Apellido(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            Result.Failure<Apellido>(Error.Validation("El apellido es obligatorio."));
        }

        var trimmedValue = value.Trim();

        if (trimmedValue.Length > MaxLength)
        {
            Result.Failure<Apellido>(Error.Validation($"El apellido no puede exceder los {MaxLength} caracteres."));
        }

        Value = trimmedValue;
    }
    public static Apellido CreateFromDatabase(string value) => new Apellido(value);
}