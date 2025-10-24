using Microsoft.AspNetCore.Mvc;
using System;
using TachLayout.Models;

namespace TachLayout.ViewComponents
{
    public class WebSettingViewComponent : ViewComponent
    {
        private readonly QuanLyBanHangContext _context;
        public WebSettingViewComponent(QuanLyBanHangContext context)
        {
            _context = context;
        }
        public IViewComponentResult Invoke()
        {
            // ---- Lấy record đầu tiên ----
            var setting = _context.WebSettings.FirstOrDefault();

            // ---- Gọi partial view _FooterPartial.cshtml và truyền model ----
            return View("~/Views/Shared/_FooterPartial.cshtml", setting);
        }
    }
}
