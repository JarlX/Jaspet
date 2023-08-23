namespace Jaspet.API.Mapping;

using AutoMapper;
using Entity.DTO.Product;
using Entity.Poco;

public class ProductRequestMapper : Profile
{
    public ProductRequestMapper()
    {
        CreateMap<Product, ProductDTORequest>()
            .ForMember(dest => dest.Name, opt =>
            {
                opt.MapFrom(src => src.Name);
            })
            .ForMember(dest => dest.Guid, opt =>
            {
                opt.MapFrom(src => src.Guid);
            })
            .ForMember(dest => dest.Description, opt =>
            {
                opt.MapFrom(src => src.Description);
            })
            .ForMember(dest => dest.CategoryID, opt =>
            {
                opt.MapFrom(src => src.CategoryId);
            })
            .ForMember(dest => dest.Image, opt =>
            {
                opt.MapFrom(src => src.Image);
            })
            .ForMember(dest => dest.UnitPrice, opt =>
            {
                opt.MapFrom(src => src.UnitPrice);
            }).ReverseMap();
    }
}