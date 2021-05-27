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
    public class DeseasesActiveSubstancesController : Controller
    {
        private readonly CompatibilityWebSiteContext _context;

        public DeseasesActiveSubstancesController(CompatibilityWebSiteContext context)
        {
            _context = context;
        }

        // GET: DeseasesActiveSubstances
        public async Task<IActionResult> Index()
        {
            var compatibilityWebSiteContext = _context.DeseasesActiveSubstances.Include(d => d.ActiveSubstance).Include(d => d.Desease);
            return View(await compatibilityWebSiteContext.ToListAsync());
        }

        // GET: DeseasesActiveSubstances/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var deseasesActiveSubstance = await _context.DeseasesActiveSubstances
                .Include(d => d.ActiveSubstance)
                .Include(d => d.Desease)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (deseasesActiveSubstance == null)
            {
                return NotFound();
            }

            return View(deseasesActiveSubstance);
        }

        // GET: DeseasesActiveSubstances/Create
        public IActionResult Create()
        {
            ViewData["ActiveSubstanceId"] = new SelectList(_context.ActiveSubstances, "Id", "Name");
            ViewData["DeseaseId"] = new SelectList(_context.Deseases, "Id", "Name");
            return View();
        }

        // POST: DeseasesActiveSubstances/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,DeseaseId,ActiveSubstanceId")] DeseasesActiveSubstance deseasesActiveSubstance)
        {
            if (ModelState.IsValid)
            {
                _context.Add(deseasesActiveSubstance);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ActiveSubstanceId"] = new SelectList(_context.ActiveSubstances, "Id", "Name", deseasesActiveSubstance.ActiveSubstanceId);
            ViewData["DeseaseId"] = new SelectList(_context.Deseases, "Id", "Name", deseasesActiveSubstance.DeseaseId);
            return View(deseasesActiveSubstance);
        }

        // GET: DeseasesActiveSubstances/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var deseasesActiveSubstance = await _context.DeseasesActiveSubstances.FindAsync(id);
            if (deseasesActiveSubstance == null)
            {
                return NotFound();
            }
            ViewData["ActiveSubstanceId"] = new SelectList(_context.ActiveSubstances, "Id", "Name", deseasesActiveSubstance.ActiveSubstanceId);
            ViewData["DeseaseId"] = new SelectList(_context.Deseases, "Id", "Name", deseasesActiveSubstance.DeseaseId);
            return View(deseasesActiveSubstance);
        }

        // POST: DeseasesActiveSubstances/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,DeseaseId,ActiveSubstanceId")] DeseasesActiveSubstance deseasesActiveSubstance)
        {
            if (id != deseasesActiveSubstance.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(deseasesActiveSubstance);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DeseasesActiveSubstanceExists(deseasesActiveSubstance.Id))
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
            ViewData["ActiveSubstanceId"] = new SelectList(_context.ActiveSubstances, "Id", "Name", deseasesActiveSubstance.ActiveSubstanceId);
            ViewData["DeseaseId"] = new SelectList(_context.Deseases, "Id", "Name", deseasesActiveSubstance.DeseaseId);
            return View(deseasesActiveSubstance);
        }

        // GET: DeseasesActiveSubstances/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var deseasesActiveSubstance = await _context.DeseasesActiveSubstances
                .Include(d => d.ActiveSubstance)
                .Include(d => d.Desease)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (deseasesActiveSubstance == null)
            {
                return NotFound();
            }

            return View(deseasesActiveSubstance);
        }

        // POST: DeseasesActiveSubstances/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var deseasesActiveSubstance = await _context.DeseasesActiveSubstances.FindAsync(id);
            _context.DeseasesActiveSubstances.Remove(deseasesActiveSubstance);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DeseasesActiveSubstanceExists(int id)
        {
            return _context.DeseasesActiveSubstances.Any(e => e.Id == id);
        }
    }
}
