using LinqToDB.Data;

namespace WellMonitoring.Infrastructure.Data
{
    /// <summary>
    /// Фабрика для создания подключений к базе данных
    /// </summary>
    public interface IDbConnectionFactory
    {
        /// <summary>
        /// Создать новое подключение к БД
        /// </summary>
        DataConnection CreateConnection();
    }
}