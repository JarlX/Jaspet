namespace Jaspet.API.Controllers;

using System.Net;
using AutoMapper;
using Business.Abstract;
using Entity.DTO.Category;
using Entity.Poco;
using Entity.Result;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("Jaspet/[action]")]
public class CategoryController : Controller
{
    private readonly ICategoryService _categoryService;
    private readonly IMapper _mapper;

    public CategoryController(ICategoryService categoryService, IMapper mapper)
    {
        _categoryService = categoryService;
        _mapper = mapper;
    }
    
    [HttpPost("/AddCategory")]
    [ProducesResponseType(typeof(Final<CategoryDTOResponse>),(int)HttpStatusCode.OK)]
    public async Task<IActionResult> AddCategory(CategoryDTORequest categoryDtoRequest)
    {
        Category category = _mapper.Map<Category>(categoryDtoRequest);

        await _categoryService.AddAsync(category);

        CategoryDTOResponse categoryDtoResponse = _mapper.Map<CategoryDTOResponse>(category);

        return Ok(Final<CategoryDTOResponse>.OkWithData(categoryDtoResponse));
    }

    [HttpGet("/Categories")]
    [ProducesResponseType(typeof(Final<CategoryDTOResponse>),(int)HttpStatusCode.OK)]
    public async Task<IActionResult> GetAllCategories()
    {
        var categories = await _categoryService.GetAllAsync(q => q.IsActive == true && q.IsDeleted == false);

        if (categories != null)
        {
            List<CategoryDTOResponse> categoryDtoResponses = new List<CategoryDTOResponse>();

            foreach (var category in categories)
            {
                categoryDtoResponses.Add(_mapper.Map<CategoryDTOResponse>(category));
            }

            return Ok(Final<List<CategoryDTOResponse>>.OkWithData(categoryDtoResponses));
        }

        return NotFound(Final<List<CategoryDTOResponse>>.NotFound());
    }

    [HttpGet("/Category/{guid:guid}")]
    [ProducesResponseType(typeof(CategoryDTOResponse), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> GetCategoryByGUID(Guid guid)
    {
        var category = await _categoryService.GetAsync(q => q.Guid == guid);

        if (category != null)
        {
            CategoryDTOResponse categoryDtoResponse = new CategoryDTOResponse();

            return Ok(Final<CategoryDTOResponse>.OkWithData(categoryDtoResponse));

        }

        return NotFound(Final<CategoryDTOResponse>.NotFound());

    }

    [HttpPut("/UpdateCategory")]
    [ProducesResponseType(typeof(CategoryDTOResponse), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> UpdateCategory(CategoryUpdateDTORequest categoryUpdateDtoRequest)
    {
        Category category = await _categoryService.GetAsync(q => q.Guid == categoryUpdateDtoRequest.Guid);

        category.CategoryName = categoryUpdateDtoRequest.CategoryName;
        category.IsActive = categoryUpdateDtoRequest.IsActive != null
            ? categoryUpdateDtoRequest.IsActive
            : category.IsActive;
        await _categoryService.UpdateAsync(category);

        return Ok(Final<CategoryDTOResponse>.OkWithOutData());
    }

    [HttpDelete("/RemoveCategory/{guid:guid}")]
    [ProducesResponseType(typeof(Final<bool>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> DeleteCategory(Guid guid)
    {
        Category category = await _categoryService.GetAsync(q => q.Guid == guid);

        category.IsActive = false;
        category.IsDeleted = true;

        await _categoryService.UpdateAsync(category);

        return Ok(Final<CategoryDTOResponse>.OkWithOutData());
    }
}

