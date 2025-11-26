using AhorroLand.Domain;
using AhorroLand.Infrastructure.Persistence.Query;
using AhorroLand.Shared.Application.Dtos;
using AhorroLand.Shared.Domain.ValueObjects.Ids;

namespace AhorroLand.Infrastructure.Persistence.Data.GastosProgramados
{
    public class GastoProgramadoReadRepository : AbsReadRepository<GastoProgramado, GastoProgramadoDto, GastoProgramadoId>, IGastoProgramadoReadRepository
    {
        public GastoProgramadoReadRepository(IDbConnectionFactory dbConnectionFactory)
            : base(dbConnectionFactory, "gastosProgramados")
        {
        }

    }
}