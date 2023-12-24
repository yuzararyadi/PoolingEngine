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
    public class TagGroupRepository : GenericRepository<TagGroup>, ITagGroupRepository
    {
        private readonly AppDbContext _dbcontext;

        public TagGroupRepository(AppDbContext context) : base(context)
        {
            _dbcontext = context;
        }

    }
}
