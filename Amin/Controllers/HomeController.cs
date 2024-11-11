using Amin.Data;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;

namespace Amin.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly PatientManagementContext _context;

        public HomeController(ILogger<HomeController> logger, PatientManagementContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            ViewData["IsAuthenticated"] = HttpContext.Session.GetString("Username") != null;
            var posts = await _context.Posts
                .OrderByDescending(p => p.PostedDate)
                .Take(10)
                .ToListAsync();
            return View(posts);
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await _context.Posts
                .FirstOrDefaultAsync(m => m.PostId == id);
            if (post == null)
            {
                return NotFound();
            }

            return View(post);
        }
        public IActionResult Register() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(User user)
        {
            if (ModelState.IsValid)
            {
                var existingUser = await _context.Users
                    .FirstOrDefaultAsync(u => u.Username == user.Username);

                if (existingUser == null)
                {
                    _context.Users.Add(user);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Login");
                }
                else
                {
                    ViewBag.Error = "Username already exists.";
                }
            }
            return View(user);
        }

        public IActionResult Login() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(string username, string password)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Username == username && u.Password == password);

            if (user != null)
            {
                HttpContext.Session.SetString("Username", user.Username);
                ViewBag.User = user;
                return RedirectToAction("UserDashboard","Home");
            }
            else
            {
                ViewBag.Error = "Invalid username or password.";
                return View();
            }
        }

        public IActionResult UserDashboard()
        {
            // Lấy giá trị của "Username" từ session
            var username = HttpContext.Session.GetString("Username");

            if (string.IsNullOrEmpty(username))
            {
                // Nếu "Username" là null hoặc empty, chuyển hướng về trang đăng nhập
                return RedirectToAction("Login");
            }

            // Nếu người dùng đã đăng nhập, lấy thông tin người dùng từ cơ sở dữ liệu
            var user = _context.Users.FirstOrDefault(u => u.Username == username);
            ViewBag.username = username;
            if (user != null)
            {
                // Lưu thông tin người dùng vào ViewBag để sử dụng trong View
                ViewBag.User = user;
                ViewData["IsAuthenticated"] = true;
                return View();
            }

            // Nếu không tìm thấy người dùng, chuyển hướng về trang đăng nhập
            return RedirectToAction("Login");
        }


        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}