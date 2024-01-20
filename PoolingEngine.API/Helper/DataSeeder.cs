
using PoolingEngine.DataAccess.Implementation;
using PoolingEngine.Domain.Entities;
using PoolingEngine.Domain.Repository;

namespace PoolingEngine.API.Helper
{
    public class DataSeeder
    {
        private readonly IUnitOfWork _unitOfWork;

        public DataSeeder(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void Seed()
        {
            
            if (_unitOfWork.DeviceItem.GetAll().Count() == 0)
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
                _unitOfWork.DeviceItem.AddRange(deviceItems);
            }

            if (_unitOfWork.TagItem.GetAll().Count() == 0)
            {
                var tagItems = new List<TagItem>()
                {
                    new TagItem()
                    {
                        Name = "Current SPM",
                        Description = "Current SPM"
                    },
                    new TagItem()
                    {
                        Name = "Average SPM",
                        Description = "Average SPM"
                    },
                    new TagItem()
                    {
                        Name = "Card Area",
                        Description = "Card Area"
                    },
                    new TagItem()
                    {
                        Name = "Daily Minimum Load",
                        Description = "Daily Minimum Load"
                    },
                    new TagItem()
                    {
                        Name = "Daily Maximum Load",
                        Description = "Daily Maximum Load"
                    },
                    new TagItem()
                    {
                        Name = "Current Cycle Runtime",
                        Description = "Current Cycle Runtime"
                    },
                    new TagItem()
                    {
                        Name = "Run Time Today",
                        Description = "Run Time Today"
                    },
                    new TagItem()
                    {
                        Name = "Run Time Yesterday",
                        Description = "Run Time Yesterday"
                    },
                    new TagItem()
                    {
                        Name = "Cycles Today",
                        Description = "Cycles Today"
                    },
                    new TagItem()
                    {
                        Name = "Cycles Yesterday",
                        Description = "Cycles Yesterday"
                    },
                    new TagItem()
                    {
                        Name = "Average Cycle Time",
                        Description = "Average Cycle Time"
                    },
                    new TagItem()
                    {
                        Name = "Inferred Production Today",
                        Description = "Inferred Production Today"
                    },
                    new TagItem()
                    {
                        Name = "Inferred Production Yesterday",
                        Description = "Inferred Production Yesterday"
                    },
                    new TagItem()
                    {
                        Name = "Current Pump Fillage",
                        Description = "Current Pump Fillage"
                    },
                    new TagItem()
                    {
                        Name = "Average Pump Fillage",
                        Description = "Average Pump Fillage"
                    }
                };
                _unitOfWork.TagItem.AddRange(tagItems);
            }

            if (_unitOfWork.TagGroup.GetAll().Count() == 0)
            {
                var tagGroups = new List<TagGroup>()
                {
                    new TagGroup()
                    {
                        Name = "Main Tag"
                    }
                };
                _unitOfWork.TagGroup.AddRange(tagGroups);
            }
            _unitOfWork.Save();
        }
    }
}
