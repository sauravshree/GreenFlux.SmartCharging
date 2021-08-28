using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using GreenFlux.SmartCharging.Domain.Entities;

namespace GreenFlux.SmartCharging.Domain.Interfaces
{
    public interface IRepository<TEntity> where TEntity : IEntity
    {
        Task<int> CreateAsync(TEntity entity);
        Task CreateAsync(List<TEntity> entities);
        Task UpdateAsync(TEntity entity);
        Task DeleteAsync(int id);
        Task DeleteAsync(Expression<Func<TEntity, bool>> where);
        Task<TEntity> GetByIdAsync(int id, params Expression<Func<TEntity, object>>[] includes);
        Task<List<TEntity>> GetAll(Expression<Func<TEntity, bool>> where = null, params Expression<Func<TEntity, object>>[] includes);
    }
}
