using AhorroLand.Domain;
using AhorroLand.Infrastructure.Persistence.Command;
using AhorroLand.Shared.Domain.ValueObjects.Ids;

namespace AhorroLand.Infrastructure.Persistence.Data.Proveedores
{

    // Nota: Asegúrate de que IProveedorWriteRepository herede de IWriteRepository<Proveedor>
    public class ProveedorWriteRepository : AbsWriteRepository<Proveedor, ProveedorId>, IProveedorWriteRepository
    {
        private readonly IProveedorReadRepository _readRepository;

        public ProveedorWriteRepository(
      AhorroLandDbContext context,
     IProveedorReadRepository readRepository) : base(context)
        {
            _readRepository = readRepository;
        }

        public override async Task CreateAsync(Proveedor entity, CancellationToken cancellationToken = default)
        {
            var exists = await _readRepository.ExistsWithSameNameAsync(
                          entity.Nombre,
                        entity.UsuarioId,
                         cancellationToken);

            if (exists)
            {
                throw new InvalidOperationException(
                 $"Ya existe un proveedor con el nombre '{entity.Nombre.Value}' para este usuario.");
            }

            await base.CreateAsync(entity, cancellationToken);
        }

        public async override void Update(Proveedor entity)
        {
            base.Update(entity);

            var exists = await _readRepository.ExistsWithSameNameExceptAsync(
                        entity.Nombre,
                      entity.UsuarioId,
                        entity.Id.Value);

            if (exists)
            {
                throw new InvalidOperationException(
              $"Ya existe otro proveedor con el nombre '{entity.Nombre.Value}' para este usuario.");
            }
        }
    }
}
