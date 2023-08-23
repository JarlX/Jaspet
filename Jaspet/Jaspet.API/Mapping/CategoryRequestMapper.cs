namespace Jaspet.API.Mapping;

using AutoMapper;
using Entity.DTO.Category;
using Entity.Poco;

public class CategoryRequestMapper : Profile
{
    public CategoryRequestMapper()
    {
        CreateMap<Category, CategoryDTORequest>()
            .ForMember(dest => dest.CategoryName, opt => { opt.MapFrom(src => src.CategoryName); }).ReverseMap();
    }
}