using System.Data;

namespace TaskManagement.Infrastructure.Database
{
    public interface IConnectionFactory
    {
        IDbConnection CreateConnection();
    }
}
