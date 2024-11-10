using Microsoft.AspNetCore.Mvc;
using Amin.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.CodeAnalysis.Scripting;

namespace Amin.Controllers
{
    public class LoginController : Controller
    {
        private readonly PatientManagementContext _context;

        public LoginController(PatientManagementContext context)
        {
            _context = context;
        }

        // Trang đăng nhập
        public IActionResult Index()
        {
            ViewData["IsLoginPage"] = true;
            return View();
        }

        // Xử lý đăng nhập
        [HttpPost]
        public async Task<IActionResult> Login(string username, string password)
        {
            var user = await _context.Users
                .Where(u => u.Username == username && u.Password == password)
            .FirstOrDefaultAsync();

            if (user != null && BCrypt.Net.BCrypt.Verify(password, user.Password))
            {
                // Lưu thông tin người dùng vào session hoặc cookie
                HttpContext.Session.SetString("Username", user.Username);
                // Redirect đến trang chính hoặc trang quản lý người dùng
                return RedirectToAction("Index", "Home");
            }

            // Nếu đăng nhập thất bại, hiển thị thông báo lỗi
            ViewData["ErrorMessage"] = "Sai tên đăng nhập hoặc mật khẩu";
            return View("Index");
        }
        // GET: Account/Register
        public IActionResult Register()
        {
            return View();
        }

        // POST: Account/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register([Bind("FullName, Username, Password")] User user, string password)
        {
            if (ModelState.IsValid)
            {
                user.Password = BCrypt.Net.BCrypt.HashPassword(password);  // Hash password before saving
                _context.Add(user);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Login));
            }

            return View(user);
        }
    }

}
