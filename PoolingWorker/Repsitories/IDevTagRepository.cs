using Microsoft.EntityFrameworkCore.ChangeTracking;
using PoolingWorker.Models.Domain;
using PoolingWorker.Models.Dtos;

namespace PoolingWorker.Repositories
{
    public interface IDevTagRepository<T> where T : class
    {
        Task<List<T>> GetAll();
        Task<T> Get(int id);
        Task<T> Add(T entity);
        Task<T> Update(T entity);
        Task<T> Delete(int id);

    }
}
