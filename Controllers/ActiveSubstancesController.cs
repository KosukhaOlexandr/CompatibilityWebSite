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
    public class ActiveSubstancesController : Controller
    {
        private readonly CompatibilityWebSiteContext _context;

        public ActiveSubstancesController(CompatibilityWebSiteContext context)
        {
            _context = context;
        }

        // GET: ActiveSubstances
        public async Task<IActionResult> Index()
        {
            return View(await _context.ActiveSubstances.ToListAsync());
        }

        // GET: ActiveSubstances/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var activeSubstance = await _context.ActiveSubstances
                .FirstOrDefaultAsync(m => m.Id == id);
            if (activeSubstance == null)
            {
                return NotFound();
            }

            return View(activeSubstance);
        }

        // GET: ActiveSubstances/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ActiveSubstances/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Info")] ActiveSubstance activeSubstance)
        {
            if (ModelState.IsValid)
            {
                _context.Add(activeSubstance);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(activeSubstance);
        }

        // GET: ActiveSubstances/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var activeSubstance = await _context.ActiveSubstances.FindAsync(id);
            if (activeSubstance == null)
            {
                return NotFound();
            }
            return View(activeSubstance);
        }

        // POST: ActiveSubstances/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Info")] ActiveSubstance activeSubstance)
        {
            if (id != activeSubstance.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(activeSubstance);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ActiveSubstanceExists(activeSubstance.Id))
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
            return View(activeSubstance);
        }

        // GET: ActiveSubstances/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var activeSubstance = await _context.ActiveSubstances
                .FirstOrDefaultAsync(m => m.Id == id);
            if (activeSubstance == null)
            {
                return NotFound();
            }

            return View(activeSubstance);
        }

        // POST: ActiveSubstances/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var activeSubstance = await _context.ActiveSubstances.FindAsync(id);
            _context.ActiveSubstances.Remove(activeSubstance);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ActiveSubstanceExists(int id)
        {
            return _context.ActiveSubstances.Any(e => e.Id == id);
        }
    }
}
