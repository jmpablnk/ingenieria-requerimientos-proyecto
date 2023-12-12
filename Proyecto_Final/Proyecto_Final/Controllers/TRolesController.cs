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
    public class TRolesController : Controller
    {
        private readonly DB_RECOLECCION_RECICLAJEContext _context;

        /// <summary>
        ///
        /// 
        /// En este apartado, El administrador Pondra las diversas tipos de roles para que el sistema lo tome en cuenta y los usuarios tenga acceso.
        /// 
        /// 
        /// 
        /// </summary>

        public TRolesController(DB_RECOLECCION_RECICLAJEContext context)
        {
            _context = context;
        }

        // Tabla: TRoles
        public async Task<IActionResult> Index()
        {
              return _context.TRole != null ? 
                          View(await _context.TRole.ToListAsync()) :
                          Problem("Entity set 'DB_RECOLECCION_RECICLAJEContext.TRole'  is null.");
        }

        // Editar
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.TRole == null)
            {
                return NotFound();
            }

            var tRole = await _context.TRole
                .FirstOrDefaultAsync(m => m.RolId == id);
            if (tRole == null)
            {
                return NotFound();
            }

            return View(tRole);
        }

        // Crear
        public IActionResult Create()
        {
            return View();
        }

        // Crear
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RolId,Nombre,Permiso,Estado")] TRole tRole)
        {
            try
            {
                _context.Add(tRole);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch 
            {
                throw;
            }
            return View(tRole);
        }

        // Editar
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.TRole == null)
            {
                return NotFound();
            }

            var tRole = await _context.TRole.FindAsync(id);
            if (tRole == null)
            {
                return NotFound();
            }
            return View(tRole);
        }

        //Roles editar
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RolId,Nombre,Permiso,Estado")] TRole tRole)
        {
            if (id != tRole.RolId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tRole);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TRoleExists(tRole.RolId))
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
            return View(tRole);
        }

        // Eliminar Roles
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.TRole == null)
            {
                return NotFound();
            }

            var tRole = await _context.TRole
                .FirstOrDefaultAsync(m => m.RolId == id);
            if (tRole == null)
            {
                return NotFound();
            }

            return View(tRole);
        }

        //Eliminar Roles
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.TRole == null)
            {
                return Problem("Entity set 'DB_RECOLECCION_RECICLAJEContext.TRole'  is null.");
            }
            var tRole = await _context.TRole.FindAsync(id);
            if (tRole != null)
            {
                _context.TRole.Remove(tRole);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TRoleExists(int id)
        {
          return (_context.TRole?.Any(e => e.RolId == id)).GetValueOrDefault();
        }
    }
}
