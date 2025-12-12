using Kash.Shared.Domain.Abstractions;

namespace Kash.Shared.Domain.Interfaces
{
    public interface IDomainValidator
    {
        Task<bool> ExistsAsync<TEntity, TId>(TId id)
                    where TEntity : AbsEntity<TId>
                    where TId : IGuidValueObject;
    }
}
