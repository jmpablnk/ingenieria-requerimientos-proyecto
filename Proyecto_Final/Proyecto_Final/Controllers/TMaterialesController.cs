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
    public class TMaterialesController : Controller
    {
        private readonly DB_RECOLECCION_RECICLAJEContext _context;

        public TMaterialesController(DB_RECOLECCION_RECICLAJEContext context)
        {
            _context = context;
        }

        // Tabla TMateriales
        public async Task<IActionResult> Index()
        {
            var dB_RECOLECCION_RECICLAJEContext = _context.TMateriale.Include(t => t.NombreMaterial).Include(t => t.Peticion);
            return View(await dB_RECOLECCION_RECICLAJEContext.ToListAsync());
        }

        // Editar
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.TMateriale == null)
            {
                return NotFound();
            }

            var tMateriale = await _context.TMateriale
                .Include(t => t.NombreMaterial)
                .Include(t => t.Peticion)
                .FirstOrDefaultAsync(m => m.MaterialId == id);
            if (tMateriale == null)
            {
                return NotFound();
            }

            return View(tMateriale);
        }

        // Crear 
        public IActionResult Create()
        {

            ViewData["NombreMaterialId"] = new SelectList(_context.TNombreMaterial, "NombreMaterialId", "Nombre");
            ViewData["PeticionId"] = new SelectList(_context.TProgramarRecoleccion, "PeticionId", "DetallesEdificio");
            return View();
        }

        // Crear 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaterialId,NombreMaterialId,Peso,PeticionId")] TMateriale tMateriale)
        {
            try
            {
                _context.Add(tMateriale);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Home");
            }
            catch 
            {
                throw;
            }
            ViewData["NombreMaterialId"] = new SelectList(_context.TNombreMaterial, "NombreMaterialId", "NombreMaterialId", tMateriale.NombreMaterialId);
            ViewData["PeticionId"] = new SelectList(_context.TProgramarRecoleccion, "PeticionId", "PeticionId", tMateriale.PeticionId);
            return View(tMateriale);
        }

        // Editar
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.TMateriale == null)
            {
                return NotFound();
            }

            var tMateriale = await _context.TMateriale.FindAsync(id);
            if (tMateriale == null)
            {
                return NotFound();
            }
            ViewData["NombreMaterialId"] = new SelectList(_context.TNombreMaterial, "NombreMaterialId", "NombreMaterialId", tMateriale.NombreMaterialId);
            ViewData["PeticionId"] = new SelectList(_context.TProgramarRecoleccion, "PeticionId", "PeticionId", tMateriale.PeticionId);
            return View(tMateriale);
        }



        // Editar
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MaterialId,NombreMaterialId,Peso,PeticionId")] TMateriale tMateriale)
        {
            if (id != tMateriale.MaterialId)
            {
                return NotFound();
            }

            
                try
                {
                    _context.Update(tMateriale);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TMaterialeExists(tMateriale.MaterialId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            
            ViewData["NombreMaterialId"] = new SelectList(_context.TNombreMaterial, "NombreMaterialId", "NombreMaterialId", tMateriale.NombreMaterialId);
            ViewData["PeticionId"] = new SelectList(_context.TProgramarRecoleccion, "PeticionId", "PeticionId", tMateriale.PeticionId);
            return View(tMateriale);
        }

        // Eliminar
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.TMateriale == null)
            {
                return NotFound();
            }

            var tMateriale = await _context.TMateriale
                .Include(t => t.NombreMaterial)
                .Include(t => t.Peticion)
                .FirstOrDefaultAsync(m => m.MaterialId == id);
            if (tMateriale == null)
            {
                return NotFound();
            }

            return View(tMateriale);
        }

        //Eliminar
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.TMateriale == null)
            {
                return Problem("Entity set 'DB_RECOLECCION_RECICLAJEContext.TMateriale'  is null.");
            }
            var tMateriale = await _context.TMateriale.FindAsync(id);
            if (tMateriale != null)
            {
                _context.TMateriale.Remove(tMateriale);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TMaterialeExists(int id)
        {
          return (_context.TMateriale?.Any(e => e.MaterialId == id)).GetValueOrDefault();
        }
    }
}
