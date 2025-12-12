using AhorroLand.Shared.Domain.Abstractions.Results;
using AhorroLand.Shared.Domain.Interfaces.Repositories;
using AhorroLand.Shared.Domain.ValueObjects.Ids;

namespace AhorroLand.Domain;

public interface ICuentaWriteRepository : IWriteRepository<Cuenta, CuentaId>
{
    Task<Result> CreateAsyncWithValidation(Cuenta entity, CancellationToken cancellationToken = default);
    Task<Result> UpdateAsync(Cuenta entity, CancellationToken cancellationToken = default);
}