using Kash.Domain;
using Kash.Infrastructure.Persistence.Command;
using Kash.Shared.Domain.ValueObjects.Ids;

namespace Kash.Infrastructure.Persistence.Data.Personas
{
    public class PersonaWriteRepository : AbsWriteRepository<Persona, PersonaId>, IPersonaWriteRepository
    {
        private readonly IPersonaReadRepository _readRepository;

        public PersonaWriteRepository(
            KashDbContext context,
            IPersonaReadRepository readRepository) : base(context)
        {
            _readRepository = readRepository;
        }

        public override async Task CreateAsync(Persona entity, CancellationToken cancellationToken = default)
        {
            var exists = await _readRepository.ExistsWithSameNameAsync(
                entity.Nombre,
                entity.UsuarioId,
                cancellationToken);

            if (exists)
            {
                throw new InvalidOperationException(
                    $"Ya existe una persona con el nombre '{entity.Nombre.Value}' para este usuario.");
            }

            await base.CreateAsync(entity, cancellationToken);
        }

        public override async void Update(Persona entity)
        {
            base.Update(entity);

            var exists = await _readRepository.ExistsWithSameNameExceptAsync(
                entity.Nombre,
                entity.UsuarioId,
                entity.Id.Value);

            if (exists)
            {
                throw new InvalidOperationException(
                    $"Ya existe otra persona con el nombre '{entity.Nombre.Value}' para este usuario.");
            }
        }
    }
}
