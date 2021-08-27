using System.Collections.Generic;
using System.Threading.Tasks;
using GreenFlux.SmartCharging.Domain.Entities;
using GreenFlux.SmartCharging.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GreenFlux.SmartCharging.Infrastructure.EntityFramework
{
    internal class EfRepository<TEntity> : IRepository<TEntity> where TEntity : EntityBase
    {
        private readonly SmartChargingDbContext _dbContext;

        public EfRepository(SmartChargingDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<int> CreateAsync(TEntity entity)
        {
            _dbContext.Add(entity);
            await _dbContext.SaveChangesAsync();
            return entity.Id;
        }

        public async Task UpdateAsync(TEntity entity)
        {
            _dbContext.Update(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            TEntity entity = await _dbContext.Set<TEntity>().SingleOrDefaultAsync(x => x.Id == id);
            if (entity == null) return;
            _dbContext.Remove(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<TEntity> GetByIdAsync(int id)
        {
            DbSet<TEntity> query = _dbContext.Set<TEntity>();
            return await query.SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<TEntity>> GetAll()
        {
            DbSet<TEntity> query = _dbContext.Set<TEntity>();
            return await query.ToListAsync();
        }
    }
}
