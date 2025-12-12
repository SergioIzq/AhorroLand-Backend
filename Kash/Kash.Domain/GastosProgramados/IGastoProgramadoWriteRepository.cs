using Kash.Shared.Domain.Interfaces.Repositories;
using Kash.Shared.Domain.ValueObjects.Ids;

namespace Kash.Domain
{
    public interface IGastoProgramadoWriteRepository : IWriteRepository<GastoProgramado, GastoProgramadoId>
    {
    }
}