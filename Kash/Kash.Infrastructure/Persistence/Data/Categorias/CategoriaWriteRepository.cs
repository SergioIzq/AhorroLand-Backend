using Kash.Domain;
using Kash.Domain.Errors;
using Kash.Infrastructure.Persistence.Command;
using Kash.Shared.Domain.Abstractions.Results;
using Kash.Shared.Domain.ValueObjects.Ids;

namespace Kash.Infrastructure.Persistence.Data.Categorias
{
    public class CategoriaWriteRepository : AbsWriteRepository<Categoria, CategoriaId>, ICategoriaWriteRepository
    {
        private readonly ICategoriaReadRepository _readRepository;

        public CategoriaWriteRepository(
            KashDbContext context,
            ICategoriaReadRepository readRepository) : base(context)
        {
            _readRepository = readRepository;
        }

        public async Task<Result> CreateAsyncWithValidation(Categoria entity, CancellationToken cancellationToken = default)
        {
            // 1. Validar duplicados
            var exists = await _readRepository.ExistsWithSameNameAsync(
                entity.Nombre,
                entity.IdUsuario,
                cancellationToken);

            if (exists)
            {
                return Result.Failure(CategoriaErrors.NombreDuplicado(entity.Nombre.Value));
            }

            // 2. Agregar al contexto
            await base.CreateAsync(entity, cancellationToken);

            return Result.Success();
        }

        public async Task<Result> UpdateAsync(Categoria entity, CancellationToken cancellationToken = default)
        {
            // 1. Validar duplicados (excepto la propia entidad)
            var exists = await _readRepository.ExistsWithSameNameExceptAsync(
                entity.Nombre,
                entity.IdUsuario,
                entity.Id.Value,
                cancellationToken);

            if (exists)
            {
                return Result.Failure(CategoriaErrors.NombreDuplicado(entity.Nombre.Value));
            }

            // 2. Marcar como modificado
            base.Update(entity);

            return Result.Success();
        }
    }
}
