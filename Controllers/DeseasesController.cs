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
    public class DeseasesController : Controller
    {
        private readonly CompatibilityWebSiteContext _context;

        public DeseasesController(CompatibilityWebSiteContext context)
        {
            _context = context;
        }

        // GET: Deseases
        public async Task<IActionResult> Index()
        {
            return View(await _context.Deseases.ToListAsync());
        }

        // GET: Deseases/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var desease = await _context.Deseases
                .FirstOrDefaultAsync(m => m.Id == id);
            if (desease == null)
            {
                return NotFound();
            }

            return View(desease);
        }

        // GET: Deseases/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Deseases/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Info")] Desease desease)
        {
            if (ModelState.IsValid)
            {
                _context.Add(desease);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(desease);
        }

        // GET: Deseases/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var desease = await _context.Deseases.FindAsync(id);
            if (desease == null)
            {
                return NotFound();
            }
            return View(desease);
        }

        // POST: Deseases/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Info")] Desease desease)
        {
            if (id != desease.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(desease);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DeseaseExists(desease.Id))
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
            return View(desease);
        }

        // GET: Deseases/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var desease = await _context.Deseases
                .FirstOrDefaultAsync(m => m.Id == id);
            if (desease == null)
            {
                return NotFound();
            }

            return View(desease);
        }

        // POST: Deseases/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var desease = await _context.Deseases.FindAsync(id);
            _context.Deseases.Remove(desease);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DeseaseExists(int id)
        {
            return _context.Deseases.Any(e => e.Id == id);
        }
    }
}
