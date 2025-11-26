using AhorroLand.Domain;
using AhorroLand.Infrastructure.Persistence.Query;
using AhorroLand.Shared.Application.Dtos;
using AhorroLand.Shared.Domain.ValueObjects.Ids;

namespace AhorroLand.Infrastructure.Persistence.Data.TraspasoProgramados
{
    public class TraspasoProgramadoReadRepository : AbsReadRepository<TraspasoProgramado, TraspasoProgramadoDto, TraspasoProgramadoId>, ITraspasoProgramadoReadRepository
    {
        public TraspasoProgramadoReadRepository(IDbConnectionFactory dbConnectionFactory)
            : base(dbConnectionFactory, "traspasoProgramados")
        {
        }

    }
}