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
    public class ProductController : Controller
    {
        private const string BaseUrl = "http://localhost:5146";
        private const string JsonContentType = "application/json";
        
        [HttpGet("/Admin/Products")]
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
    }
}