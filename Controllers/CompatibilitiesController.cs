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
    public class CompatibilitiesController : Controller
    {
        private readonly CompatibilityWebSiteContext _context;

        public CompatibilitiesController(CompatibilityWebSiteContext context)
        {
            _context = context;
        }

        // GET: Compatibilities
        public async Task<IActionResult> Index()
        {
            var compatibilityWebSiteContext = _context.Compatibilities.Include(c => c.CompatibilityStatus).Include(c => c.FirstActiveSubstanceNavigation).Include(c => c.SecondActiveSubstanceNavigation);
            return View(await compatibilityWebSiteContext.ToListAsync());
        }

        // GET: Compatibilities/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var compatibility = await _context.Compatibilities
                .Include(c => c.CompatibilityStatus)
                .Include(c => c.FirstActiveSubstanceNavigation)
                .Include(c => c.SecondActiveSubstanceNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (compatibility == null)
            {
                return NotFound();
            }

            return View(compatibility);
        }

        // GET: Compatibilities/Create
        public IActionResult Create()
        {
            ViewData["CompatibilityStatusId"] = new SelectList(_context.CompatibilityStatuses, "Id", "Name");
            ViewData["FirstActiveSubstance"] = new SelectList(_context.ActiveSubstances, "Id", "Name");
            ViewData["SecondActiveSubstance"] = new SelectList(_context.ActiveSubstances, "Id", "Name");
            return View();
        }

        // POST: Compatibilities/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FirstActiveSubstance,SecondActiveSubstance,CompatibilityStatusId")] Compatibility compatibility)
        {
            if (ModelState.IsValid)
            {
                _context.Add(compatibility);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CompatibilityStatusId"] = new SelectList(_context.CompatibilityStatuses, "Id", "Name", compatibility.CompatibilityStatusId);
            ViewData["FirstActiveSubstance"] = new SelectList(_context.ActiveSubstances, "Id", "Name", compatibility.FirstActiveSubstance);
            ViewData["SecondActiveSubstance"] = new SelectList(_context.ActiveSubstances, "Id", "Name", compatibility.SecondActiveSubstance);
            return View(compatibility);
        }

        // GET: Compatibilities/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var compatibility = await _context.Compatibilities.FindAsync(id);
            if (compatibility == null)
            {
                return NotFound();
            }
            ViewData["CompatibilityStatusId"] = new SelectList(_context.CompatibilityStatuses, "Id", "Name", compatibility.CompatibilityStatusId);
            ViewData["FirstActiveSubstance"] = new SelectList(_context.ActiveSubstances, "Id", "Name", compatibility.FirstActiveSubstance);
            ViewData["SecondActiveSubstance"] = new SelectList(_context.ActiveSubstances, "Id", "Name", compatibility.SecondActiveSubstance);
            return View(compatibility);
        }

        // POST: Compatibilities/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FirstActiveSubstance,SecondActiveSubstance,CompatibilityStatusId")] Compatibility compatibility)
        {
            if (id != compatibility.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(compatibility);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CompatibilityExists(compatibility.Id))
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
            ViewData["CompatibilityStatusId"] = new SelectList(_context.CompatibilityStatuses, "Id", "Name", compatibility.CompatibilityStatusId);
            ViewData["FirstActiveSubstance"] = new SelectList(_context.ActiveSubstances, "Id", "Name", compatibility.FirstActiveSubstance);
            ViewData["SecondActiveSubstance"] = new SelectList(_context.ActiveSubstances, "Id", "Name", compatibility.SecondActiveSubstance);
            return View(compatibility);
        }

        // GET: Compatibilities/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var compatibility = await _context.Compatibilities
                .Include(c => c.CompatibilityStatus)
                .Include(c => c.FirstActiveSubstanceNavigation)
                .Include(c => c.SecondActiveSubstanceNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (compatibility == null)
            {
                return NotFound();
            }

            return View(compatibility);
        }

        // POST: Compatibilities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var compatibility = await _context.Compatibilities.FindAsync(id);
            _context.Compatibilities.Remove(compatibility);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CompatibilityExists(int id)
        {
            return _context.Compatibilities.Any(e => e.Id == id);
        }

        [HttpGet]
        public IActionResult VerifyActiveSubs([Bind(Prefix = "FirstActiveSubstance")] int firstActiveSubstanceId,
                                            [Bind(Prefix = "SecondActiveSubstance")]  int secondActiveSubstanceId)
        {
            /*if (!_userService.VerifyName(firstName, lastName))
            {
                return Json($"A user named {firstName} {lastName} already exists.");
            }*/

            var compatibilitiesByFirst = (from u in _context.Compatibilities
                                           where u.FirstActiveSubstance == firstActiveSubstanceId
                                           select u.SecondActiveSubstance).ToList();

            var compatibilitiesBySecond = (from u in _context.Compatibilities
                                          where u.SecondActiveSubstance == firstActiveSubstanceId
                                          select u.FirstActiveSubstance).ToList();

            if (compatibilitiesByFirst.Contains(secondActiveSubstanceId) 
             || compatibilitiesBySecond.Contains(secondActiveSubstanceId))
            {                
                return Json(false);
            }

            return Json(true);
        }
    }
}
