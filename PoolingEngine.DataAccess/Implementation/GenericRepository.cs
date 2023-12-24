using Microsoft.EntityFrameworkCore;
using PoolingEngine.DataAccess.Context;
using PoolingEngine.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoolingEngine.DataAccess.Implementation
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly AppDbContext _dbcontect;

        public GenericRepository(AppDbContext dbContext)
        {
            _dbcontect = dbContext;
        }
        public void Add(T entity)
        {
            _dbcontect.Set<T>().Add(entity);
        }

        public void AddRange(IEnumerable<T> entities)
        {
            _dbcontect.Set<T>().AddRange(entities);
        }

        public IEnumerable<T> Find(System.Linq.Expressions.Expression<Func<T, bool>> predicate)
        {
            return _dbcontect.Set<T>().Where(predicate);
        }

        public IEnumerable<T> GetAll()
        {
            return _dbcontect.Set<T>().ToList();
        }

        public T GetById(int id)
        {
            return _dbcontect.Set<T>().Find(id);
        }

        public void Remove(T entity)
        {
            _dbcontect.Set<T>().Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> entities)
        {
            _dbcontect.Set<T>().RemoveRange(entities);
        }
    }
}
