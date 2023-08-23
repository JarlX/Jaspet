using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Jaspet.API.Controllers;

using System.Net;
using AutoMapper;
using Business.Abstract;
using Entity.DTO.Product;
using Entity.Poco;
using Entity.Result;

[ApiController]
[Route("Jaspet/[action]")]
public class ProductController : Controller
{
    private readonly IProductService _productService;
    private readonly ICategoryService _categoryService;
    private readonly IMapper _mapper;

    public ProductController(IProductService productService, IMapper mapper, ICategoryService categoryService)
    {
        _productService = productService;
        _mapper = mapper;
        _categoryService = categoryService;
    }

    [HttpPost("/AddProduct")]
    [ProducesResponseType(typeof(Final<bool>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> AddProduct(ProductDTORequest productDtoRequest)
    {
        var category = await _categoryService .GetAsync(q => q.Guid == productDtoRequest.CategoryGUID);
        
        productDtoRequest.CategoryID = category.Id;

        Product product = _mapper.Map<Product>(productDtoRequest);

        product.Category = category;

        await _productService.AddAsync(product);

        return Ok(Final<bool>.OkWithData(true));
    }
}

