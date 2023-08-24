using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using JaspetWEBUI.Models;

namespace JaspetWEBUI.Controllers;


public class HomeController : Controller
{
    
    [HttpGet("/")]
    public IActionResult Index()
    {
        return View();
    }
    
}