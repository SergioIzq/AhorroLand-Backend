using AhorroLand.Shared.Domain.Abstractions.Results;
using AhorroLand.Shared.Domain.Interfaces.Repositories;
using AhorroLand.Shared.Domain.ValueObjects.Ids;

namespace AhorroLand.Domain
{
    public interface ICategoriaWriteRepository : IWriteRepository<Categoria, CategoriaId>
    {
        Task<Result> CreateAsyncWithValidation(Categoria entity, CancellationToken cancellationToken = default);
        Task<Result> UpdateAsync(Categoria entity, CancellationToken cancellationToken = default);
    }
}