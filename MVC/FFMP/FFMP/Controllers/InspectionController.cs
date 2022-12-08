using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FFMP.Data;
using Microsoft.Data.SqlClient;
using FFMP.Models;
using FFMP.BlobStorageServices;

namespace FFMP.Controllers
{
    public class InspectionController : Controller
    {
        private readonly project_3Context _context;
        private readonly IHttpContextAccessor _cntxt;
        private readonly IBlobStorageService _blobStorage;

        public InspectionController(project_3Context context, IHttpContextAccessor cntxt, IBlobStorageService blobStorage)
        {

            _context = context;
            _cntxt = cntxt;
            _blobStorage = blobStorage;
        }

        // GET: All Blob Files
        public async Task<IActionResult> Files()
        {
            return View(await _blobStorage.GetAllBlobFiles());
        }

        // Upload view
        [HttpGet]
        public IActionResult Upload()
        {
            return View();
        }

        // Upload a file
        [HttpPost]
        public async Task<IActionResult> Upload(IFormFile files)
        {
            await _blobStorage.UploadBlobFileAsync(files);
            return RedirectToAction("Files");
        }

        // Delete a file
        
        public async Task<IActionResult> DeleteFile(string blobName)
        {
            await _blobStorage.DeleteDocumentAsync(blobName);
            return RedirectToAction("Files", "Inspection");
        }
        
        public async Task<IActionResult> DownloadFile(string blobName)
        {
            await _blobStorage.DownloadDocumentAsync(blobName);
            return RedirectToAction("Files", "Inspection");
        }


        // GET: Inspection
        public async Task<IActionResult> Index(string SortOrder, string searchByCreator)
        {
            ViewData["TimestampSortParam"] = String.IsNullOrEmpty(SortOrder) ? "timestamp_sort" : "";
            ViewData["ObjectSortParam"] = SortOrder == "object_sort" ? "object_sort_desc" : "object_sort";

            var inspections = await _context.Inspections.Include(i => i.Object).Include(i => i.UserLoginNavigation).ToListAsync();


            if(!String.IsNullOrEmpty(searchByCreator))
            {
                inspections = await _context.Inspections.Include(i => i.Object).Include(i => i.UserLoginNavigation).Where(i => i.UserLoginNavigation.Name.Contains(searchByCreator)).ToListAsync();
            }
            switch (SortOrder)
            {

                case "timestamp_sort":
                    inspections = inspections.OrderByDescending(i => i.Timestamp).ToList();
                    break;
                case "object_sort":
                    inspections = inspections.OrderBy(i => i.ObjectId).ToList();
                    break;
                case "object_sort_desc":
                    inspections = inspections.OrderByDescending(i => i.ObjectId).ToList();
                    break;
                default:
                    inspections = inspections.OrderBy(i => i.Timestamp).ToList();
                    break;
            }

            return View(inspections);
        }

        // GET: Inspections of Object(id)
        public async Task<IActionResult> ObjectsInspections(uint? id)
        {
            var objInspections = await _context.Inspections.Include(i => i.Object).Include(i => i.UserLoginNavigation).Where(x=>x.ObjectId == id).ToListAsync();
            return View("ObjectsInspections", objInspections);
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
        public IActionResult Create(string id)
        {
            //if (id == null || _context.Inspections == null)
            //{
            //    return NotFound();
            //}

            //var loginUser = await _context.Inspections.Where(x => x.UserLogin == id).FirstOrDefaultAsync();
            //if (loginUser == null)
            //{
            //    return NotFound();
            //}
            Inspection inspection = new Inspection();
            ViewData["ObjectId"] = new SelectList(_context.ObjectToChecks, "Id", "Id");
            //ViewData["UserLogin"] = new SelectList(_context.Users, "Login", "Login");
            return PartialView("_CreatePartialView", inspection);
            
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
                inspection.UserLogin = _cntxt!.HttpContext.Session.GetString("userlogin").ToString();
                _context.Add(inspection);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            //ViewData["ObjectId"] = new SelectList(_context.ObjectToChecks, "Id", "Id", inspection.ObjectId);
            //ViewData["UserLogin"] = new SelectList(_context.Users, "Login", "Login", inspection.UserLogin);
            return View(inspection);
        }

        // GET: Inspection/Edit/5
        //public async Task<IActionResult> Edit(uint? id)
        //{
        //    if (id == null || _context.Inspections == null)
        //    {
        //        return NotFound();
        //    }

        //    //var inspection = await _context.Inspections.FindAsync(id);
        //    var inspection = await _context.Inspections
        //        .Include(i => i.Object)
        //        .Include(i => i.UserLoginNavigation)
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (inspection == null)
        //    {
        //        return NotFound();
        //    }
        //    ViewData["ObjectId"] = new SelectList(_context.ObjectToChecks, "Id", "Id", inspection.ObjectId);
        //    ViewData["UserLogin"] = new SelectList(_context.Users, "Login", "Login", inspection.UserLogin);
        //    return View(inspection);
        //}

        // POST: Inspection/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(uint id, [Bind("Id,UserLogin,ObjectId,Timestamp,Reason,Observations,ChangeOfState,Inspectioncol")] Inspection inspection)
        {

            var insp = await _context.Inspections.FindAsync(id);
            if (id != inspection.Id)
            {
                return NotFound();
            }
            if (insp == null)
            {
                return NotFound();
            }
            insp.Id = id;
            insp.UserLogin = inspection.UserLogin;
            insp.ObjectId = inspection.ObjectId;
            insp.Timestamp = inspection.Timestamp;
            insp.Reason = inspection.Reason;
            insp.Observations = inspection.Observations;
            insp.ChangeOfState = inspection.ChangeOfState;
            insp.Inspectioncol = inspection.Inspectioncol;
            
            //if (ModelState.IsValid)
            //{
            //    try
            //    {
                    _context.Update(insp);
                    await _context.SaveChangesAsync();
                //}
                //catch (DbUpdateConcurrencyException)
                //{
                //    if (!InspectionExists(inspection.Id))
                //    {
                //        return NotFound();
                //    }
                //    else
                //    {
                //        throw;
                //    }
                //}
                return RedirectToAction(nameof(Index));
            }
            //ViewData["ObjectId"] = new SelectList(_context.ObjectToChecks, "Id", "Id", inspection.ObjectId);
            //ViewData["UserLogin"] = new SelectList(_context.Users, "Login", "Login", inspection.UserLogin);
            //return RedirectToAction(nameof(Index));
        //}

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
