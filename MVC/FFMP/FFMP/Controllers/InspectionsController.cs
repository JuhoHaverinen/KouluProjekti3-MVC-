using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FFMP.Data;

namespace FFMP.Controllers
{
    public class InspectionsController : Controller
    {
        private readonly project_3Context _context;

        public InspectionsController(project_3Context context)
        {
            _context = context;
        }

        // GET: Inspections
        public async Task<IActionResult> Index()
        {
            var project_3Context = _context.Inspections.Include(i => i.Object).Include(i => i.UserLoginNavigation);
            return View(await project_3Context.ToListAsync());
        }

        // GET: Inspections/Details/5
        public async Task<IActionResult> Details(DateTime? id)
        {
            if (id == null || _context.Inspections == null)
            {
                return NotFound();
            }

            var inspection = await _context.Inspections
                .Include(i => i.Object)
                .Include(i => i.UserLoginNavigation)
                .FirstOrDefaultAsync(m => m.Timestamp == id);
            if (inspection == null)
            {
                return NotFound();
            }

            return View(inspection);
        }

        // GET: Inspections/Create
        public IActionResult Create()
        {
            ViewData["ObjectId"] = new SelectList(_context.Objects, "Id", "Id");
            ViewData["UserLogin"] = new SelectList(_context.Users, "Login", "Login");
            return View();
        }

        // POST: Inspections/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Timestamp,UserLogin,ObjectId,Reason,Observations,ChangeOfState")] Inspection inspection)
        {
            if (ModelState.IsValid)
            {
                _context.Add(inspection);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ObjectId"] = new SelectList(_context.Objects, "Id", "Id", inspection.ObjectId);
            ViewData["UserLogin"] = new SelectList(_context.Users, "Login", "Login", inspection.UserLogin);
            return View(inspection);
        }

        // GET: Inspections/Edit/5
        public async Task<IActionResult> Edit(DateTime? id)
        {
            if (id == null || _context.Inspections == null)
            {
                return NotFound();
            }

            var inspection = await _context.Inspections.FindAsync(id);
            if (inspection == null)
            {
                return NotFound();
            }
            ViewData["ObjectId"] = new SelectList(_context.Objects, "Id", "Id", inspection.ObjectId);
            ViewData["UserLogin"] = new SelectList(_context.Users, "Login", "Login", inspection.UserLogin);
            return View(inspection);
        }

        // POST: Inspections/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(DateTime id, [Bind("Timestamp,UserLogin,ObjectId,Reason,Observations,ChangeOfState")] Inspection inspection)
        {
            if (id != inspection.Timestamp)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(inspection);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InspectionExists(inspection.Timestamp))
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
            ViewData["ObjectId"] = new SelectList(_context.Objects, "Id", "Id", inspection.ObjectId);
            ViewData["UserLogin"] = new SelectList(_context.Users, "Login", "Login", inspection.UserLogin);
            return View(inspection);
        }

        // GET: Inspections/Delete/5
        public async Task<IActionResult> Delete(DateTime? id)
        {
            if (id == null || _context.Inspections == null)
            {
                return NotFound();
            }

            var inspection = await _context.Inspections
                .Include(i => i.Object)
                .Include(i => i.UserLoginNavigation)
                .FirstOrDefaultAsync(m => m.Timestamp == id);
            if (inspection == null)
            {
                return NotFound();
            }

            return View(inspection);
        }

        // POST: Inspections/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(DateTime id)
        {
            if (_context.Inspections == null)
            {
                return Problem("Entity set 'project_3Context.Inspections'  is null.");
            }
            var inspection = await _context.Inspections.FindAsync(id);
            if (inspection != null)
            {
                _context.Inspections.Remove(inspection);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InspectionExists(DateTime id)
        {
          return (_context.Inspections?.Any(e => e.Timestamp == id)).GetValueOrDefault();
        }
    }
}
