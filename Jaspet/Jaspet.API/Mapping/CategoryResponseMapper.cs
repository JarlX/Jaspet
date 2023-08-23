namespace Jaspet.API.Mapping;

using AutoMapper;
using Entity.DTO.Category;
using Entity.Poco;

public class CategoryResponseMapper : Profile
{
    public CategoryResponseMapper()
    {
        CreateMap<Category, CategoryDTOResponse>()
            .ForMember(dest => dest.CategoryName, opt => { opt.MapFrom(src => src.CategoryName); })
            .ForMember(dest => dest.Guid, opt => { opt.MapFrom(src => src.Guid); }).ReverseMap();
    }
}