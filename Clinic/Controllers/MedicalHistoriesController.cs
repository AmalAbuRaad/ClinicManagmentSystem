using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Clinic.Data;
using Clinic.Models;

namespace Clinic.Controllers
{
    public class MedicalHistoriesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MedicalHistoriesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: MedicalHistories
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.MedicalHistories.Include(m => m.Patient);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: MedicalHistories/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var medicalHistory = await _context.MedicalHistories
                .Include(m => m.Patient)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (medicalHistory == null)
            {
                return NotFound();
            }

            return View(medicalHistory);
        }

        // GET: MedicalHistories/Create
        public IActionResult Create()
        {
            ViewData["PatientId"] = new SelectList(_context.Patients, "Id", "PatientName");
            return View();
        }

        // POST: MedicalHistories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,PatientId,Description")] MedicalHistory medicalHistory)
        {
            if (ModelState.IsValid)
            {
                _context.Add(medicalHistory);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PatientId"] = new SelectList(_context.Patients, "Id", "PatientName", medicalHistory.PatientId);
            return View(medicalHistory);
        }

        // GET: MedicalHistories/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var medicalHistory = await _context.MedicalHistories.FindAsync(id);
            if (medicalHistory == null)
            {
                return NotFound();
            }
            ViewData["PatientId"] = new SelectList(_context.Patients, "Id", "PatientName", medicalHistory.PatientId);
            return View(medicalHistory);
        }

        // POST: MedicalHistories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,PatientId,Description")] MedicalHistory medicalHistory)
        {
            if (id != medicalHistory.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(medicalHistory);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MedicalHistoryExists(medicalHistory.Id))
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
            ViewData["PatientId"] = new SelectList(_context.Patients, "Id", "PatientName", medicalHistory.PatientId);
            return View(medicalHistory);
        }

        // GET: MedicalHistories/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var medicalHistory = await _context.MedicalHistories
                .Include(m => m.Patient)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (medicalHistory == null)
            {
                return NotFound();
            }

            return View(medicalHistory);
        }

        // POST: MedicalHistories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var medicalHistory = await _context.MedicalHistories.FindAsync(id);
            _context.MedicalHistories.Remove(medicalHistory);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MedicalHistoryExists(long id)
        {
            return _context.MedicalHistories.Any(e => e.Id == id);
        }
    }
}
