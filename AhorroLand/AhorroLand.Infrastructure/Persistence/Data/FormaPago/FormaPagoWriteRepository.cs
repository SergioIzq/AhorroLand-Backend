using AhorroLand.Domain;
using AhorroLand.Infrastructure.Persistence.Command;
using AhorroLand.Shared.Domain.ValueObjects.Ids;

namespace AhorroLand.Infrastructure.Persistence.Data.FormasPago
{
    public class FormaPagoWriteRepository : AbsWriteRepository<FormaPago, FormaPagoId>, IFormaPagoWriteRepository
    {
        private readonly IFormaPagoReadRepository _readRepository;

        public FormaPagoWriteRepository(
            AhorroLandDbContext context,
            IFormaPagoReadRepository readRepository) : base(context)
        {
            _readRepository = readRepository;
        }

        public override async Task CreateAsync(FormaPago entity, CancellationToken cancellationToken = default)
        {
            var exists = await _readRepository.ExistsWithSameNameAsync(
                entity.Nombre,
                entity.UsuarioId,
                cancellationToken);

            if (exists)
            {
                throw new InvalidOperationException(
                    $"Ya existe una forma de pago con el nombre '{entity.Nombre.Value}' para este usuario.");
            }

            await base.CreateAsync(entity, cancellationToken);
        }

        public override async void Update(FormaPago entity)
        {
            base.Update(entity);

            var exists = await _readRepository.ExistsWithSameNameExceptAsync(
                entity.Nombre,
                entity.UsuarioId,
                entity.Id.Value);

            if (exists)
            {
                throw new InvalidOperationException(
                    $"Ya existe otra forma de pago con el nombre '{entity.Nombre.Value}' para este usuario.");
            }
        }
    }
}
