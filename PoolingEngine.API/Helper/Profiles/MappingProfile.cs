using AutoMapper;
using PoolingEngine.API.Dtos;
using PoolingEngine.Domain.Entities;
using System.Linq.Expressions;

namespace PoolingEngine.API.Helper.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<DeviceItem, DeviceItemDto>()
                .ForMember(dto => dto.TagGroups, opt => opt.MapFrom(x => x.TagGroups));
                
            CreateMap<DeviceItemDto, DeviceItem>()
                .ForMember(di => di.TagGroups, dto => dto.MapFrom(x => x.TagGroups));

            CreateMap<RequestItem, RequestItemDto>();
            CreateMap<RequestItemDto, RequestItem>();
            
            CreateMap<TagDef, TagDefDto>();
            CreateMap<TagDefDto, TagDef>();

            CreateMap<TagGroup, TagGroupDto>()
                .ForMember(dto => dto.TagItems, opt => opt.MapFrom(x => x.TagItems));

            CreateMap<TagGroupDto, TagGroup>()
                .ForMember(tg => tg.TagItems, dto => dto.MapFrom(x => x.TagItems));

            CreateMap<TagItem, TagItemDto>();
            CreateMap<TagItemDto, TagItem>();

            CreateMap<TagValue, TagValueDto>();

            CreateMap<RequestPoolingDto, RequestPooling>();

        }

    }
}
