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
    public class InspectionController : Controller
    {
        private readonly project_3Context _context;

        public InspectionController(project_3Context context)
        {
            _context = context;
        }

        // GET: Inspection
        public async Task<IActionResult> Index()
        {
            var project_3Context = _context.Inspections.Include(i => i.Object).Include(i => i.UserLoginNavigation);
            return View(await project_3Context.ToListAsync());
        }

        // GET: Inspection/Details/5
        public async Task<IActionResult> Details(uint? id)
        {
            if (id == null || _context.Inspections == null)
            {
                return NotFound();
            }

            var inspection = await _context.Inspections
                .Include(i => i.Object)
                .Include(i => i.UserLoginNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (inspection == null)
            {
                return NotFound();
            }

            return View(inspection);
        }

        // GET: Inspection/Create
        public IActionResult Create()
        {
            ViewData["ObjectId"] = new SelectList(_context.ObjectToChecks, "Id", "Id");
            ViewData["UserLogin"] = new SelectList(_context.Users, "Login", "Login");
            return View();
        }

        // POST: Inspection/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UserLogin,ObjectId,Timestamp,Reason,Observations,ChangeOfState,Inspectioncol")] Inspection inspection)
        {
            if (ModelState.IsValid)
            {
                _context.Add(inspection);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ObjectId"] = new SelectList(_context.ObjectToChecks, "Id", "Id", inspection.ObjectId);
            ViewData["UserLogin"] = new SelectList(_context.Users, "Login", "Login", inspection.UserLogin);
            return View(inspection);
        }

        // GET: Inspection/Edit/5
        public async Task<IActionResult> Edit(uint? id)
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
            ViewData["ObjectId"] = new SelectList(_context.ObjectToChecks, "Id", "Id", inspection.ObjectId);
            ViewData["UserLogin"] = new SelectList(_context.Users, "Login", "Login", inspection.UserLogin);
            return View(inspection);
        }

        // POST: Inspection/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(uint id, [Bind("Id,UserLogin,ObjectId,Timestamp,Reason,Observations,ChangeOfState,Inspectioncol")] Inspection inspection)
        {
            if (id != inspection.Id)
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
                    if (!InspectionExists(inspection.Id))
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
            ViewData["ObjectId"] = new SelectList(_context.ObjectToChecks, "Id", "Id", inspection.ObjectId);
            ViewData["UserLogin"] = new SelectList(_context.Users, "Login", "Login", inspection.UserLogin);
            return View(inspection);
        }

        // GET: Inspection/Delete/5
        public async Task<IActionResult> Delete(uint? id)
        {
            if (id == null || _context.Inspections == null)
            {
                return NotFound();
            }

            var inspection = await _context.Inspections
                .Include(i => i.Object)
                .Include(i => i.UserLoginNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (inspection == null)
            {
                return NotFound();
            }

            return View(inspection);
        }

        // POST: Inspection/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(uint id)
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

        private bool InspectionExists(uint id)
        {
            return (_context.Inspections?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
