
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
                _unitOfWork.TagItem.AddRange(tagItems);
            }

            if (_unitOfWork.TagGroup.GetAll().Count() == 0)
            {
                var tagGroups = new List<TagGroup>()
                {
                    new TagGroup()
                    {
                        Name = "WellPilot Type Well"
                    }
                };
                _unitOfWork.TagGroup.AddRange(tagGroups);
            }
            _unitOfWork.Save();
        }
    }
}
