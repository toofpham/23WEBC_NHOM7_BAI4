using Microsoft.AspNetCore.Mvc;

namespace TachLayout.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            ViewData["Title"] = "Admin - Quản trị";
            return View();
        }
    }
}
