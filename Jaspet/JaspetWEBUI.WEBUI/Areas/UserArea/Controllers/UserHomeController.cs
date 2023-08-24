using Microsoft.AspNetCore.Mvc;

namespace JaspetWEBUI.WEBUI.Areas.UserArea.Controllers;

using Core.DTO;
using Core.Result;
using Core.ViewModel;
using Helper.SessionHelper;
using Newtonsoft.Json;
using RestSharp;

[Area("UserArea")]
public class UserHomeController : Controller
{
    
    private const string BaseUrl = "http://localhost:5146";
    private const string JsonContentType = "application/json";
    
    public async Task<List<ProductDTO>> GetProductList()
    {
        var url = $"{BaseUrl}/Products";
        var client = new RestClient(url);
        var request = new RestRequest(url);
        request.AddHeader("Content-Type", JsonContentType);
        request.AddHeader("Authorization", "Bearer " + SessionManager.LoggedUser?.Token);
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
        request.AddHeader("Authorization", "Bearer " + SessionManager.LoggedUser?.Token);
        RestResponse restResponse = await client.ExecuteAsync(request);

        var responseObject = JsonConvert.DeserializeObject<ApiResult<List<CategoryDTO>>>(restResponse.Content);

        var categories = responseObject.Data;

        return categories;
    }
    
    public async Task<List<UserDTO>> GetUserList()
    {
        var url = $"{BaseUrl}/GetAllUser";
        var client = new RestClient(url);
        var request = new RestRequest(url);
        request.AddHeader("Content-Type", JsonContentType);
        request.AddHeader("Content-Type", "application/json");
        request.AddHeader("Authorization", "Bearer " + SessionManager.LoggedUser?.Token);
        RestResponse restResponse = await client.ExecuteAsync(request);

        var responseObject = JsonConvert.DeserializeObject<ApiResult<List<UserDTO>>>(restResponse.Content);

        var users = responseObject.Data;

        return users;
    }
    
    [HttpGet("/UserHome")]
    public async Task<IActionResult> Index()
    {
        HomeViewModel homeViewModel = new()
        {
            Products = await GetProductList(),
            Categories = await GetCategoryList(),
            Users = await GetUserList()
          
        };
        return View(homeViewModel);
    }
}