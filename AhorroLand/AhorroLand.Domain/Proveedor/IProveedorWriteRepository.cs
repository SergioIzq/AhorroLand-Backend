using AhorroLand.Shared.Domain.Abstractions.Results;
using AhorroLand.Shared.Domain.Interfaces.Repositories;
using AhorroLand.Shared.Domain.ValueObjects.Ids;

namespace AhorroLand.Domain
{
    public interface IProveedorWriteRepository : IWriteRepository<Proveedor, ProveedorId>
    {
        Task<Result> CreateAsyncWithValidation(Proveedor entity, CancellationToken cancellationToken = default);
        Task<Result> UpdateAsync(Proveedor entity, CancellationToken cancellationToken = default);
    }
}