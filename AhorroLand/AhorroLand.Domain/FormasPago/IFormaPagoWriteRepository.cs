using AhorroLand.Shared.Domain.Interfaces.Repositories;
using AhorroLand.Shared.Domain.ValueObjects.Ids;

namespace AhorroLand.Domain;

public interface IFormaPagoWriteRepository : IWriteRepository<FormaPago, FormaPagoId>
{
}