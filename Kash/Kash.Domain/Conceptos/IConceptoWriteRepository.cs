using Kash.Shared.Domain.Abstractions.Results;
using Kash.Shared.Domain.Interfaces.Repositories;
using Kash.Shared.Domain.ValueObjects.Ids;

namespace Kash.Domain;

public interface IConceptoWriteRepository : IWriteRepository<Concepto, ConceptoId>
{
    Task<Result> CreateAsyncWithValidation(Concepto entity, CancellationToken cancellationToken = default);
    Task<Result> UpdateAsync(Concepto entity, CancellationToken cancellationToken = default);
}