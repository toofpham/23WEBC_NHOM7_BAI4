using Microsoft.AspNetCore.Mvc;
using TachLayout.Filters;

namespace TachLayout.Areas.Admin.Controllers
{
    [Area("Admin")]
    [AdminAuthorize] // Duy Khang edit
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            ViewData["Title"] = "Admin - Quản trị";
            return View();
        }
    }
}
