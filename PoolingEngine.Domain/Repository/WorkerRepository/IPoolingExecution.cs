using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoolingEngine.Domain.Repository.WorkerRepository
{
    public interface IPoolingExecution
    {
        Task DoRequestItemPooling(CancellationToken stoppingToken);
    }
}
