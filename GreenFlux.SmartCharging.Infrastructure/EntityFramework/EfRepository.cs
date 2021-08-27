using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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
            await _dbContext.Set<TEntity>().AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            return entity.Id;
        }

        public async Task CreateAsync(List<TEntity> entities)
        {
            await _dbContext.Set<TEntity>().AddRangeAsync(entities);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(TEntity entity)
        {
            _dbContext.Set<TEntity>().Update(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            DbSet<TEntity> entities = _dbContext.Set<TEntity>();
            TEntity entity = await entities.SingleOrDefaultAsync(x => x.Id == id);
            if (entity == null) return;
            entities.Remove(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<TEntity> GetByIdAsync(int id, params Expression<Func<TEntity, object>>[] includes)
        {
            IQueryable<TEntity> entities = _dbContext.Set<TEntity>();
            if (includes != null)
            {
                entities = includes.Aggregate(entities, (current, n) => current.Include(n));
            }
            return await entities.AsNoTracking().SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<TEntity>> GetAll()
        {
            return await _dbContext.Set<TEntity>().AsNoTracking().ToListAsync();
        }
    }
}
