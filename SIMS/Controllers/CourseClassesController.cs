using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SIMS.Data;
using SIMS.Models;

namespace SIMS.Controllers
{
    public class CourseClassesController : Controller
    {
        private readonly SimsDbContext _context;

        public CourseClassesController(SimsDbContext context)
        {
            _context = context;
        }

        // GET: CourseClasses
        public async Task<IActionResult> Index()
        {
            var simsDbContext = _context.Classes
             .Where(c => !c.IsDeleted)                  // chỉ lấy các class chưa bị xóa
             .Include(c => c.Course)                     // include Course
             .Include(c => c.Faculty)                    // include Faculty
                 .ThenInclude(f => f.User);
            return View(await simsDbContext.ToListAsync());
        }

        // GET: CourseClasses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var courseClass = await _context.Classes
                .Where(c => !c.IsDeleted) // chỉ hiển thị false
                .Include(c => c.Course)
                .Include(c => c.Faculty)
                    .ThenInclude(f => f.User) // include User trong Faculty
                .FirstOrDefaultAsync(m => m.ClassId == id);
            if (courseClass == null)
            {
                return NotFound();
            }

            return View(courseClass);
        }

        // GET: CourseClasses/Create
        public IActionResult Create()
        {
            //ViewData["CourseId"] = new SelectList(_context.Courses, "CourseId", "CourseCode");
            //ViewData["FacultyId"] = new SelectList(_context.Faculties, "FacultyId", "FacultyId");

            ViewData["CourseId"] = new SelectList(_context.Courses.Where(c => !c.IsDeleted), "CourseId", "Name");
            ViewData["FacultyId"] = new SelectList(
                _context.Faculties
                    .Include(f => f.User)
                    .Where(f => !f.User.IsDeleted)
                    .Select(f => new { f.FacultyId, f.User.FullName }),
                "FacultyId",
                "FullName"
            );

            return View();
        }

        // POST: CourseClasses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ClassId,Name,CourseId,FacultyId,CreatedAt,IsDeleted")] CourseClass courseClass)
        {
            if (ModelState.IsValid)
            {
                _context.Add(courseClass);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            //ViewData["CourseId"] = new SelectList(_context.Courses, "CourseId", "CourseCode", courseClass.CourseId);
            //ViewData["FacultyId"] = new SelectList(_context.Faculties, "FacultyId", "FacultyId", courseClass.FacultyId);

            ViewData["CourseId"] = new SelectList(_context.Courses.Where(c => !c.IsDeleted), "CourseId", "Name");
            ViewData["FacultyId"] = new SelectList(
                _context.Faculties
                    .Include(f => f.User)
                    .Where(f => !f.User.IsDeleted)
                    .Select(f => new { f.FacultyId, f.User.FullName }),
                "FacultyId",
                "FullName"
            );

            return View(courseClass);
        }

        // GET: CourseClasses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var courseClass = await _context.Classes
                .Include(c => c.Course)       // include Course
                .Include(c => c.Faculty)      // include Faculty
                    .ThenInclude(f => f.User) // include User liên kết
                .FirstOrDefaultAsync(c => c.ClassId == id);
            if (courseClass == null)
            {
                return NotFound();
            }

            //ViewData["CourseId"] = new SelectList(_context.Courses, "CourseId", "CourseCode", courseClass.CourseId);
            //ViewData["FacultyId"] = new SelectList(_context.Faculties, "FacultyId", "FacultyId", courseClass.FacultyId);

            ViewData["CourseId"] = new SelectList(_context.Courses.Where(c => !c.IsDeleted), "CourseId", "Name");
            ViewData["FacultyId"] = new SelectList(
                _context.Faculties
                    .Include(f => f.User)
                    .Where(f => !f.User.IsDeleted)
                    .Select(f => new { f.FacultyId, f.User.FullName }),
                "FacultyId",
                "FullName"
            );

            return View(courseClass);
        }

        // POST: CourseClasses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ClassId,Name,CourseId,FacultyId,CreatedAt,IsDeleted")] CourseClass courseClass)
        {
            if (id != courseClass.ClassId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(courseClass);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CourseClassExists(courseClass.ClassId))
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

            //ViewData["CourseId"] = new SelectList(_context.Courses, "CourseId", "CourseCode", courseClass.CourseId);
            //ViewData["FacultyId"] = new SelectList(_context.Faculties, "FacultyId", "FacultyId", courseClass.FacultyId);

            ViewData["CourseId"] = new SelectList(_context.Courses.Where(c => !c.IsDeleted), "CourseId", "Name");
            ViewData["FacultyId"] = new SelectList(
                _context.Faculties
                    .Include(f => f.User)
                    .Where(f => !f.User.IsDeleted)
                    .Select(f => new { f.FacultyId, f.User.FullName }),
                "FacultyId",
                "FullName"
            );

            return View(courseClass);
        }

        // GET: CourseClasses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var courseClass = await _context.Classes
                .Include(c => c.Course)
                .Include(c => c.Faculty)
                .FirstOrDefaultAsync(m => m.ClassId == id);
            if (courseClass == null)
            {
                return NotFound();
            }

            return View(courseClass);
        }

        // POST: CourseClasses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var courseClass = await _context.Classes.FindAsync(id);
            if (courseClass != null)
            {
                _context.Classes.Remove(courseClass);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CourseClassExists(int id)
        {
            return _context.Classes.Any(e => e.ClassId == id);
        }
    }
}
