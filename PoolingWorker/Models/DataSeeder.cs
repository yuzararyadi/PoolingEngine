using PoolingWorker.Data;
using PoolingWorker.Models.Domain;

namespace PoolingWorker.Models
{
    public class DataSeeder
    {
        private readonly AppDbContext _DbContext;

        public DataSeeder(AppDbContext dbContext)
        {
            _DbContext = dbContext;
        }

        public void Seed()
        {
            if (!_DbContext.DeviceItems.Any())
            {
                var deviceItems = new List<DeviceItem>()
                {
                    new DeviceItem()
                    {
                        Name = "WellPilot1",
                        Description = "RRL Well 1"
                    },
                    new DeviceItem()
                    {
                        Name = "WellPilot2",
                        Description = "RRL Well 2"
                    },
                    new DeviceItem()
                    {
                        Name = "WellPilot3",
                        Description = "RRL Well 3"
                    },
                    new DeviceItem()
                    {
                        Name = "WellPilot4",
                        Description = "RRL Well 4"
                    },
                    new DeviceItem()
                    {
                        Name = "WellPilot5",
                        Description = "RRL Well 5"
                    },
                    new DeviceItem()
                    {
                        Name = "WellPilot6",
                        Description = "RRL Well 6"
                    }

                };
                _DbContext.DeviceItems.AddRange(deviceItems);
            }
            
            if(!_DbContext.TagItems.Any())
            {
                var tagItems = new List<TagItem>()
                {
                    new TagItem()
                    {
                        Name = "SPM",
                        Description = "Stroke Per Minute"
                    },
                    new TagItem()
                    {
                        Name = "Today Runtime",
                        Description = "Today Runtime"
                    },
                    new TagItem()
                    {
                        Name = "Yest Runtime",
                        Description = "Yesterday Runtime"
                    },
                    new TagItem()
                    {
                        Name = "Stroke Length",
                        Description = "Stroke Length"
                    },
                    new TagItem()
                    {
                        Name = "Average Pump Fillage",
                        Description = "Average Pump Fillage"
                    }

                };
                _DbContext.TagItems.AddRange(tagItems);
            }

            if (!_DbContext.TagGroups.Any())
            {
                var tagGroups = new List<TagGroup>()
                {
                    new TagGroup()
                    {
                        Name = "WellPilot Type Well"
                    }
                };
                _DbContext.TagGroups.AddRange(tagGroups);
            }
            _DbContext.SaveChanges();
        }
    }
}
