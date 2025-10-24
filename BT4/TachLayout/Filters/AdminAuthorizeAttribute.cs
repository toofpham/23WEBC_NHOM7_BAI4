using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace TachLayout.Filters
{
    public class AdminAuthorizeAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var role = context.HttpContext.Session.GetString("Role"); // Lấy Role từ session 
            if (string.IsNullOrEmpty(role) || role != "Admin") // nếu role k phải admin hoặc rỗng thì đưa về đăng nhập.
            {
                context.Result = new RedirectToActionResult("Login", "Account", null);
            }
        }
    }
}
// Duy Khang crate
// Tác dụng là một lớp phủ bảo vệ cho action của controller admin, sử dụng bằng các đặt [Adminauthorze] trước một action