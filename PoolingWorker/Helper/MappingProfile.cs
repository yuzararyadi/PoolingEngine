using AutoMapper;
using PoolingWorker.Models.Domain;
using PoolingWorker.Models.Dtos;

namespace PoolingWorker.Helper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<DeviceItem,  DeviceItemDto>();
            CreateMap<TagItem, TagItemDto>();
            CreateMap<TagValue, TagValueDto>();
            CreateMap<RequestItem, RequestPoolingDto>();
            CreateMap<TagGroup, TagGroupDto>();
        }
    }
}
