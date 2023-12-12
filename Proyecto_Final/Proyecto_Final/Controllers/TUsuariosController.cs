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
    public class TUsuariosController : Controller
    {
        private readonly DB_RECOLECCION_RECICLAJEContext _context;

        public TUsuariosController(DB_RECOLECCION_RECICLAJEContext context)
        {
            _context = context;
        }

        // GET: TUsuarios
        public async Task<IActionResult> Index()
        {
            var dB_RECOLECCION_RECICLAJEContext = _context.TUsuario.Include(t => t.Rol);
            return View(await dB_RECOLECCION_RECICLAJEContext.ToListAsync());
        }





        // Mostrar detalles de los usuarios
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.TUsuario == null)
            {
                return NotFound();
            }

            var tUsuario = await _context.TUsuario
                .Include(t => t.Rol)
                .FirstOrDefaultAsync(m => m.UsuarioId == id);
            if (tUsuario == null)
            {
                return NotFound();
            }

            return View(tUsuario);
        }



        // Crear
        public IActionResult Create()
        {
            var roles = _context.TRole.ToList(); // Obtén todos los roles

            // Mueve la opción con el valor "2" al principio
            var usuarioRol = roles.SingleOrDefault(r => r.RolId == 2);
            roles.Remove(usuarioRol);
            roles.Insert(0, usuarioRol);
            var tUsuario = new TUsuario
            {
                RolId = 2
            };

            ViewData["RolId"] = new SelectList(roles, "RolId", "Nombre");
            return View();
        }
        // Crear
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UsuarioId,Nombre,Apellido,Indentificacion,NumTelefono,CorreoElectronico,Contrasena,RolId")] TUsuario tUsuario)
        {
            try
            {
                _context.Add(tUsuario);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                throw;
            }

            var roles = _context.TRole.ToList(); // Obtén todos los roles

            // Mueve la opción con el valor "2" al principio
            var usuarioRol = roles.SingleOrDefault(r => r.RolId == 2);
            roles.Remove(usuarioRol);
            roles.Insert(0, usuarioRol);

            ViewData["RolId"] = new SelectList(roles, "RolId", "Nombre", tUsuario.RolId);
            return View(tUsuario);
        }



        // Editar USuarios
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.TUsuario == null)
            {
                return NotFound();
            }

            var tUsuario = await _context.TUsuario.FindAsync(id);
            if (tUsuario == null)
            {
                return NotFound();
            }
            ViewData["RolId"] = new SelectList(_context.TRole, "RolId", "RolId", tUsuario.RolId);
            return View(tUsuario);
        }

        // Editar
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("UsuarioId,Nombre,Apellido,Indentificacion,NumTelefono,CorreoElectronico,Contrasena,RolId")] TUsuario tUsuario)
        {
            if (id != tUsuario.UsuarioId)
            {
                return NotFound();
            }


            try
            {
                _context.Update(tUsuario);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TUsuarioExists(tUsuario.UsuarioId))
                {
                    return NotFound();
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    throw;
                }


            }
            ViewData["RolId"] = new SelectList(_context.TRole, "RolId", "RolId", tUsuario.RolId);
            return View(tUsuario);
        }

        // Eliminar
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.TUsuario == null)
            {
                return NotFound();
            }

            var tUsuario = await _context.TUsuario
                .Include(t => t.Rol)
                .FirstOrDefaultAsync(m => m.UsuarioId == id);
            if (tUsuario == null)
            {
                return NotFound();
            }

            return View(tUsuario);
        }


        // Eliminar
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.TUsuario == null)
            {
                return Problem("Entity set 'DB_RECOLECCION_RECICLAJEContext.TUsuario'  is null.");
            }
            var tUsuario = await _context.TUsuario.FindAsync(id);
            if (tUsuario != null)
            {
                _context.TUsuario.Remove(tUsuario);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TUsuarioExists(int id)
        {
            return (_context.TUsuario?.Any(e => e.UsuarioId == id)).GetValueOrDefault();
        }
    }
}

