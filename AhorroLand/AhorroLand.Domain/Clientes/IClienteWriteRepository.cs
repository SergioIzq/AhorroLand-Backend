using AhorroLand.Shared.Domain.Abstractions.Results;
using AhorroLand.Shared.Domain.Interfaces.Repositories;
using AhorroLand.Shared.Domain.ValueObjects.Ids;

namespace AhorroLand.Domain;

public interface IClienteWriteRepository : IWriteRepository<Cliente, ClienteId>
{
    Task<Result> CreateAsyncWithValidation(Cliente entity, CancellationToken cancellationToken = default);
    Task<Result> UpdateAsync(Cliente entity, CancellationToken cancellationToken = default);
}