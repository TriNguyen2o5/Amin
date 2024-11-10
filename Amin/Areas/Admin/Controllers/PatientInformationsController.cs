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
    public class PatientInformationsController : Controller
    {
        private readonly PatientManagementContext _context;

        public PatientInformationsController(PatientManagementContext context)
        {
            _context = context;
        }

        // GET: Admin/PatientInformations
        public async Task<IActionResult> Index()
        {
            var patientManagementContext = _context.PatientInformations.Include(p => p.User);
            return View(await patientManagementContext.ToListAsync());
        }

        // GET: Admin/PatientInformations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var patientInformation = await _context.PatientInformations
                .Include(p => p.User)
                .FirstOrDefaultAsync(m => m.RecordId == id);
            if (patientInformation == null)
            {
                return NotFound();
            }

            return View(patientInformation);
        }

        // GET: Admin/PatientInformations/Create
        public IActionResult Create()
        {
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "UserId");
            return View();
        }

        // POST: Admin/PatientInformations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RecordId,Date,PhysicalActivityDuration,CaffeineIntake,SleepTime,WakeTime,UserId")] PatientInformation patientInformation)
        {
            if (ModelState.IsValid)
            {
                _context.Add(patientInformation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "UserId", patientInformation.UserId);
            return View(patientInformation);
        }

        // GET: Admin/PatientInformations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var patientInformation = await _context.PatientInformations.FindAsync(id);
            if (patientInformation == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "UserId", patientInformation.UserId);
            return View(patientInformation);
        }

        // POST: Admin/PatientInformations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RecordId,Date,PhysicalActivityDuration,CaffeineIntake,SleepTime,WakeTime,UserId")] PatientInformation patientInformation)
        {
            if (id != patientInformation.RecordId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(patientInformation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PatientInformationExists(patientInformation.RecordId))
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
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "UserId", patientInformation.UserId);
            return View(patientInformation);
        }

        // GET: Admin/PatientInformations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var patientInformation = await _context.PatientInformations
                .Include(p => p.User)
                .FirstOrDefaultAsync(m => m.RecordId == id);
            if (patientInformation == null)
            {
                return NotFound();
            }

            return View(patientInformation);
        }

        // POST: Admin/PatientInformations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var patientInformation = await _context.PatientInformations.FindAsync(id);
            if (patientInformation != null)
            {
                _context.PatientInformations.Remove(patientInformation);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PatientInformationExists(int id)
        {
            return _context.PatientInformations.Any(e => e.RecordId == id);
        }
    }
}
