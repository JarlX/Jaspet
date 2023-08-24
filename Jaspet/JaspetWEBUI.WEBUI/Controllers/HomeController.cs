using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using JaspetWEBUI.Models;

namespace JaspetWEBUI.Controllers;


public class HomeController : Controller
{
    
    private readonly IHttpContextAccessor _httpContextAccessor;

    public HomeController(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    [HttpGet("/")]
    public IActionResult Index()
    {
        _httpContextAccessor.HttpContext.Session.Clear();
        return View();
    }
    
}