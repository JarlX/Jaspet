using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace JaspetWEBUI.WEBUI.Areas.AdminArea.Controllers
{
    using Core.DTO;
    using Core.Result;
    using Helper.SessionHelper;
    using Newtonsoft.Json;
    using RestSharp;

    [Area("AdminArea")]
    public class CategoryController : Controller
    {
        private const string BaseUrl = "http://localhost:5146";
        private const string JsonContentType = "application/json";

        [HttpGet("/Admin/Categories")]
        public async Task<IActionResult> Index()
        {
            var url = $"{BaseUrl}/Categories";
            var client = new RestClient(url);
            var request = new RestRequest(url);
            request.AddHeader("Content-Type", JsonContentType);
            request.AddHeader("Authorization", "Bearer " + SessionManager.LoggedUser.Token);
            RestResponse restResponse = await client.ExecuteAsync(request);

            var responseObject = JsonConvert.DeserializeObject<ApiResult<List<CategoryDTO>>>(restResponse.Content);

            var categories = responseObject.Data;
            
            return View(categories);
        }
        
        [HttpGet("/Admin/Category/{categoryGUID}")]
        public async Task<IActionResult>GetCategory(Guid categoryGUID)
        {
            var url = $"{BaseUrl}/Category/" + categoryGUID;
            var client = new RestClient(url);
            var request = new RestRequest(url);
            request.AddHeader("Content-Type", JsonContentType);
            request.AddHeader("Authorization", "Bearer " + SessionManager.LoggedUser.Token);
            RestResponse restResponse = await client.ExecuteAsync(request);

            var responseObject = JsonConvert.DeserializeObject<ApiResult<CategoryDTO>>(restResponse.Content);
            
            
            return Json(new { success = true, data = responseObject.Data });
            
           
        }

        [HttpPost("/Admin/AddCategory")]
        public async Task<IActionResult> AddCategory(CategoryDTO categoryDto)
        {
            var url = $"{BaseUrl}/AddCategory";
            var client = new RestClient(url);
            var request = new RestRequest(url,Method.Post);
            request.AddHeader("Content-Type", JsonContentType);
            request.AddHeader("Authorization", "Bearer " + SessionManager.LoggedUser.Token);
            var body = JsonConvert.SerializeObject(categoryDto);
            request.AddBody(body, JsonContentType);
            RestResponse restResponse = await client.ExecuteAsync(request);
            
            var responseObject = JsonConvert.DeserializeObject<ApiResult<CategoryDTO>>(restResponse.Content);

            return Json(new { success = true, data = responseObject.Data });
        }
        
        [HttpPost("/Admin/UpdateCategory")]
        public async Task<IActionResult> UpdateCategory(CategoryDTO categoryDto)
        {
            var url = $"{BaseUrl}/UpdateCategory";
            var client = new RestClient(url);
            var request = new RestRequest(url,Method.Put);
            request.AddHeader("Content-Type", JsonContentType);
            request.AddHeader("Authorization", "Bearer " + SessionManager.LoggedUser.Token);
            var body = JsonConvert.SerializeObject(categoryDto);
            request.AddBody(body, JsonContentType);
            RestResponse restResponse = await client.ExecuteAsync(request);
            
            var responseObject = JsonConvert.DeserializeObject<ApiResult<CategoryDTO>>(restResponse.Content);

            return Json(new { success = true, data = responseObject.Data });
        }

        [HttpPost("/Admin/RemoveCategory/{categoryGUID}")]
        public async Task<IActionResult> RemoveCategory(Guid categoryGUID)
        {
            var url = $"{BaseUrl}/RemoveCategory/" + categoryGUID;
            var client = new RestClient(url);
            var request = new RestRequest(url,Method.Delete);
            request.AddHeader("Content-Type", JsonContentType);
            request.AddHeader("Authorization", "Bearer " + SessionManager.LoggedUser.Token);
            
            RestResponse restResponse = await client.ExecuteAsync(request);

            var responseObject = JsonConvert.DeserializeObject<ApiResult<bool>>(restResponse.Content);

            return Json(new { success = true });
        }
    }
}