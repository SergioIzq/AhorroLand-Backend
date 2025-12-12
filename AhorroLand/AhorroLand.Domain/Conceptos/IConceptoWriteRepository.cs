using AhorroLand.Shared.Domain.Abstractions.Results;
using AhorroLand.Shared.Domain.Interfaces.Repositories;
using AhorroLand.Shared.Domain.ValueObjects.Ids;

namespace AhorroLand.Domain;

public interface IConceptoWriteRepository : IWriteRepository<Concepto, ConceptoId>
{
    Task<Result> CreateAsyncWithValidation(Concepto entity, CancellationToken cancellationToken = default);
    Task<Result> UpdateAsync(Concepto entity, CancellationToken cancellationToken = default);
}