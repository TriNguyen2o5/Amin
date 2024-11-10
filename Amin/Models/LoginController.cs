using Microsoft.AspNetCore.Mvc;

namespace Amin.Models
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            ViewData["IsLoginPage"] = true;
            return View();
        }
    }
}
