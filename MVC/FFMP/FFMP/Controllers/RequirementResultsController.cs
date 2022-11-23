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
    public class RequirementResultsController : Controller
    {
        private readonly project_3Context _context;

        public RequirementResultsController(project_3Context context)
        {
            _context = context;
        }

        // GET: RequirementResults
        public async Task<IActionResult> Index()
        {
            var project_3Context = _context.RequirementResults.Include(r => r.AuditingLogs);
            return View(await project_3Context.ToListAsync());
        }

        // GET: RequirementResults/Details/5
        public async Task<IActionResult> Details(uint? id)
        {
            if (id == null || _context.RequirementResults == null)
            {
                return NotFound();
            }

            var requirementResult = await _context.RequirementResults
                .Include(r => r.AuditingLogs)
                .FirstOrDefaultAsync(m => m.RequirementId == id);
            if (requirementResult == null)
            {
                return NotFound();
            }

            return View(requirementResult);
        }

        // GET: RequirementResults/Create
        public IActionResult Create()
        {
            ViewData["AuditingLogsId"] = new SelectList(_context.AuditingLogs, "Id", "Id");
            return View();
        }

        // POST: RequirementResults/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RequirementId,AuditingLogsId,Description,Must,Result")] RequirementResult requirementResult)
        {
            _context.Add(requirementResult);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

            if (ModelState.IsValid)
            {
                _context.Add(requirementResult);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AuditingLogsId"] = new SelectList(_context.AuditingLogs, "Id", "Id", requirementResult.AuditingLogsId);
            return View(requirementResult);
        }

        // GET: RequirementResults/Edit/5
        public async Task<IActionResult> Edit(uint? id)
        {
            if (id == null || _context.RequirementResults == null)
            {
                return NotFound();
            }

            var requirementResult = await _context.RequirementResults.FindAsync(id);
            if (requirementResult == null)
            {
                return NotFound();
            }
            ViewData["AuditingLogsId"] = new SelectList(_context.AuditingLogs, "Id", "Id", requirementResult.AuditingLogsId);
            return View(requirementResult);
        }

        // POST: RequirementResults/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(uint id, [Bind("RequirementId,AuditingLogsId,Description,Must,Result")] RequirementResult requirementResult)
        {
            if (id != requirementResult.RequirementId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(requirementResult);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RequirementResultExists(requirementResult.RequirementId))
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
            ViewData["AuditingLogsId"] = new SelectList(_context.AuditingLogs, "Id", "Id", requirementResult.AuditingLogsId);
            return View(requirementResult);
        }

        // GET: RequirementResults/Delete/5
        public async Task<IActionResult> Delete(uint? id)
        {
            if (id == null || _context.RequirementResults == null)
            {
                return NotFound();
            }

            var requirementResult = await _context.RequirementResults
                .Include(r => r.AuditingLogs)
                .FirstOrDefaultAsync(m => m.RequirementId == id);
            if (requirementResult == null)
            {
                return NotFound();
            }

            return View(requirementResult);
        }

        // POST: RequirementResults/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(uint id)
        {
            if (_context.RequirementResults == null)
            {
                return Problem("Entity set 'project_3Context.RequirementResults'  is null.");
            }
            var requirementResult = await _context.RequirementResults.FindAsync(id);
            if (requirementResult != null)
            {
                _context.RequirementResults.Remove(requirementResult);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RequirementResultExists(uint id)
        {
            return _context.RequirementResults.Any(e => e.RequirementId == id);
        }
    }
}
