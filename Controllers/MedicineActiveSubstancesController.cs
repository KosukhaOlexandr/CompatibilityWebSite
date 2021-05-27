using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CompatibilityWebSite;

namespace CompatibilityWebSite.Controllers
{
    public class MedicineActiveSubstancesController : Controller
    {
        private readonly CompatibilityWebSiteContext _context;

        public MedicineActiveSubstancesController(CompatibilityWebSiteContext context)
        {
            _context = context;
        }

        // GET: MedicineActiveSubstances
        public async Task<IActionResult> Index()
        {
            var compatibilityWebSiteContext = _context.MedicineActiveSubstances.Include(m => m.ActiveSubstance).Include(m => m.Medicine);
            return View(await compatibilityWebSiteContext.ToListAsync());
        }

        // GET: MedicineActiveSubstances/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var medicineActiveSubstance = await _context.MedicineActiveSubstances
                .Include(m => m.ActiveSubstance)
                .Include(m => m.Medicine)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (medicineActiveSubstance == null)
            {
                return NotFound();
            }

            return View(medicineActiveSubstance);
        }

        // GET: MedicineActiveSubstances/Create
        public IActionResult Create()
        {
            ViewData["ActiveSubtanceId"] = new SelectList(_context.ActiveSubstances, "Id", "Name");
            ViewData["MedicineId"] = new SelectList(_context.Medicines, "Id", "Name");
            return View();
        }

        // POST: MedicineActiveSubstances/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,MedicineId,ActiveSubtanceId")] MedicineActiveSubstance medicineActiveSubstance)
        {
            if (ModelState.IsValid)
            {
                _context.Add(medicineActiveSubstance);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ActiveSubtanceId"] = new SelectList(_context.ActiveSubstances, "Id", "Name", medicineActiveSubstance.ActiveSubtanceId);
            ViewData["MedicineId"] = new SelectList(_context.Medicines, "Id", "CompanyName", medicineActiveSubstance.MedicineId);
            return View(medicineActiveSubstance);
        }

        // GET: MedicineActiveSubstances/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var medicineActiveSubstance = await _context.MedicineActiveSubstances.FindAsync(id);
            if (medicineActiveSubstance == null)
            {
                return NotFound();
            }
            ViewData["ActiveSubtanceId"] = new SelectList(_context.ActiveSubstances, "Id", "Name", medicineActiveSubstance.ActiveSubtanceId);
            ViewData["MedicineId"] = new SelectList(_context.Medicines, "Id", "CompanyName", medicineActiveSubstance.MedicineId);
            return View(medicineActiveSubstance);
        }

        // POST: MedicineActiveSubstances/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,MedicineId,ActiveSubtanceId")] MedicineActiveSubstance medicineActiveSubstance)
        {
            if (id != medicineActiveSubstance.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(medicineActiveSubstance);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MedicineActiveSubstanceExists(medicineActiveSubstance.Id))
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
            ViewData["ActiveSubtanceId"] = new SelectList(_context.ActiveSubstances, "Id", "Name", medicineActiveSubstance.ActiveSubtanceId);
            ViewData["MedicineId"] = new SelectList(_context.Medicines, "Id", "CompanyName", medicineActiveSubstance.MedicineId);
            return View(medicineActiveSubstance);
        }

        // GET: MedicineActiveSubstances/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var medicineActiveSubstance = await _context.MedicineActiveSubstances
                .Include(m => m.ActiveSubstance)
                .Include(m => m.Medicine)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (medicineActiveSubstance == null)
            {
                return NotFound();
            }

            return View(medicineActiveSubstance);
        }

        // POST: MedicineActiveSubstances/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var medicineActiveSubstance = await _context.MedicineActiveSubstances.FindAsync(id);
            _context.MedicineActiveSubstances.Remove(medicineActiveSubstance);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MedicineActiveSubstanceExists(int id)
        {
            return _context.MedicineActiveSubstances.Any(e => e.Id == id);
        }
    }
}
