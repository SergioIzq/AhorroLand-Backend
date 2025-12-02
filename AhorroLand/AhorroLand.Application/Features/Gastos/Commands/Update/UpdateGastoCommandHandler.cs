using AhorroLand.Domain;
using AhorroLand.Shared.Application.Abstractions.Messaging.Abstracts.Commands;
using AhorroLand.Shared.Application.Abstractions.Servicies;
using AhorroLand.Shared.Application.Dtos;
using AhorroLand.Shared.Domain.Interfaces;
using AhorroLand.Shared.Domain.Interfaces.Repositories;
using AhorroLand.Shared.Domain.ValueObjects;
using AhorroLand.Shared.Domain.ValueObjects.Ids;

namespace AhorroLand.Application.Features.Gastos.Commands;

/// <summary>
/// Maneja la creación de una nueva entidad Gasto.
/// </summary>
public sealed class UpdateGastoCommandHandler
    : AbsUpdateCommandHandler<Gasto, GastoId, GastoDto, UpdateGastoCommand>
{
    private readonly IDomainValidator _validator;
    public UpdateGastoCommandHandler(
        IUnitOfWork unitOfWork,
        IWriteRepository<Gasto, GastoId> writeRepository,
        ICacheService cacheService,
        IDomainValidator validator
        )
        : base(unitOfWork, writeRepository, cacheService)
    {
        _validator = validator;
    }

    protected override void ApplyChanges(Gasto entity, UpdateGastoCommand command)
    {
        var importeVO = new Cantidad(command.Importe);
        var fechaVO = new FechaRegistro(command.Fecha);
        var conceptoIdVO = new ConceptoId(command.ConceptoId);
        var categoriaIdVO = new CategoriaId(command.CategoriaId);
        var proveedorIdVO = new ProveedorId(command.ProveedorId);
        var personaIdVO = new PersonaId(command.PersonaId);
        var cuentaIdVO = new CuentaId(command.CuentaId);
        var formaPagoIdVO = new FormaPagoId(command.FormaPagoId);
        var usuarioIdVO = new UsuarioId(command.UsuarioId);
        var descripcionVO = new Descripcion(command.Descripcion);


        var existenceTasks = new List<Task<bool>>
        {
            _validator.ExistsAsync < Concepto, ConceptoId >(new ConceptoId(command.ConceptoId)),
            _validator.ExistsAsync < Categoria, CategoriaId >(new CategoriaId(command.CategoriaId)),
            _validator.ExistsAsync < Cuenta, CuentaId >(new CuentaId(command.CuentaId)),
            _validator.ExistsAsync < FormaPago, FormaPagoId >(new FormaPagoId(command.FormaPagoId)),
            _validator.ExistsAsync < Proveedor, ProveedorId >(new ProveedorId(command.ProveedorId)),
            _validator.ExistsAsync < Persona, PersonaId >(new PersonaId(command.PersonaId))
        };

        entity.Update(
            importeVO,
            fechaVO,
            conceptoIdVO,
            proveedorIdVO,
            personaIdVO,
            cuentaIdVO,
            formaPagoIdVO,
            usuarioIdVO,
            descripcionVO
        );
    }
}
