using AhorroLand.Domain;
using AhorroLand.Infrastructure.Persistence.Command;
using AhorroLand.Shared.Domain.ValueObjects.Ids;

namespace AhorroLand.Infrastructure.Persistence.Data.Cuentas
{
    public class CuentaWriteRepository : AbsWriteRepository<Cuenta, CuentaId>, ICuentaWriteRepository
    {
        private readonly ICuentaReadRepository _readRepository;

        public CuentaWriteRepository(
            AhorroLandDbContext context,
            ICuentaReadRepository readRepository) : base(context)
        {
            _readRepository = readRepository;
        }

        public override async Task CreateAsync(Cuenta entity, CancellationToken cancellationToken = default)
        {
            var exists = await _readRepository.ExistsWithSameNameAsync(
                entity.Nombre,
                entity.UsuarioId,
                cancellationToken);

            if (exists)
            {
                throw new InvalidOperationException(
                    $"Ya existe una cuenta con el nombre '{entity.Nombre.Value}' para este usuario.");
            }

            await base.CreateAsync(entity, cancellationToken);
        }

        public override async void Update(Cuenta entity)
        {
            // Primero verificar que la entidad existe (validación del base)
            base.Update(entity);

            // Luego validar duplicados
            var exists = await _readRepository.ExistsWithSameNameExceptAsync(
                entity.Nombre,
                entity.UsuarioId,
                entity.Id.Value);

            if (exists)
            {
                throw new InvalidOperationException(
                    $"Ya existe otra cuenta con el nombre '{entity.Nombre.Value}' para este usuario.");
            }
        }
    }
}
