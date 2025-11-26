using AhorroLand.Domain;
using AhorroLand.Infrastructure.Persistence.Query;
using AhorroLand.Shared.Application.Dtos;
using AhorroLand.Shared.Domain.ValueObjects.Ids;

namespace AhorroLand.Infrastructure.Persistence.Data.Traspasos
{
    public class TraspasoReadRepository : AbsReadRepository<Traspaso, TraspasoDto, TraspasoId>, ITraspasoReadRepository
    {
        public TraspasoReadRepository(IDbConnectionFactory dbConnectionFactory)
            : base(dbConnectionFactory, "traspasos")
        {
        }

    }
}