using AhorroLand.Shared.Domain.Abstractions;
using AhorroLand.Shared.Domain.ValueObjects;
using AhorroLand.Shared.Domain.ValueObjects.Ids;
using System.ComponentModel.DataAnnotations.Schema;

namespace AhorroLand.Domain;

[Table("formas_pago")]
public sealed class FormaPago : AbsEntity<FormaPagoId>
{
    // Constructor privado sin parámetros para EF Core
    private FormaPago() : base(new FormaPagoId(Guid.Empty))
    {
    }

    private FormaPago(FormaPagoId id, Nombre nombre, UsuarioId usuarioId) : base(id)
    {
        Nombre = nombre;
        UsuarioId = usuarioId;
    }

    public Nombre Nombre { get; private set; }
    public UsuarioId UsuarioId { get; private set; }

    public static FormaPago Create(Nombre nombre, UsuarioId usuarioId)
    {
        var formaPago = new FormaPago(new FormaPagoId(Guid.NewGuid()), nombre, usuarioId);

        return formaPago;
    }

    public void Update(Nombre nombre) => Nombre = nombre;
}
