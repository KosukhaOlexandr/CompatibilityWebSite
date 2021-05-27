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
    public class CompatibilityStatusController : Controller
    {
        private readonly CompatibilityWebSiteContext _context;

        public CompatibilityStatusController(CompatibilityWebSiteContext context)
        {
            _context = context;
        }

        // GET: CompatibilityStatus
        public async Task<IActionResult> Index()
        {
            return View(await _context.CompatibilityStatuses.ToListAsync());
        }

        // GET: CompatibilityStatus/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var compatibilityStatus = await _context.CompatibilityStatuses
                .FirstOrDefaultAsync(m => m.Id == id);
            if (compatibilityStatus == null)
            {
                return NotFound();
            }

            return View(compatibilityStatus);
        }

        // GET: CompatibilityStatus/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CompatibilityStatus/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Info")] CompatibilityStatus compatibilityStatus)
        {
            if (ModelState.IsValid)
            {
                _context.Add(compatibilityStatus);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(compatibilityStatus);
        }

        // GET: CompatibilityStatus/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var compatibilityStatus = await _context.CompatibilityStatuses.FindAsync(id);
            if (compatibilityStatus == null)
            {
                return NotFound();
            }
            return View(compatibilityStatus);
        }

        // POST: CompatibilityStatus/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Info")] CompatibilityStatus compatibilityStatus)
        {
            if (id != compatibilityStatus.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(compatibilityStatus);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CompatibilityStatusExists(compatibilityStatus.Id))
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
            return View(compatibilityStatus);
        }

        // GET: CompatibilityStatus/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var compatibilityStatus = await _context.CompatibilityStatuses
                .FirstOrDefaultAsync(m => m.Id == id);
            if (compatibilityStatus == null)
            {
                return NotFound();
            }

            return View(compatibilityStatus);
        }

        // POST: CompatibilityStatus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var compatibilityStatus = await _context.CompatibilityStatuses.FindAsync(id);
            _context.CompatibilityStatuses.Remove(compatibilityStatus);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CompatibilityStatusExists(int id)
        {
            return _context.CompatibilityStatuses.Any(e => e.Id == id);
        }
    }
}
