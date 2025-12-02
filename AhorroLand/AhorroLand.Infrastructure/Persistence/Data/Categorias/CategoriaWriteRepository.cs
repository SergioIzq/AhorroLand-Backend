using AhorroLand.Domain;
using AhorroLand.Infrastructure.Persistence.Command;
using AhorroLand.Shared.Domain.ValueObjects.Ids;

namespace AhorroLand.Infrastructure.Persistence.Data.Categorias
{
    public class CategoriaWriteRepository : AbsWriteRepository<Categoria, CategoriaId>, ICategoriaWriteRepository
    {
        private readonly ICategoriaReadRepository _readRepository;

        public CategoriaWriteRepository(
            AhorroLandDbContext context,
            ICategoriaReadRepository readRepository) : base(context)
        {
            _readRepository = readRepository;
        }

        public override async Task CreateAsync(Categoria entity, CancellationToken cancellationToken = default)
        {
            // Validar que no exista una categoría con el mismo nombre para el mismo usuario
            var exists = await _readRepository.ExistsWithSameNameAsync(
                entity.Nombre,
                entity.IdUsuario,
                cancellationToken);

            if (exists)
            {
                throw new InvalidOperationException(
                    $"Ya existe una categoría con el nombre '{entity.Nombre.Value}' para este usuario.");
            }

            await base.CreateAsync(entity, cancellationToken);
        }

        /// <summary>
        /// Actualiza una categoría validando que no exista duplicado.
        /// </summary>
        public override async void Update(Categoria entity)
        {
            // Primero verificar que la entidad existe (validación del base)
            base.Update(entity);

            // Luego validar duplicados
            var exists = await _readRepository.ExistsWithSameNameExceptAsync(
                entity.Nombre,
                entity.IdUsuario,
                entity.Id.Value);

            if (exists)
            {
                throw new InvalidOperationException(
                    $"Ya existe otra categoría con el nombre '{entity.Nombre.Value}' para este usuario.");
            }
        }
    }
}
