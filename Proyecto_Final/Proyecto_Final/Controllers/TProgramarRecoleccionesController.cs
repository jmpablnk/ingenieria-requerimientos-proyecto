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

        // TProgramarRecolecciones
        public async Task<IActionResult> Index()
        {
            var dB_RECOLECCION_RECICLAJEContext = _context.TProgramarRecoleccion.Include(t => t.FechaNavigation).Include(t => t.HoraNavigation).Include(t => t.ProvinciaNavigation).Include(t => t.Usuario);
            return View(await dB_RECOLECCION_RECICLAJEContext.ToListAsync());
        }

        // Detalles
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
                .Include(t => t.Usuario)
                .FirstOrDefaultAsync(m => m.PeticionId == id);
            if (tProgramarRecoleccion == null)
            {
                return NotFound();
            }

            return View(tProgramarRecoleccion);
        }



        // Crear 
        public IActionResult Create()
        {
            var usuarios = _context.TUsuario.ToList(); // Obtén todos los usuarios

            // Construir la lista de SelectListItem con nombre y apellido concatenados
            var usuariosList = usuarios.Select(u => new SelectListItem
            {
                Value = u.UsuarioId.ToString(),
                Text = $"{u.Nombre} {u.Apellido}"  // Concatenar nombre y apellido
            }).ToList();

            ViewData["UsuarioId"] = new SelectList(usuariosList, "Value", "Text");
            ViewData["Fecha"] = new SelectList(_context.TFecha, "FechaId", "Fecha");
            ViewData["Hora"] = new SelectList(_context.THora, "HoraId", "Hora");
            ViewData["Provincia"] = new SelectList(_context.TProvincium, "ProvinciaId", "Provincia");
            return View();
        }

        // Crear
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PeticionId,DetallesEdificio,CodigoPostal,Municipio,Provincia,Canton,Fecha,Hora,UsuarioId")] TProgramarRecoleccion tProgramarRecoleccion)
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
            ViewData["UsuarioId"] = new SelectList(_context.TUsuario, "UsuarioId", "Apellido", tProgramarRecoleccion.UsuarioId);
            return View(tProgramarRecoleccion);
        }

        // Editar
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
            var usuarios = _context.TUsuario.ToList(); // Obtén todos los usuarios

            // Construir la lista de SelectListItem con nombre y apellido concatenados
            var usuariosList = usuarios.Select(u => new SelectListItem
            {
                Value = u.UsuarioId.ToString(),
                Text = $"{u.Nombre} {u.Apellido}"  // Concatenar nombre y apellido
            }).ToList();

            ViewData["UsuarioId"] = new SelectList(usuariosList, "Value", "Text");
            ViewData["Fecha"] = new SelectList(_context.TFecha, "FechaId", "Fecha", tProgramarRecoleccion.Fecha);
            ViewData["Hora"] = new SelectList(_context.THora, "HoraId", "Hora", tProgramarRecoleccion.Hora);
            ViewData["Provincia"] = new SelectList(_context.TProvincium, "ProvinciaId", "Provincia", tProgramarRecoleccion.Provincia);
            return View(tProgramarRecoleccion);
        }

        // Editar
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PeticionId,DetallesEdificio,CodigoPostal,Municipio,Provincia,Canton,Fecha,Hora,UsuarioId")] TProgramarRecoleccion tProgramarRecoleccion)
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
            ViewData["UsuarioId"] = new SelectList(_context.TUsuario, "UsuarioId", "Apellido", tProgramarRecoleccion.UsuarioId);
            return View(tProgramarRecoleccion);
        }

        // Eliminar
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
                .Include(t => t.Usuario)
                .FirstOrDefaultAsync(m => m.PeticionId == id);
            if (tProgramarRecoleccion == null)
            {
                return NotFound();
            }

            return View(tProgramarRecoleccion);
        }

        // Eliminar
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
