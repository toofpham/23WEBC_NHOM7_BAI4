using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TachLayout.Models;

namespace TachLayout.Controllers
{
    [Route("")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        // Tiêm QuanLyBanHangContext vào để sử dụng tương tác với bảng SanPham
        private readonly QuanLyBanHangContext _context;

        public HomeController(ILogger<HomeController> logger , QuanLyBanHangContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet("")]
        public IActionResult Index()
        {
            ViewData["Title"] = "Trang chủ";
            ViewData["PageName"] = "Home";

            // Lấy danh sách sản phẩm từ cơ sở dữ liệu
            var products = _context.SanPhams.ToList();
            return View(products);
        }

        [HttpGet("about")]
        public IActionResult About()
        {
            ViewData["Title"] = "About";
            ViewData["PageName"] = "About";

            return View();
        }

        [HttpGet("contact")]
        public IActionResult Contact()
        {
            ViewData["Title"] = "Contact";
            ViewData["PageName"] = "Contact";

            return View();
        }

        [HttpGet("faqs")]
        public IActionResult Faqs()
        {
            ViewData["Title"] = "Faqs";
            ViewData["PageName"] = "Faqs";

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}