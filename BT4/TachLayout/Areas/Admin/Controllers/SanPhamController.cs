using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TachLayout.Models;

namespace TachLayout.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SanPhamController : Controller
    {
        private readonly QuanLyBanHangContext _context;
        private readonly IWebHostEnvironment _env;

        public SanPhamController(QuanLyBanHangContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        // GET: /Admin/SanPham/Them
        public async Task<IActionResult> Them()
        {
            ViewBag.DanhMucs = await _context.DanhMucs.Where(dm => dm.TrangThai == 1).ToListAsync();
            return View();
        }

        // POST: /Admin/SanPham/Them
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Them(SanPham sp, IFormFile? uploadHinh)
        {
            // Kiểm tra quyền admin
            if (HttpContext.Session.GetString("Role") != "Admin")
                return RedirectToAction("AccessDenied", "Home");

            if (ModelState.IsValid)
            {
                // Upload ảnh nếu có
                if (uploadHinh != null && uploadHinh.Length > 0)
                {
                    string folder = Path.Combine(_env.WebRootPath, "images", "product");
                    if (!Directory.Exists(folder))
                        Directory.CreateDirectory(folder);

                    string fileName = Guid.NewGuid() + Path.GetExtension(uploadHinh.FileName);
                    string filePath = Path.Combine(folder, fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await uploadHinh.CopyToAsync(stream);
                    }

                    sp.Hinh = "/images/product/" + fileName;
                }

                _context.SanPhams.Add(sp);
                await _context.SaveChangesAsync();

                TempData["msg"] = "Thêm sản phẩm thành công!";
                return RedirectToAction(nameof(Them));
            }

            ViewBag.DanhMucs = await _context.DanhMucs.ToListAsync();
            return View(sp);
        }

        // POST AJAX: thêm danh mục mới
        [HttpPost]
        public async Task<IActionResult> ThemDanhMucAjax([FromBody] DanhMuc dm)
        {
            if (HttpContext.Session.GetString("Role") != "Admin")
                return Json(new { success = false, message = "Bạn không có quyền" });

            if (string.IsNullOrWhiteSpace(dm.TenDanhMuc))
                return Json(new { success = false, message = "Tên danh mục không hợp lệ" });

            dm.TrangThai = 1;
            _context.DanhMucs.Add(dm);
            await _context.SaveChangesAsync();

            return Json(new { success = true, id = dm.MaDanhMuc, name = dm.TenDanhMuc });
        }
    }
}
