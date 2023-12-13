using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Proyecto_Final.Models;

namespace Proyecto_Final.Controllers
{
    public class TProgramarRecoleccionesController : Controller
    {
        private readonly DB_RECOLECCION_RECICLAJEContext _context;

        //Index TProgramarRecolecciones
        public TProgramarRecoleccionesController(DB_RECOLECCION_RECICLAJEContext context)
        {
            _context = context;
        }



        ////Index TProgramarRecolecciones
        [Authorize(Roles = "Administrador,Usuario")]
        public async Task<IActionResult> Index()
        {
            var dB_RECOLECCION_RECICLAJEContext = _context.TProgramarRecoleccion.Include(t => t.FechaNavigation).Include(t => t.HoraNavigation).Include(t => t.ProvinciaNavigation);
            return View(await dB_RECOLECCION_RECICLAJEContext.ToListAsync());
        }
        [Authorize(Roles = "Administrador,Usuario")]



        //Detalles
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

        //Crear 

        [Authorize(Roles = "Administrador,Usuario")]
        public IActionResult Create()
        {
            ViewData["Fecha"] = new SelectList(_context.TFecha, "FechaId", "Fecha");
            ViewData["Hora"] = new SelectList(_context.THora, "HoraId", "Hora");
            ViewData["Provincia"] = new SelectList(_context.TProvincium, "ProvinciaId", "Provincia");
            return View();
        }


        //Crear 

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador,Usuario")]
        public async Task<IActionResult> Create([Bind("PeticionId,DetallesEdificio,CodigoPostal,Municipio,Provincia,Canton,Fecha,Hora")] TProgramarRecoleccion tProgramarRecoleccion)
        {
            try
            {
                _context.Add(tProgramarRecoleccion);
                await _context.SaveChangesAsync();
                return RedirectToAction("Create", "TMateriales");
            }
            catch
            {
                throw;
            }
            ViewData["Fecha"] = new SelectList(_context.TFecha, "FechaId", "FechaId", tProgramarRecoleccion.Fecha);
            ViewData["Hora"] = new SelectList(_context.THora, "HoraId", "HoraId", tProgramarRecoleccion.Hora);
            ViewData["Provincia"] = new SelectList(_context.TProvincium, "ProvinciaId", "ProvinciaId", tProgramarRecoleccion.Provincia);
            return View(tProgramarRecoleccion);
        }



        //Editar 

        [Authorize(Roles = "Administrador")]
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
            ViewData["Fecha"] = new SelectList(_context.TFecha, "FechaId", "Fecha", tProgramarRecoleccion.Fecha);
            ViewData["Hora"] = new SelectList(_context.THora, "HoraId", "Hora", tProgramarRecoleccion.Hora);
            ViewData["Provincia"] = new SelectList(_context.TProvincium, "ProvinciaId", "Provincia", tProgramarRecoleccion.Provincia);
            return View(tProgramarRecoleccion);
        }

      

        //Editar 


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Edit(int id, [Bind("PeticionId,DetallesEdificio,CodigoPostal,Municipio,Provincia,Canton,Fecha,Hora")] TProgramarRecoleccion tProgramarRecoleccion)
        {
            if (id != tProgramarRecoleccion.PeticionId)
            {
                return NotFound();
            }
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
            
            ViewData["Fecha"] = new SelectList(_context.TFecha, "FechaId", "FechaId", tProgramarRecoleccion.Fecha);
            ViewData["Hora"] = new SelectList(_context.THora, "HoraId", "HoraId", tProgramarRecoleccion.Hora);
            ViewData["Provincia"] = new SelectList(_context.TProvincium, "ProvinciaId", "ProvinciaId", tProgramarRecoleccion.Provincia);
            return View(tProgramarRecoleccion);
        }

        //Eliminar


        [Authorize(Roles = "Administrador")]
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

        //Eliminar 

        [Authorize(Roles = "Administrador")]
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
