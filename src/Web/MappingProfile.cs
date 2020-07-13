using ApplicationCore.Entities;
using AutoMapper;
using Web.Dtos;

namespace Web
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Hero, HeroDto>()
                .ForMember(dto => dto.Id,
                    opt =>
                        opt.MapFrom(entity => entity.HeroId))
                .ReverseMap();
        }
    }
}