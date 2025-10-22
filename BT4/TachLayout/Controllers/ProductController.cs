using Microsoft.AspNetCore.Mvc;
using TachLayout.Models;
namespace TachLayout.Controllers
{
    [Route("products")]
    public class ProductController : Controller
    {
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

            var products = _context.SanPhams.ToList();
            return View(products);
        }

        [HttpGet("{id}")]
        public IActionResult Details(int id)
        {
            ViewData["Title"] = "Chi tiết sản phẩm";
            ViewData["PageName"] = "ProductDetails";

            var products = _context.SanPhams.FirstOrDefault(p => p.MaSp == id);
            if (products == null)
            {
                return NotFound();
            }
            return View(products);
        }
    }
}
