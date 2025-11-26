using AhorroLand.Domain;
using AhorroLand.Infrastructure.Persistence.Command;
using AhorroLand.Shared.Domain.ValueObjects.Ids;

namespace AhorroLand.Infrastructure.Persistence.Data.Usuarios
{

    public class UsuarioWriteRepository : AbsWriteRepository<Usuario, UsuarioId>, IUsuarioWriteRepository
    {
        public UsuarioWriteRepository(AhorroLandDbContext context) : base(context)
        {
        }
    }
}
