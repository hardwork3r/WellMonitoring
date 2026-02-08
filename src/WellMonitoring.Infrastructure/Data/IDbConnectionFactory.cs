using LinqToDB.Data;

namespace WellMonitoring.Infrastructure.Data
{
    public interface IDbConnectionFactory
    {
        DataConnection CreateConnection();
    }
}