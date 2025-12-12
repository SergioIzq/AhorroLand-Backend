using AhorroLand.Domain;
using AhorroLand.Infrastructure.Persistence.Command;
using AhorroLand.Shared.Domain.ValueObjects.Ids;

namespace AhorroLand.Infrastructure.Persistence.Data.IngresosProgramados
{

    // Nota: Asegúrate de que IIngresoProgramadoWriteRepository herede de IWriteRepository<IngresoProgramado>
    public class IngresoProgramadoWriteRepository : AbsWriteRepository<IngresoProgramado, IngresoProgramadoId>, IIngresoProgramadoWriteRepository
    {
        public IngresoProgramadoWriteRepository(AhorroLandDbContext context) : base(context)
        {
        }
    }
}
