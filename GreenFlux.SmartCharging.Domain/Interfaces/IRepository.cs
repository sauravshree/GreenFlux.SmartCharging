using System.Collections.Generic;
using System.Threading.Tasks;
using GreenFlux.SmartCharging.Domain.Entities;

namespace GreenFlux.SmartCharging.Domain.Interfaces
{
    public interface IRepository<TEntity> where TEntity : IEntity
    {
        Task<int> CreateAsync(TEntity entity);
        Task UpdateAsync(TEntity entity);
        Task DeleteAsync(int id);
        Task<TEntity> GetByIdAsync(int id);
        Task<List<TEntity>> GetAll();
    }
}
