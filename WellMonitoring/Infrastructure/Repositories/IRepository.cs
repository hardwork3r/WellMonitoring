using System;
using System.Linq;

namespace WellMonitoring.Infrastructure.Repositories
{
    /// <summary>
    /// Базовый интерфейс репозитория для работы с сущностями
    /// </summary>
    /// <typeparam name="TEntity">Тип сущности</typeparam>
    public interface IRepository<TEntity> where TEntity : class
    {
        /// <summary>
        /// Получить все записи
        /// </summary>
        IQueryable<TEntity> GetAll();

        /// <summary>
        /// Получить запись по ID
        /// </summary>
        TEntity GetById(int id);
    }
}