using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using PoolingEngine.Models;

namespace PoolingEngine.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<DeviceItem> DeviceItems => Set<DeviceItem>();
        public DbSet<RequestItem> RequestItems => Set<RequestItem>();
        public DbSet<TagItem> TagItems => Set<TagItem>();
        public DbSet<TagValue> TagValues => Set<TagValue>();
    }
}
