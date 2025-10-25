using Microsoft.AspNetCore.Mvc;
using TachLayout.Models; // namespace model Product
using System.Linq;

namespace YourProjectNamespace.ViewComponents
{
    public class ProductListViewComponent : ViewComponent
    {
        private readonly QuanLyBanHangContext _context;

        public ProductListViewComponent(QuanLyBanHangContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke()
        {
            var products = _context.SanPhams.ToList();
            return View(products);
        }
    }
}
