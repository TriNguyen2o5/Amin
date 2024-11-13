using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Amin.Data;

namespace Amin.Areas.Admin.Controllers
{
    
    [Area("Admin")]
    public class UsersManagementController : Controller
    {
        private readonly PatientManagementContext _context;

        public UsersManagementController(PatientManagementContext context)
        {
            _context = context;
        }

        // GET: Admin/UsersManagement
        public async Task<IActionResult> Index()
        {
            return View(await _context.Users.ToListAsync());
        }
        public async Task<IActionResult> AdminDashboard()
        {
            var users = await _context.Users
                .OrderByDescending(p => p.UserId)
                .Take(30)
                .ToListAsync();
            ViewBag.Users = users;
            //// Đếm số lượng người có và không hút thuốc lá
            //int smokers = users.Count(u => u.SmokingStatus == true);
            //int nonSmokers = users.Count(u => u.SmokingStatus == false);

            //// Đếm số lượng người có và không uống rượu bia
            //int drinkers = users.Count(u => u.AlcoholicStatus == true);
            //int nonDrinkers = users.Count(u => u.AlcoholicStatus == false);

            //// Truyền dữ liệu qua ViewBag
            //ViewBag.SmokingData = new { smokers, nonSmokers };
            //ViewBag.AlcoholicData = new { drinkers, nonDrinkers };

            return View();
        }



        // GET: Admin/UsersManagement/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users
                .FirstOrDefaultAsync(m => m.UserId == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }
       

        // GET: Admin/UsersManagement/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/UsersManagement/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserId,FullName,Age,YearOfBirth,Address,Gender,Bmi,Password,SmokingStatus,AlcoholicStatus,Username")] User user)
        {
            if (ModelState.IsValid)
            {
                _context.Add(user);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }

        // GET: Admin/UsersManagement/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        // POST: Admin/UsersManagement/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("UserId,FullName,Age,YearOfBirth,Address,Gender,Bmi,Password,SmokingStatus,AlcoholicStatus,Username")] User user)
        {
            if (id != user.UserId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(user);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(user.UserId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }

        // GET: Admin/UsersManagement/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users
                .FirstOrDefaultAsync(m => m.UserId == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: Admin/UsersManagement/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user != null)
            {
                _context.Users.Remove(user);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.UserId == id);
        }
    }
}
