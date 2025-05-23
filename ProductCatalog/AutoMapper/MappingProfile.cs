using AutoMapper;
using ProductCatalog.DAL.Entities;
using ProductCatalog.DTOs;

namespace ProductCatalog.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ProductToAddViewModel, Product>().ForMember(

                dest => dest.CreationDate, o => o.MapFrom(src => DateTime.UtcNow)).ReverseMap();

        }
    }
}
