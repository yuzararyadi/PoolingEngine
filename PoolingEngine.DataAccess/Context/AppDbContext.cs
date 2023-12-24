using Microsoft.EntityFrameworkCore;
using PoolingEngine.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoolingEngine.DataAccess.Context
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
