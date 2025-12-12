using System.Data;

namespace Kash.Infrastructure.Persistence.Query;

public interface IDbConnectionFactory
{
    IDbConnection CreateConnection();
}
