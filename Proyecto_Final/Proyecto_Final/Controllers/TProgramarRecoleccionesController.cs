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
    public class TProgramarRecoleccionesController : Controller
    {
        private readonly DB_RECOLECCION_RECICLAJEContext _context;

        public TProgramarRecoleccionesController(DB_RECOLECCION_RECICLAJEContext context)
        {
            _context = context;
        }

        // GET: TProgramarRecolecciones
        public async Task<IActionResult> Index()
        {
            var dB_RECOLECCION_RECICLAJEContext = _context.TProgramarRecoleccion.Include(t => t.FechaNavigation).Include(t => t.HoraNavigation).Include(t => t.ProvinciaNavigation);
            return View(await dB_RECOLECCION_RECICLAJEContext.ToListAsync());
        }

        // GET: TProgramarRecolecciones/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.TProgramarRecoleccion == null)
            {
                return NotFound();
            }

            var tProgramarRecoleccion = await _context.TProgramarRecoleccion
                .Include(t => t.FechaNavigation)
                .Include(t => t.HoraNavigation)
                .Include(t => t.ProvinciaNavigation)
                .FirstOrDefaultAsync(m => m.PeticionId == id);
            if (tProgramarRecoleccion == null)
            {
                return NotFound();
            }

            return View(tProgramarRecoleccion);
        }

        // GET: TProgramarRecolecciones/Create
        public IActionResult Create()
        {
            ViewData["Fecha"] = new SelectList(_context.TFecha, "FechaId", "FechaId");
            ViewData["Hora"] = new SelectList(_context.THora, "HoraId", "HoraId");
            ViewData["Provincia"] = new SelectList(_context.TProvincium, "ProvinciaId", "ProvinciaId");
            return View();
        }

        // POST: TProgramarRecolecciones/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PeticionId,DetallesEdificio,CodigoPostal,Municipio,Provincia,Canton,Fecha,Hora")] TProgramarRecoleccion tProgramarRecoleccion)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tProgramarRecoleccion);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Fecha"] = new SelectList(_context.TFecha, "FechaId", "FechaId", tProgramarRecoleccion.Fecha);
            ViewData["Hora"] = new SelectList(_context.THora, "HoraId", "HoraId", tProgramarRecoleccion.Hora);
            ViewData["Provincia"] = new SelectList(_context.TProvincium, "ProvinciaId", "ProvinciaId", tProgramarRecoleccion.Provincia);
            return View(tProgramarRecoleccion);
        }

        // GET: TProgramarRecolecciones/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.TProgramarRecoleccion == null)
            {
                return NotFound();
            }

            var tProgramarRecoleccion = await _context.TProgramarRecoleccion.FindAsync(id);
            if (tProgramarRecoleccion == null)
            {
                return NotFound();
            }
            ViewData["Fecha"] = new SelectList(_context.TFecha, "FechaId", "FechaId", tProgramarRecoleccion.Fecha);
            ViewData["Hora"] = new SelectList(_context.THora, "HoraId", "HoraId", tProgramarRecoleccion.Hora);
            ViewData["Provincia"] = new SelectList(_context.TProvincium, "ProvinciaId", "ProvinciaId", tProgramarRecoleccion.Provincia);
            return View(tProgramarRecoleccion);
        }

        // POST: TProgramarRecolecciones/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PeticionId,DetallesEdificio,CodigoPostal,Municipio,Provincia,Canton,Fecha,Hora")] TProgramarRecoleccion tProgramarRecoleccion)
        {
            if (id != tProgramarRecoleccion.PeticionId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tProgramarRecoleccion);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TProgramarRecoleccionExists(tProgramarRecoleccion.PeticionId))
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
            ViewData["Fecha"] = new SelectList(_context.TFecha, "FechaId", "FechaId", tProgramarRecoleccion.Fecha);
            ViewData["Hora"] = new SelectList(_context.THora, "HoraId", "HoraId", tProgramarRecoleccion.Hora);
            ViewData["Provincia"] = new SelectList(_context.TProvincium, "ProvinciaId", "ProvinciaId", tProgramarRecoleccion.Provincia);
            return View(tProgramarRecoleccion);
        }

        // GET: TProgramarRecolecciones/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.TProgramarRecoleccion == null)
            {
                return NotFound();
            }

            var tProgramarRecoleccion = await _context.TProgramarRecoleccion
                .Include(t => t.FechaNavigation)
                .Include(t => t.HoraNavigation)
                .Include(t => t.ProvinciaNavigation)
                .FirstOrDefaultAsync(m => m.PeticionId == id);
            if (tProgramarRecoleccion == null)
            {
                return NotFound();
            }

            return View(tProgramarRecoleccion);
        }

        // POST: TProgramarRecolecciones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.TProgramarRecoleccion == null)
            {
                return Problem("Entity set 'DB_RECOLECCION_RECICLAJEContext.TProgramarRecoleccion'  is null.");
            }
            var tProgramarRecoleccion = await _context.TProgramarRecoleccion.FindAsync(id);
            if (tProgramarRecoleccion != null)
            {
                _context.TProgramarRecoleccion.Remove(tProgramarRecoleccion);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TProgramarRecoleccionExists(int id)
        {
          return (_context.TProgramarRecoleccion?.Any(e => e.PeticionId == id)).GetValueOrDefault();
        }
    }
}
