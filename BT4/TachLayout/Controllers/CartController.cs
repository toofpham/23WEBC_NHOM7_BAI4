using Microsoft.AspNetCore.Mvc;
using TachLayout.Models;
using TachLayout.Extensions;
namespace TachLayout.Controllers
{
    [Route("cart")]
    public class CartController : Controller
    {
        private readonly QuanLyBanHangContext _context;

        public CartController(QuanLyBanHangContext context)
        {
            _context = context;
        }
        private List<SanPhamGio> Cart
        {
            get
            {
                var cart = HttpContext.Session.GetObject<List<SanPhamGio>>("Cart");
                if (cart == null)
                {
                    cart = new List<SanPhamGio>();
                    HttpContext.Session.SetObject("Cart", cart);
                }
                return cart;
            }
        }

        [HttpGet("add/{id}")]
        public IActionResult AddToCart(int id)
        {
            var product = _context.SanPhams.FirstOrDefault(p => p.MaSp == id);
            var cart = Cart;
            var item = cart.FirstOrDefault(p => p.MaSp == id);

            decimal giaBan = product.GiaKm.HasValue && product.GiaKm.Value > 0
                ? product.GiaKm.Value
                : product.Gia;

            if (item == null)
            {
                cart.Add(new SanPhamGio
                {
                    MaSp = product.MaSp,
                    TenSp = product.TenSp,
                    Hinh = product.Hinh,
                    Gia = giaBan,
                    SoLuong = 1
                });
            }
            else
            {
                item.SoLuong++;
            }

            HttpContext.Session.SetObject("Cart", cart);
            return RedirectToAction("Index");
        }
        [HttpGet("remove/{id}")]
        public IActionResult Remove(int id)
        {

            var cart = Cart;

            if (cart != null && cart.Count > 0)
            {
                var item = cart.FirstOrDefault(p => p.MaSp == id);
                if (item != null)
                {
                    cart.Remove(item);
                    HttpContext.Session.SetObject("Cart", cart);
                }
            }

            return RedirectToAction("Index");


        }

        [HttpPost("update")]
        public IActionResult UpdateCart(int id, int quantity)
        {

            var cart = Cart;
            var item = cart?.FirstOrDefault(p => p.MaSp == id);

            if (item != null)
            {
                if (quantity <= 0)
                {

                    cart.Remove(item);
                }
                else
                {

                    item.SoLuong = quantity;
                }

                HttpContext.Session.SetObject("Cart", cart);
            }

            return RedirectToAction("Index");


        }
        [HttpGet("increase/{id}")]
        public IActionResult Increase(int id)
        {
            var cart = Cart;
            var item = cart?.FirstOrDefault(p => p.MaSp == id);

            if (item != null)
            {
                item.SoLuong++;
                HttpContext.Session.SetObject("Cart", cart);
            }

            return RedirectToAction("Index");
        }

        [HttpGet("decrease/{id}")]
        public IActionResult Decrease(int id)
        {
            var cart = Cart;
            var item = cart?.FirstOrDefault(p => p.MaSp == id);

            if (item != null)
            {
                if (item.SoLuong > 1)
                {
                    item.SoLuong--;
                }
                else
                {
                    cart.Remove(item);
                }
                HttpContext.Session.SetObject("Cart", cart);
            }

            return RedirectToAction("Index");
        }

        [HttpGet("create-order")]
        public IActionResult CreateOrder()
        {
            
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var cart = Cart;
            if (!cart.Any())
            {
                return RedirectToAction("Index");
            }


            
            var user = _context.Users.Find(userId);
            if (user == null)
            {
                HttpContext.Session.Clear(); 
                return RedirectToAction("Login", "Account");
            }

            
            var hoaDon = new HoaDon
            {
                NgayLap = DateTime.Now,
                TongTien = cart.Sum(x => x.ThanhTien),
                TrangThai = "Chờ xác nhận",
                TenNguoiNhan = user.UserName, 
                Sdt = "Chưa cập nhật",
                DiaChi = "Chưa cập nhật",
                UserId = userId
            };

            _context.HoaDons.Add(hoaDon);
            _context.SaveChanges();

            // Thêm chi tiết hóa đơn
            foreach (var item in cart)
            {
                _context.ChiTietHoaDons.Add(new ChiTietHoaDon
                {
                    MaHd = hoaDon.MaHd,
                    MaSp = item.MaSp,
                    SoLuong = item.SoLuong,
                    DonGia = item.Gia,
                    ThanhTien = item.ThanhTien
                });
            }

            _context.SaveChanges();  
            
            HttpContext.Session.Remove("Cart");
 
            return RedirectToAction("Index");
        }

        [HttpGet("")]
        public IActionResult Index()
        {


            return View(Cart);
        }
    }
}
