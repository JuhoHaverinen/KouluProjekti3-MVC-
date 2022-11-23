using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FFMP.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace FFMP.Controllers
{
    public class AuditingLogsController : Controller
    {
        private readonly project_3Context _context;

        public AuditingLogsController(project_3Context context)
        {
            _context = context;
        }

        // GET: AuditingLogs
        public async Task<IActionResult> Index()
        {
            var project_3Context = _context.AuditingLogs.Include(a => a.Object).Include(a => a.UserLoginNavigation);
            return View(await project_3Context.ToListAsync());
        }

        // GET: AuditingLogs/Details/5
        public async Task<IActionResult> Details(uint? id)
        {
            if (id == null || _context.AuditingLogs == null)
            {
                return NotFound();
            }

            var auditingLog = await _context.AuditingLogs
                .Include(a => a.Object)
                .Include(a => a.UserLoginNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (auditingLog == null)
            {
                return NotFound();
            }

            return View(auditingLog);
        }

        // GET: AuditingLogs/Create
        public IActionResult Create()
        {
            ViewData["ObjectId"] = new SelectList(_context.ObjectToChecks, "Id", "Id");
            ViewData["UserLogin"] = new SelectList(_context.Users, "Login", "Login");
            return View();
        }

        // POST: AuditingLogs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UserLogin,ObjectId,Created,Description,Result")] AuditingLog auditingLog)
        {
            _context.Add(auditingLog);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
            if (ModelState.IsValid)
            {
                _context.Add(auditingLog);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ObjectId"] = new SelectList(_context.ObjectToChecks, "Id", "Id", auditingLog.ObjectId);
            ViewData["UserLogin"] = new SelectList(_context.Users, "Login", "Login", auditingLog.UserLogin);
            return View(auditingLog);
        }

        // GET: AuditingLogs/Edit/5
        public async Task<IActionResult> Edit(uint? id)
        {
            if (id == null || _context.AuditingLogs == null)
            {
                return NotFound();
            }

            var auditingLog = await _context.AuditingLogs.FindAsync(id);
            if (auditingLog == null)
            {
                return NotFound();
            }
            ViewData["ObjectId"] = new SelectList(_context.ObjectToChecks, "Id", "Id", auditingLog.ObjectId);
            ViewData["UserLogin"] = new SelectList(_context.Users, "Login", "Login", auditingLog.UserLogin);
            return View(auditingLog);
        }

        // POST: AuditingLogs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(uint id, [Bind("Id,UserLogin,ObjectId,Created,Description,Result")] AuditingLog auditingLog)
        {
            if (id != auditingLog.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(auditingLog);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AuditingLogExists(auditingLog.Id))
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
            ViewData["ObjectId"] = new SelectList(_context.ObjectToChecks, "Id", "Id", auditingLog.ObjectId);
            ViewData["UserLogin"] = new SelectList(_context.Users, "Login", "Login", auditingLog.UserLogin);
            return View(auditingLog);
        }

        // GET: AuditingLogs/Delete/5
        public async Task<IActionResult> Delete(uint? id)
        {
            if (id == null || _context.AuditingLogs == null)
            {
                return NotFound();
            }

            var auditingLog = await _context.AuditingLogs
                .Include(a => a.Object)
                .Include(a => a.UserLoginNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (auditingLog == null)
            {
                return NotFound();
            }

            return View(auditingLog);
        }

        // POST: AuditingLogs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(uint id)
        {
            if (_context.AuditingLogs == null)
            {
                return Problem("Entity set 'project_3Context.AuditingLogs'  is null.");
            }
            var auditingLog = await _context.AuditingLogs.FindAsync(id);
            if (auditingLog != null)
            {
                _context.AuditingLogs.Remove(auditingLog);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AuditingLogExists(uint id)
        {
            return _context.AuditingLogs.Any(e => e.Id == id);
        }
    }
}
