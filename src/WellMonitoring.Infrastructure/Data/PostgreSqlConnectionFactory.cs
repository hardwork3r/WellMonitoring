using LinqToDB.Data;
using LinqToDB.DataProvider.PostgreSQL;

namespace WellMonitoring.Infrastructure.Data
{
    public class PostgreSqlConnectionFactory : IDbConnectionFactory
    {
        private readonly string _connectionString;

        public PostgreSqlConnectionFactory(string connectionString)
        {
            _connectionString = connectionString ?? throw new System.ArgumentNullException(nameof(connectionString));
        }

        public DataConnection CreateConnection()
        {
            var dataProvider = PostgreSQLTools.GetDataProvider(PostgreSQLVersion.v95, _connectionString);
            return new DataConnection(dataProvider, _connectionString);
        }
    }
}