//Tạo ra bởi Duy Khang
using Microsoft.AspNetCore.Mvc;
using TachLayout.Models;

namespace TachLayout.Controllers
{
    public class AccountController : Controller
    {
        // Tiêm QuanLybanHangContext vào để sử dụng tương tác với bảng user
        private readonly QuanLyBanHangContext _context;

        public AccountController(QuanLyBanHangContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Login()
        {
            ViewBag.Title = "Login";
            return View();
        }

        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            var user = _context.Users.FirstOrDefault(u => u.UserName == username && u.Pass == password);
            if (user != null)
            {
                HttpContext.Session.SetString("UserName", user.UserName);
                HttpContext.Session.SetString("Role", user.Role);
                HttpContext.Session.SetInt32("UserId", user.UserId);

                return RedirectToAction("Index", "Home");
            }

            ViewBag.Error = "Tên đăng nhập hoặc mật khẩu sai!";
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }
    }
}
