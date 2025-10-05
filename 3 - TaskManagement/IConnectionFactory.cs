using System.Data;

namespace TaskManagement
{
    public interface IConnectionFactory
    {
        IDbConnection CreateConnection();
    }
}
