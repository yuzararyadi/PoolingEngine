using Microsoft.EntityFrameworkCore;
using PoolingWorker.Data;
using PoolingWorker.Repositories;

namespace PoolingWorker.Repsitories.Services
{
    public class DeviceUpdateRepository<TEntity, TContext> : IDevTagRepository<TEntity>
        where TEntity : class
        where TContext : AppDbContext
    {
        private readonly TContext _dbContext;
        public DeviceUpdateRepository(TContext context)
        {
            _dbContext = context;
        }
        public async Task<TEntity> Add(TEntity entity)
        {
            _dbContext.Set<TEntity>().Add(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<TEntity> Delete(int id)
        {
            var entity = await _dbContext.Set<TEntity>().FindAsync(id);
            if (entity == null) return entity;
            _dbContext.Set<TEntity>().Remove(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<TEntity> Get(int id)
        {
            var entity = await _dbContext.Set<TEntity>().FindAsync(id);
            if (entity == null) return null;

            return entity;
        }

        public Task<List<TEntity>> GetAll()
        {
            return _dbContext.Set<TEntity>().ToListAsync();
        }

        public async Task<TEntity> Update(TEntity entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
            return entity;
        }
    }
}
