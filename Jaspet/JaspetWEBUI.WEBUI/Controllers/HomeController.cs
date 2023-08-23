using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using JaspetWEBUI.Models;

namespace JaspetWEBUI.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    [HttpGet("/")]
    public IActionResult Index()
    {
        return View();
    }
    
}