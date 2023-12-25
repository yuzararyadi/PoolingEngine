using AutoMapper;
using PoolingEngine.API.Dtos;
using PoolingEngine.Domain.Entities;

namespace PoolingEngine.API.Helper.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<DeviceItem, DeviceItemDto>();
            CreateMap<DeviceItemDto, DeviceItem>();

            CreateMap<RequestItem, RequestItemDto>();

            CreateMap<TagGroup, TagGroupDto>();
            CreateMap<TagGroupDto, TagGroup>();

            CreateMap<TagItem, TagItemDto>();
            CreateMap<TagItemDto, TagItem>();

            CreateMap<TagValue, TagValueDto>();

        }

    }
}
