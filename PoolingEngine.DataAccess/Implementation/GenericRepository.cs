using Microsoft.EntityFrameworkCore;
using PoolingEngine.DataAccess.Context;
using PoolingEngine.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PoolingEngine.DataAccess.Implementation
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly AppDbContext _dbcontext;

        public GenericRepository(AppDbContext dbContext)
        {
            _dbcontext = dbContext;
        }
        public void Add(T entity)
        {
            _dbcontext.Set<T>().Add(entity);
        }

        public void AddRange(IEnumerable<T> entities)
        {
            _dbcontext.Set<T>().AddRange(entities);
        }

        public IEnumerable<T> Find(System.Linq.Expressions.Expression<Func<T, bool>> predicate)
        {
            return _dbcontext.Set<T>().Where(predicate);
        }

        public IEnumerable<T> GetAll()
        {
            return _dbcontext.Set<T>().ToList();
        }
        public IQueryable<T> GetAllwithChild(params Expression<Func<T, object>>[] includeExpressions)
        {
            IQueryable<T> set = _dbcontext.Set<T>();
            foreach (var includeExpression in includeExpressions)
            {
                set = set.Include(includeExpression);
            }
            return set;
        }
        public bool UpdateById(int id, T entity)
        {
            if (GetById(id) == null) return false; 
            _dbcontext.ChangeTracker.Clear();
            _dbcontext.Entry(entity).State = EntityState.Modified;
            return true;
        }

        public T? GetById(int id)
        {
            return _dbcontext.Set<T>().Find(id);
        }

        public void Remove(T entity)
        {
            _dbcontext.Set<T>().Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> entities)
        {
            _dbcontext.Set<T>().RemoveRange(entities);
        }
    }
}
