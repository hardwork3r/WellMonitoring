using System;
using System.Linq;

namespace WellMonitoring.Infrastructure.Repositories
{
    public interface IRepository<TEntity> where TEntity : class
    {
        IQueryable<TEntity> GetAll();

        TEntity GetById(int id);
    }
}