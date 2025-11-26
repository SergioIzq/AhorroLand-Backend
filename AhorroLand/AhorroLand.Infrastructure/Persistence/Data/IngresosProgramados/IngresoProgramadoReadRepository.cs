using AhorroLand.Domain;
using AhorroLand.Infrastructure.Persistence.Query;
using AhorroLand.Shared.Application.Dtos;
using AhorroLand.Shared.Domain.ValueObjects.Ids;

namespace AhorroLand.Infrastructure.Persistence.Data.IngresosProgramados
{
    public class IngresoProgramadoReadRepository : AbsReadRepository<IngresoProgramado, IngresoProgramadoDto, IngresoProgramadoId>, IIngresoProgramadoReadRepository
    {
        public IngresoProgramadoReadRepository(IDbConnectionFactory dbConnectionFactory)
            : base(dbConnectionFactory, "ingresosProgramados")
        {
        }

    }
}