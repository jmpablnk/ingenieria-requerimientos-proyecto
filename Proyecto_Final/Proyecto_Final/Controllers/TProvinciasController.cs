using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Proyecto_Final.Models;

namespace Proyecto_Final.Controllers
{
    public class TProvinciasController : Controller
    {
        private readonly DB_RECOLECCION_RECICLAJEContext _context;

        public TProvinciasController(DB_RECOLECCION_RECICLAJEContext context)
        {
            _context = context;
        }

        // GET: TProvincias
        public async Task<IActionResult> Index()
        {
              return _context.TProvincium != null ? 
                          View(await _context.TProvincium.ToListAsync()) :
                          Problem("Entity set 'DB_RECOLECCION_RECICLAJEContext.TProvincium'  is null.");
        }

        // GET: TProvincias/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.TProvincium == null)
            {
                return NotFound();
            }

            var tProvincium = await _context.TProvincium
                .FirstOrDefaultAsync(m => m.ProvinciaId == id);
            if (tProvincium == null)
            {
                return NotFound();
            }

            return View(tProvincium);
        }

        // GET: TProvincias/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TProvincias/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProvinciaId,Provincia")] TProvincium tProvincium)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tProvincium);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tProvincium);
        }

        // GET: TProvincias/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.TProvincium == null)
            {
                return NotFound();
            }

            var tProvincium = await _context.TProvincium.FindAsync(id);
            if (tProvincium == null)
            {
                return NotFound();
            }
            return View(tProvincium);
        }

        // POST: TProvincias/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProvinciaId,Provincia")] TProvincium tProvincium)
        {
            if (id != tProvincium.ProvinciaId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tProvincium);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TProvinciumExists(tProvincium.ProvinciaId))
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
            return View(tProvincium);
        }

        // GET: TProvincias/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.TProvincium == null)
            {
                return NotFound();
            }

            var tProvincium = await _context.TProvincium
                .FirstOrDefaultAsync(m => m.ProvinciaId == id);
            if (tProvincium == null)
            {
                return NotFound();
            }

            return View(tProvincium);
        }

        // POST: TProvincias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.TProvincium == null)
            {
                return Problem("Entity set 'DB_RECOLECCION_RECICLAJEContext.TProvincium'  is null.");
            }
            var tProvincium = await _context.TProvincium.FindAsync(id);
            if (tProvincium != null)
            {
                _context.TProvincium.Remove(tProvincium);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TProvinciumExists(int id)
        {
          return (_context.TProvincium?.Any(e => e.ProvinciaId == id)).GetValueOrDefault();
        }
    }
}
