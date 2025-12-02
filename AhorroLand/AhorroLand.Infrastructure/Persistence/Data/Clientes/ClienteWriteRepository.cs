using AhorroLand.Domain;
using AhorroLand.Infrastructure.Persistence.Command;
using AhorroLand.Shared.Domain.ValueObjects.Ids;

namespace AhorroLand.Infrastructure.Persistence.Data.Clientes
{
    public class ClienteWriteRepository : AbsWriteRepository<Cliente, ClienteId>, IClienteWriteRepository
    {
        private readonly IClienteReadRepository _readRepository;

        public ClienteWriteRepository(
            AhorroLandDbContext context,
            IClienteReadRepository readRepository) : base(context)
        {
            _readRepository = readRepository;
        }

        public override async Task CreateAsync(Cliente entity, CancellationToken cancellationToken = default)
        {
            var exists = await _readRepository.ExistsWithSameNameAsync(
                entity.Nombre,
                entity.UsuarioId,
                cancellationToken);

            if (exists)
            {
                throw new InvalidOperationException(
                    $"Ya existe un cliente con el nombre '{entity.Nombre.Value}' para este usuario.");
            }

            await base.CreateAsync(entity, cancellationToken);
        }

        public override async void Update(Cliente entity)
        {
            base.Update(entity);

            var exists = await _readRepository.ExistsWithSameNameExceptAsync(
            entity.Nombre,
            entity.UsuarioId,
            entity.Id.Value);

            if (exists)
            {
                throw new InvalidOperationException(
                    $"Ya existe otro cliente con el nombre '{entity.Nombre.Value}' para este usuario.");
            }
        }
    }
}
