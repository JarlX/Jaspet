namespace Jaspet.API.Mapping;

using AutoMapper;
using Entity.DTO.Product;
using Entity.Poco;

public class ProductResponseMapper : Profile
{
    public ProductResponseMapper()
    {
        CreateMap<Product, ProductDTOResponse>()
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
            .ForMember(dest => dest.CategoryGUID, opt =>
            {
                opt.MapFrom(src => src.Category.Guid);
            })
            .ForMember(dest => dest.Image, opt =>
            {
                opt.MapFrom(src => src.Image);
            })
            .ForMember(dest => dest.UnitPrice, opt =>
            {
                opt.MapFrom(src => src.UnitPrice);
            }).ForMember(dest => dest.CategoryName, opt =>
            {
                opt.MapFrom(src => src.Category.CategoryName);
            }).ReverseMap();
    }
}