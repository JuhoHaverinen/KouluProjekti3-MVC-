﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FFMP.Data;
using Microsoft.AspNetCore.Identity;

namespace FFMP.Controllers
{
    public class ObjectToCheckController : Controller
    {
        private readonly project_3Context _context;
        private readonly IHttpContextAccessor _cntxt;

        public ObjectToCheckController(project_3Context context, IHttpContextAccessor cntxt)
        {
            _context = context;
            _cntxt = cntxt;
        }

        // GET: ObjectToCheck
        public async Task<IActionResult> Index()
        {
            if (!UsersController.UserAuthenticated(_cntxt))
                return RedirectToAction("Login", "Users");

            var project_3Context = _context.ObjectToChecks.Include(o => o.TargetGroup).Include(o => o.UserLoginNavigation);
            return View(await project_3Context.ToListAsync());
        }

        // GET: ObjectToCheck/Details/5
        public async Task<IActionResult> Details(uint? id)
        {
            if (id == null || _context.ObjectToChecks == null)
            {
                return NotFound();
            }

            var objectToCheck = await _context.ObjectToChecks
                .Include(o => o.TargetGroup)
                .Include(o => o.UserLoginNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (objectToCheck == null)
            {
                return NotFound();
            }

            return View(objectToCheck);
        }
        // POST: InspectionCreate
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateInspection([Bind("Id,UserLogin,ObjectId,/*Timestamp*/,Reason,Observations,ChangeOfState,Inspectioncol")] Inspection inspection)
        {
            var insp = new Inspection();
            insp.UserLogin = _cntxt!.HttpContext.Session.GetString("userlogin");
            insp.ObjectId = inspection.ObjectId;
            insp.Reason = inspection.Reason;
            insp.Observations = inspection.Observations;
            insp.ChangeOfState = inspection.ChangeOfState;
            insp.Inspectioncol = inspection.Inspectioncol;
            
            
            if (insp != null) /*ModelState.IsValid*/
            {
                
                _context.Add(insp);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(InspectionController.Index), "Inspection");
            }
            //ViewData["ObjectId"] = new SelectList(_context.ObjectToChecks, "Id", "Id", inspection.ObjectId);
            //ViewData["UserLogin"] = new SelectList(_context.Users, "Login", "Login", inspection.UserLogin);
            return RedirectToAction(nameof(Index));
        }

        // GET: ObjectToCheck/Create
        public IActionResult Create()
        {
            ViewData["TargetGroupId"] = new SelectList(_context.TargetGroups, "Id", "Id");
            ViewData["UserLogin"] = new SelectList(_context.Users, "Login", "Login");
            return View();
        }

        // POST: ObjectToCheck/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserLogin,TargetGroupId,Name,Description,Location,Type,Model")] ObjectToCheck objectToCheck)
        {
            if (ModelState.IsValid)
            {
                _context.Add(objectToCheck);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["TargetGroupId"] = new SelectList(_context.TargetGroups, "Id", "Id", objectToCheck.TargetGroupId);
            ViewData["UserLogin"] = new SelectList(_context.Users, "Login", "Login", objectToCheck.UserLogin);
            return View(objectToCheck);
        }

        // GET: ObjectToCheck/Edit/5
        public async Task<IActionResult> Edit(uint? id)
        {
            if (id == null || _context.ObjectToChecks == null)
            {
                return NotFound();
            }

            var objectToCheck = await _context.ObjectToChecks.FindAsync(id);
            if (objectToCheck == null)
            {
                return NotFound();
            }
            ViewData["TargetGroupId"] = new SelectList(_context.TargetGroups, "Id", "Id", objectToCheck.TargetGroupId);
            ViewData["UserLogin"] = new SelectList(_context.Users, "Login", "Login", objectToCheck.UserLogin);
            return View(objectToCheck);
        }

        // POST: ObjectToCheck/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(uint id, [Bind("Id,UserLogin,TargetGroupId,Name,Description,Location,Type,Model,State")] ObjectToCheck objectToCheck)
        {
            var otc = await _context.ObjectToChecks.FindAsync(id);
            if (null == otc)
            {
                return NotFound();
            }

            otc.Id = id;
            otc.UserLogin = objectToCheck.UserLogin;
            otc.TargetGroupId = objectToCheck.TargetGroupId;
            otc.Name = objectToCheck.Name;
            otc.Description = objectToCheck.Description;
            otc.Location = objectToCheck.Location;
            otc.Type = objectToCheck.Type;
            otc.Model = objectToCheck.Model;
            otc.State = objectToCheck.State;
            //otc.Created = objectToCheck.Created;


            //if (ModelState.IsValid)
            //{
            //    try
            //    {
            //_context.Update(objectToCheck);
            await _context.SaveChangesAsync();
            //}
            //catch (DbUpdateConcurrencyException)
            //{
            //    if (!ObjectToCheckExists(objectToCheck.Id))
            //    {
            //        return NotFound();
            //    }
            //    else
            //    {
            //        throw;
            //    }
            //}
            return RedirectToAction(nameof(Index));
            //}
            //ViewData["TargetGroupId"] = new SelectList(_context.TargetGroups, "Id", "Id", objectToCheck.TargetGroupId);
            //ViewData["UserLogin"] = new SelectList(_context.Users, "Login", "Login", objectToCheck.UserLogin);
            //return View(objectToCheck);
        }

        // GET: ObjectToCheck/Delete/5
        public async Task<IActionResult> Delete(uint? id)
        {
            if (id == null || _context.ObjectToChecks == null)
            {
                return NotFound();
            }

            var objectToCheck = await _context.ObjectToChecks
                .Include(o => o.TargetGroup)
                .Include(o => o.UserLoginNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (objectToCheck == null)
            {
                return NotFound();
            }

            return View(objectToCheck);
        }

        // POST: ObjectToCheck/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(uint id)
        {
            if (_context.ObjectToChecks == null)
            {
                return Problem("Entity set 'project_3Context.Objects'  is null.");
            }
            var objectToCheck = await _context.ObjectToChecks.FindAsync(id);
            if (objectToCheck != null)
            {
                _context.ObjectToChecks.Remove(objectToCheck);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ObjectToCheckExists(uint id)
        {
            return (_context.ObjectToChecks?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
