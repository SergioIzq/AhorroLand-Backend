using AhorroLand.Shared.Domain.Abstractions.Results;
using AhorroLand.Shared.Domain.Interfaces.Repositories;
using AhorroLand.Shared.Domain.ValueObjects.Ids;

namespace AhorroLand.Domain;

public interface IFormaPagoWriteRepository : IWriteRepository<FormaPago, FormaPagoId>
{
    Task<Result> CreateAsyncWithValidation(FormaPago entity, CancellationToken cancellationToken = default);
    Task<Result> UpdateAsync(FormaPago entity, CancellationToken cancellationToken = default);
}