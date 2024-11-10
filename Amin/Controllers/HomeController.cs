using Amin.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Amin.Controllers
{
    public class HomeController : Controller
    {
        private readonly PatientManagementContext _context;

        public HomeController(PatientManagementContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var posts = await _context.Posts
                .OrderByDescending(p => p.PostedDate)
                .Take(10) // Lấy 10 bài viết mới nhất
                .ToListAsync();
            return View(posts);
        }
    }
}
