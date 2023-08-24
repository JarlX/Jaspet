using Microsoft.AspNetCore.Mvc;

namespace JaspetWEBUI.Controllers;

using Core.DTO;
using Core.Result;
using Helper.SessionHelper;
using Newtonsoft.Json;
using RestSharp;


public class LoginController : Controller
{
    
    private readonly IHttpContextAccessor _httpContextAccessor;

    public LoginController(IHttpContextAccessor contextAccessor)
    {
        _httpContextAccessor = contextAccessor;
    }
    
    [HttpGet("/Login")]
    public IActionResult Index()
    {
        _httpContextAccessor.HttpContext.Session.Clear();
        return View();
    }

    [HttpPost("/Login")]
    public async Task<IActionResult> Login(LoginDTO loginDto)
    {
        var url = "http://localhost:5146/Login";
        var client = new RestClient(url);
        var request = new RestRequest(url, Method.Post);
        request.AddHeader("Content-Type", "application/json");
        var body = JsonConvert.SerializeObject(loginDto);
        request.AddBody(body, "application/json");
        RestResponse restResponse = await client.ExecuteAsync(request);

        var responseObject = JsonConvert.DeserializeObject<ApiResult<LoginDTO>>(restResponse.Content);

        SessionManager.LoggedUser = responseObject.Data;

        if (SessionManager.LoggedUser.UserRole == "Admin")
        {
            return RedirectToAction("Index", "AdminHome");
        }

        return RedirectToAction("Index", "UserHome");
    }
    
}