using System;
using TachLayout.Models;


namespace TachLayout.Services
{
    public class WebSettingService
    {
        private readonly QuanLyBanHangContext _context;

        public WebSettingService(QuanLyBanHangContext context)
        {
            _context = context;
        }

        // ---- Lấy setting ----
        public WebSetting GetWebSetting()
        {
            return _context.WebSettings.FirstOrDefault();
        }

        // ---- Cập nhật setting ----
        public void UpdateWebSetting(WebSetting model)
        {
            // ---- Sử dụng Linq FirstOrDefault ----
            var existing = _context.WebSettings.FirstOrDefault();
            if (existing != model)
            {
                // ---- Cập nhật các trường ----
                existing.TenSite = model.TenSite;
                existing.Logo = model.Logo;
                existing.DiaChi = model.DiaChi;
                existing.Email = model.Email;
                existing.HotLine = model.HotLine;

                // ---- Lưu thay đổi vào DB ----
                _context.SaveChanges();
            }
        }
    }
}
