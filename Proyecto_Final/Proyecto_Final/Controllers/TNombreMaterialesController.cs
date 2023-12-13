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
    public class TNombreMaterialesController : Controller
    {
        private readonly DB_RECOLECCION_RECICLAJEContext _context;

        public TNombreMaterialesController(DB_RECOLECCION_RECICLAJEContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
              return _context.TNombreMaterial != null ? 
                          View(await _context.TNombreMaterial.ToListAsync()) :
                          Problem("Entity set 'DB_RECOLECCION_RECICLAJEContext.TNombreMaterial'  is null.");
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.TNombreMaterial == null)
            {
                return NotFound();
            }

            var tNombreMaterial = await _context.TNombreMaterial
                .FirstOrDefaultAsync(m => m.NombreMaterialId == id);
            if (tNombreMaterial == null)
            {
                return NotFound();
            }

            return View(tNombreMaterial);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("NombreMaterialId,Nombre")] TNombreMaterial tNombreMaterial)
        {
            try
            {
                _context.Add(tNombreMaterial);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch
            { }
            return View(tNombreMaterial);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.TNombreMaterial == null)
            {
                return NotFound();
            }

            var tNombreMaterial = await _context.TNombreMaterial.FindAsync(id);
            if (tNombreMaterial == null)
            {
                return NotFound();
            }
            return View(tNombreMaterial);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("NombreMaterialId,Nombre")] TNombreMaterial tNombreMaterial)
        {
            if (id != tNombreMaterial.NombreMaterialId)
            {
                return NotFound();
            }
                try
                {
                    _context.Update(tNombreMaterial);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TNombreMaterialExists(tNombreMaterial.NombreMaterialId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                
                return RedirectToAction(nameof(Index));
            }
            return View(tNombreMaterial);
        }
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.TNombreMaterial == null)
            {
                return NotFound();
            }

            var tNombreMaterial = await _context.TNombreMaterial
                .FirstOrDefaultAsync(m => m.NombreMaterialId == id);
            if (tNombreMaterial == null)
            {
                return NotFound();
            }

            return View(tNombreMaterial);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.TNombreMaterial == null)
            {
                return Problem("Entity set 'DB_RECOLECCION_RECICLAJEContext.TNombreMaterial'  is null.");
            }
            var tNombreMaterial = await _context.TNombreMaterial.FindAsync(id);
            if (tNombreMaterial != null)
            {
                _context.TNombreMaterial.Remove(tNombreMaterial);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TNombreMaterialExists(int id)
        {
          return (_context.TNombreMaterial?.Any(e => e.NombreMaterialId == id)).GetValueOrDefault();
        }
    }
}
