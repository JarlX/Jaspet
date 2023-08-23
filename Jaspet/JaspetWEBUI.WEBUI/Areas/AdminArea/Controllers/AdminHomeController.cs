using Microsoft.AspNetCore.Mvc;

namespace JaspetWEBUI.WEBUI.Areas.AdminArea.Controllers;

using Core.ViewModel;
using Helper.SessionHelper;

[Area("AdminArea")]
public class AdminHomeController : Controller
{

    private readonly IHttpContextAccessor _httpContextAccessor;

    public AdminHomeController(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    [HttpGet("/AdminHome")]
    public IActionResult Index()
    {
        UserViewModel user = new UserViewModel()
        {
            FullName = SessionManager.LoggedUser.FullName,
            UserId = SessionManager.LoggedUser.UserId,
            Email = SessionManager.LoggedUser.Email,
            Address = SessionManager.LoggedUser.Address

        };
        return View(user);
    }
}