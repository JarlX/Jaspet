using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace JaspetWEBUI.WEBUI.Areas.UserArea.Controllers
{
    [Area("UserArea")]
    public class CartController : Controller
    {
        [HttpGet("/User/Cart")]
        public IActionResult Index()
        {
            return View();
        }
    }
}