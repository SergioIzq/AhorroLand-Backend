namespace AhorroLand.Shared.Domain.ValueObjects;

public readonly record struct Cantidad
{
    public decimal Valor { get; }

    public Cantidad(decimal valor)
    {
        // REGLA DE DOMINIO: El saldo no puede ser negativo
        if (valor < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(valor), "La cantidad no puede ser negativo.");
        }

        if (valor != Math.Round(valor, 2))
        {
            throw new InvalidOperationException($"La cantidad tiene más de dos decimales significativos. Valor recibido: {valor}");
        }

        Valor = valor;
    }

    public Cantidad Sumar(Cantidad otro) => new Cantidad(this.Valor + otro.Valor);
    public Cantidad Restar(Cantidad otro) => new Cantidad(this.Valor - otro.Valor);
    public static Cantidad Zero() => new Cantidad(0);
}