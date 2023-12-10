using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using PoolingWorker.Models.Domain;

namespace PoolingWorker.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<DeviceItem> DeviceItems => Set<DeviceItem>();
        //public DbSet<DeviceTagGroups> DeviceTagGroups => Set<DeviceTagGroups>();
        //public DbSet<DeviceTagItems> DeviceTagItems => Set<DeviceTagItems>();
        public DbSet<RequestItem> RequestItems => Set<RequestItem>();
        public DbSet<TagGroup> TagGroups => Set<TagGroup>();
        public DbSet<TagItem> TagItems => Set<TagItem>();
        public DbSet<TagValue> TagValues => Set<TagValue>();      

    }
}
