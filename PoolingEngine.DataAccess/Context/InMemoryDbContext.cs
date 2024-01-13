using Microsoft.EntityFrameworkCore;
using PoolingEngine.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace PoolingEngine.DataAccess.Context
{
    public class InMemoryDbContext : DbContext
    {
        public InMemoryDbContext(DbContextOptions<InMemoryDbContext> options) : base(options)
        {
            
        }

        public DbSet<RequestItem> RequestItems => Set<RequestItem>();
        public DbSet<TagGroup> TagGroups => Set<TagGroup>();
    }
}
