using AhorroLand.Shared.Domain.Abstractions;

namespace AhorroLand.Shared.Domain.Interfaces
{
    public interface IDomainValidator
    {
        Task<bool> ExistsAsync<TEntity, TId>(TId id)
                    where TEntity : AbsEntity<TId>
                    where TId : IGuidValueObject;
    }
}
