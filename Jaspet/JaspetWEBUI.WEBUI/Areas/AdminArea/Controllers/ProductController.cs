using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace JaspetWEBUI.WEBUI.Areas.AdminArea.Controllers
{
    using System.Net;
    using Core.DTO;
    using Core.Result;
    using Core.ViewModel;
    using Helper.SessionHelper;
    using Newtonsoft.Json;
    using RestSharp;

    [Area("AdminArea")]
    public class ProductController : Controller
    {
        private const string BaseUrl = "http://localhost:5146";
        private const string JsonContentType = "application/json";
        
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProductController(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<List<ProductDTO>> GetProductList()
        {
            var url = $"{BaseUrl}/Products";
            var client = new RestClient(url);
            var request = new RestRequest(url);
            request.AddHeader("Content-Type", JsonContentType);
            request.AddHeader("Authorization", "Bearer " + SessionManager.LoggedUser.Token);
            RestResponse restResponse = await client.ExecuteAsync(request);

            var responseObject = JsonConvert.DeserializeObject<ApiResult<List<ProductDTO>>>(restResponse.Content);

            var products = responseObject.Data;

            return products;
        }
        public async Task<List<CategoryDTO>> GetCategoryList()
        {
            var url = $"{BaseUrl}/Categories";
            var client = new RestClient(url);
            var request = new RestRequest(url);
            request.AddHeader("Content-Type", JsonContentType);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Bearer " + SessionManager.LoggedUser.Token);
            RestResponse restResponse = await client.ExecuteAsync(request);

            var responseObject = JsonConvert.DeserializeObject<ApiResult<List<CategoryDTO>>>(restResponse.Content);

            var categories = responseObject.Data;

            return categories;
        }
        
        [HttpGet("/Admin/Products")]
        public async Task<IActionResult> Index()
        {
            ProductViewModel productViewModel = new()
            {
                Products = await GetProductList(),
                Categories = await GetCategoryList()
            };
            return View(productViewModel);
        }
        
        [HttpGet("/Admin/Product/{productGUID}")]
        public async Task<IActionResult> GetProduct(Guid productGUID)
        {
            var url = $"{BaseUrl}/Product/" + productGUID;
            var client = new RestClient(url);
            var request = new RestRequest(url);
            request.AddHeader("Content-Type", JsonContentType);
            request.AddHeader("Authorization", "Bearer " + SessionManager.LoggedUser.Token);
            RestResponse restResponse = await client.ExecuteAsync(request);

            var responseObject = JsonConvert.DeserializeObject<ApiResult<ProductDTO>>(restResponse.Content);

            var product = responseObject.Data;

            return Json(new { success = true, data = product });
        }


        [HttpPost("/Admin/AddProduct")]
        public async Task<IActionResult> AddProduct(ProductDTO productDto, IFormFile productImage)
        {
            if (productImage != null)
            {

                string[] fileNameParts = productImage.FileName.Split('.');
                string fileNameWithoutExtension = string.Join(".", fileNameParts.Take(fileNameParts.Length - 1));
                string fileExtension = fileNameParts.Last();

                string uniqueFileName = $"{fileNameWithoutExtension}_{Guid.NewGuid()}.{fileExtension}";


                string uploadFolder = Path.Combine(_webHostEnvironment.WebRootPath, "ProductsMedia", uniqueFileName);

                await using (var fileStream = new FileStream(uploadFolder, FileMode.Create))
                {
                    productImage.CopyTo(fileStream);
                }

                productDto.Image = uniqueFileName;
            }

            var url = $"{BaseUrl}/AddProduct";
            var client = new RestClient(url);
            var request = new RestRequest(url, Method.Post);
            request.AddHeader("Content-Type", JsonContentType);
            request.AddHeader("Authorization", "Bearer " + SessionManager.LoggedUser.Token);
            var body = JsonConvert.SerializeObject(productDto);
            request.AddBody(body, JsonContentType);
            RestResponse restResponse = await client.ExecuteAsync(request);

            var responseObject = JsonConvert.DeserializeObject<ApiResult<bool>>(restResponse.Content);
            
            return Json(new { success = true, data = responseObject.Data });            
        }
        
        [HttpPost("/Admin/UpdateProduct")]
        public async Task<IActionResult> UpdateProduct(ProductDTO productDto, IFormFile productImage)
        {
            if (productImage != null)
            {

                string[] fileNameParts = productImage.FileName.Split('.');
                string fileNameWithoutExtension = string.Join(".", fileNameParts.Take(fileNameParts.Length - 1));
                string fileExtension = fileNameParts.Last();

                string uniqueFileName = $"{fileNameWithoutExtension}_{Guid.NewGuid()}.{fileExtension}";


                string uploadFolder = Path.Combine(_webHostEnvironment.WebRootPath, "ProductsMedia", uniqueFileName);

                await using (var fileStream = new FileStream(uploadFolder, FileMode.Create))
                {
                    productImage.CopyTo(fileStream);
                }

                productDto.Image = uniqueFileName;
            }

            var url = $"{BaseUrl}/UpdateProduct";
            var client = new RestClient(url);
            var request = new RestRequest(url, Method.Put);
            request.AddHeader("Content-Type", JsonContentType);
            request.AddHeader("Authorization", "Bearer " + SessionManager.LoggedUser.Token);
            var body = JsonConvert.SerializeObject(productDto);
            request.AddBody(body, JsonContentType);
            RestResponse restResponse = await client.ExecuteAsync(request);

            var responseObject = JsonConvert.DeserializeObject<ApiResult<bool>>(restResponse.Content);
            return Json(new { success = true, data = responseObject.Data });
        }

        [HttpPost("/Admin/RemoveProduct/{productGUID}")]
        public async Task<IActionResult> RemoveProduct(Guid productGUID)
        {
            var url = $"{BaseUrl}/RemoveProduct" + productGUID;
            var client = new RestClient(url);
            var request = new RestRequest(url, Method.Delete);
            request.AddHeader("Content-Type", JsonContentType);
            request.AddHeader("Authorization", "Bearer " + SessionManager.LoggedUser.Token);
            RestResponse restResponse = await client.ExecuteAsync(request);

            var responseObject = JsonConvert.DeserializeObject<ApiResult<bool>>(restResponse.Content);
            return Json(new { success = true, data = responseObject.Data });
        }
    }
}