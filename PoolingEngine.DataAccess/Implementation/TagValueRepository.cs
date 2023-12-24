using PoolingEngine.DataAccess.Context;
using PoolingEngine.Domain.Entities;
using PoolingEngine.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoolingEngine.DataAccess.Implementation
{
    public class TagValueRepository : GenericRepository<TagValue>, ITagValueRepository
    {
        private readonly AppDbContext _dbcontext;

        public TagValueRepository(AppDbContext context) : base(context)
        {
            _dbcontext = context;
        }
    }
}
