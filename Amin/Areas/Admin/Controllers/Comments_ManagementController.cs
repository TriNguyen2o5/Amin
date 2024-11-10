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
    public class Comments_ManagementController : Controller
    {
        private readonly PatientManagementContext _context;

        public Comments_ManagementController(PatientManagementContext context)
        {
            _context = context;
        }

        // GET: Admin/Comments_Management
        public async Task<IActionResult> Index()
        {
            var patientManagementContext = _context.Comments.Include(c => c.CommentAuthor).Include(c => c.Post);
            return View(await patientManagementContext.ToListAsync());
        }

        // GET: Admin/Comments_Management/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comment = await _context.Comments
                .Include(c => c.CommentAuthor)
                .Include(c => c.Post)
                .FirstOrDefaultAsync(m => m.CommentId == id);
            if (comment == null)
            {
                return NotFound();
            }

            return View(comment);
        }

        // GET: Admin/Comments_Management/Create
        public IActionResult Create()
        {
            ViewData["CommentAuthorId"] = new SelectList(_context.Users, "UserId", "UserId");
            ViewData["PostId"] = new SelectList(_context.Posts, "PostId", "PostId");
            return View();
        }

        // POST: Admin/Comments_Management/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CommentId,PostId,CommentContent,CommentAuthorId,CommentDate")] Comment comment)
        {
            if (ModelState.IsValid)
            {
                _context.Add(comment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CommentAuthorId"] = new SelectList(_context.Users, "UserId", "UserId", comment.CommentAuthorId);
            ViewData["PostId"] = new SelectList(_context.Posts, "PostId", "PostId", comment.PostId);
            return View(comment);
        }

        // GET: Admin/Comments_Management/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comment = await _context.Comments.FindAsync(id);
            if (comment == null)
            {
                return NotFound();
            }
            ViewData["CommentAuthorId"] = new SelectList(_context.Users, "UserId", "UserId", comment.CommentAuthorId);
            ViewData["PostId"] = new SelectList(_context.Posts, "PostId", "PostId", comment.PostId);
            return View(comment);
        }

        // POST: Admin/Comments_Management/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CommentId,PostId,CommentContent,CommentAuthorId,CommentDate")] Comment comment)
        {
            if (id != comment.CommentId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(comment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CommentExists(comment.CommentId))
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
            ViewData["CommentAuthorId"] = new SelectList(_context.Users, "UserId", "UserId", comment.CommentAuthorId);
            ViewData["PostId"] = new SelectList(_context.Posts, "PostId", "PostId", comment.PostId);
            return View(comment);
        }

        // GET: Admin/Comments_Management/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comment = await _context.Comments
                .Include(c => c.CommentAuthor)
                .Include(c => c.Post)
                .FirstOrDefaultAsync(m => m.CommentId == id);
            if (comment == null)
            {
                return NotFound();
            }

            return View(comment);
        }

        // POST: Admin/Comments_Management/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var comment = await _context.Comments.FindAsync(id);
            if (comment != null)
            {
                _context.Comments.Remove(comment);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CommentExists(int id)
        {
            return _context.Comments.Any(e => e.CommentId == id);
        }
    }
}
