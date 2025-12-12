using Kash.Shared.Domain.Interfaces.Repositories;
using Kash.Shared.Domain.ValueObjects.Ids;

namespace Kash.Domain
{
    public interface IIngresoWriteRepository : IWriteRepository<Ingreso, IngresoId>
    {
    }
}