using Kash.Domain;
using Kash.Infrastructure.Persistence.Command;
using Kash.Shared.Domain.ValueObjects.Ids;

namespace Kash.Infrastructure.Persistence.Data.Ingresos
{

    // Nota: Asegúrate de que IIngresoWriteRepository herede de IWriteRepository<Ingreso>
    public class IngresoWriteRepository : AbsWriteRepository<Ingreso, IngresoId>, IIngresoWriteRepository
    {
        public IngresoWriteRepository(KashDbContext context) : base(context)
        {
        }
    }
}
