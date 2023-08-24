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
    
    [HttpGet("/Products")]
    [ProducesResponseType(typeof(Final<List<ProductDTOResponse>>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> GetProducts()
    {
        var products = await _productService.GetAllAsync(q => q.IsActive == true && q.IsDeleted == false, "Category");

        if (products != null)
        {
            List<ProductDTOResponse> productDtoResponseList = new List<ProductDTOResponse>();

            foreach (var product in products)
            {
                productDtoResponseList.Add(_mapper.Map<ProductDTOResponse>(product));

            }

            return Ok(Final<List<ProductDTOResponse>>.OkWithData(productDtoResponseList));
        }
        else
        {
            return NotFound(Final<List<ProductDTOResponse>>.NotFound());
        }
    }
    
    [HttpGet("/Product/{productGUID}")]
    [ProducesResponseType(typeof(Final<List<ProductDTOResponse>>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> GetProduct(Guid productGUID)
    {
        var product = await _productService.GetAsync(q => q.Guid == productGUID, "Category");

        if (product != null)
        {
            ProductDTOResponse productDtoResponse = _mapper.Map<ProductDTOResponse>(product);
            return Ok(Final<ProductDTOResponse>.OkWithData(productDtoResponse));
        }
        else
        {
            return NotFound(Final<List<ProductDTOResponse>>.NotFound());
        }
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
    
    [HttpPut("/UpdateProduct")]
    [ProducesResponseType(typeof(Final<bool>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> UpdateProduct(ProductDTORequest productDTORequest)
    {

        Product product = await _productService.GetAsync(q => q.Guid == productDTORequest.Guid);

        string featuredImage = product.Image;

        var category = await _categoryService.GetAsync(q => q.Guid == productDTORequest.CategoryGUID);

        //_mapper.Map<Product>(productDTORequest);

        product.UnitPrice = productDTORequest.UnitPrice;
        product.Name=productDTORequest.Name;
        product.Description=productDTORequest.Description;
        product.Image = featuredImage;

        if (productDTORequest.Image is null)
        {
            product.Image = featuredImage;
        }

        product.Category = category;
        await _productService.UpdateAsync(product);

        return Ok(Final<bool>.OkWithData(true));
    }


    [HttpDelete("/RemoveProduct/{productGUID}")]
    [ProducesResponseType(typeof(Final<bool>), (int)HttpStatusCode.OK)]

    public async Task<IActionResult>RemoveProduct(Guid productGUID)
    {
        Product product = await _productService.GetAsync(q => q.Guid == productGUID);

        product.IsActive = false;
        product.IsDeleted = true;
        await _productService.UpdateAsync(product);
        return Ok(Final<bool>.OkWithData(true));
    }
}

