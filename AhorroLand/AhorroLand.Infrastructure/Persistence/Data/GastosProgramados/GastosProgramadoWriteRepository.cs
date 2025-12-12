using AhorroLand.Domain;
using AhorroLand.Infrastructure.Persistence.Command;
using AhorroLand.Shared.Domain.ValueObjects.Ids;

namespace AhorroLand.Infrastructure.Persistence.Data.GastosProgramados
{

    // Nota: Asegúrate de que IGastoProgramadoWriteRepository herede de IWriteRepository<GastoProgramado>
    public class GastoProgramadoWriteRepository : AbsWriteRepository<GastoProgramado, GastoProgramadoId>, IGastoProgramadoWriteRepository
    {
        public GastoProgramadoWriteRepository(AhorroLandDbContext context) : base(context)
        {
        }
    }
}
