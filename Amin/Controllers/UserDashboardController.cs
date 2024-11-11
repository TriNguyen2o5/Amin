using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Amin.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

[Authorize]
public class UserDashboardController : Controller
{
    private readonly PatientManagementContext _context;

    public UserDashboardController(PatientManagementContext context)
    {
        _context = context;
    }

    public IActionResult UserDashboard()
    {
        // Lấy UserId từ session
        int? userId = HttpContext.Session.GetInt32("UserId");

        if (userId == null)
        {
            // Nếu không có UserId trong session, chuyển hướng về trang đăng nhập
            return RedirectToAction("Login");
        }

        // Truyền UserId vào View thông qua ViewBag
        ViewBag.UserId = userId;

        return View();
    }






}
