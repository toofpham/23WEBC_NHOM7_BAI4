using Microsoft.AspNetCore.Mvc;
using TachLayout.Models;
namespace TachLayout.Controllers
{
    [Route("products")]
    public class ProductController : Controller
    {
        // Tiêm QuanLyBanHangContext vào để sử dụng tương tác với bảng SanPham
        private readonly QuanLyBanHangContext _context;
        public ProductController(QuanLyBanHangContext context)
        {
            _context = context;
        }

        [HttpGet("")]
        public IActionResult Index()
        {
            ViewData["Title"] = "Sản phẩm";
            ViewData["PageName"] = "Products";

            // Lấy danh sách sản phẩm từ cơ sở dữ liệu
            var products = _context.SanPhams.ToList();
            return View(products);
        }

        [HttpGet("{id}")]
        public IActionResult Details(int id)
        {
            ViewData["Title"] = "Chi tiết sản phẩm";
            ViewData["PageName"] = "ProductDetails";

            // Tìm sản phẩm theo id
            var products = _context.SanPhams.FirstOrDefault(p => p.MaSp == id);
            if (products == null)
            {
                return NotFound();
            }
            return View(products);
        }
    }
}
