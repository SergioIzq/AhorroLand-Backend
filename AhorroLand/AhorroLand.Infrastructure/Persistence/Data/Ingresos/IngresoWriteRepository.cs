using AhorroLand.Domain;
using AhorroLand.Infrastructure.Persistence.Command;
using AhorroLand.Shared.Domain.ValueObjects.Ids;

namespace AhorroLand.Infrastructure.Persistence.Data.Ingresos
{

    // Nota: Asegúrate de que IIngresoWriteRepository herede de IWriteRepository<Ingreso>
    public class IngresoWriteRepository : AbsWriteRepository<Ingreso, IngresoId>, IIngresoWriteRepository
    {
        public IngresoWriteRepository(AhorroLandDbContext context) : base(context)
        {
        }
    }
}
