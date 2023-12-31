﻿using System;
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
    public class THorasController : Controller
    {
        private readonly DB_RECOLECCION_RECICLAJEContext _context;

        public THorasController(DB_RECOLECCION_RECICLAJEContext context)
        {
            _context = context;
        }
        
        // Index THORAS

        [Authorize(Roles = "Administrador,Usuario")]
        public async Task<IActionResult> Index()
        {
              return _context.THora != null ? 
                          View(await _context.THora.ToListAsync()) :
                          Problem("Entity set 'DB_RECOLECCION_RECICLAJEContext.THora'  is null.");
        }
        
        
        //Detalles
        
        [Authorize(Roles = "Administrador,Usuario")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.THora == null)
            {
                return NotFound();
            }

            var tHora = await _context.THora
                .FirstOrDefaultAsync(m => m.HoraId == id);
            if (tHora == null)
            {
                return NotFound();
            }

            return View(tHora);
        }

        //Crear 
        [Authorize(Roles = "Administrador")]
        public IActionResult Create()
        {
            return View();
        }
        //Crear 

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Create([Bind("HoraId,Hora")] THora tHora)
        {
            try
            {
                _context.Add(tHora);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch 
            {
                throw;
            }
            return View(tHora);
        }


        //Editar
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.THora == null)
            {
                return NotFound();
            }

            var tHora = await _context.THora.FindAsync(id);
            if (tHora == null)
            {
                return NotFound();
            }
            return View(tHora);
        }


        //Editar

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Edit(int id, [Bind("HoraId,Hora")] THora tHora)
        {
            if (id != tHora.HoraId)
            {
                return NotFound();
            }
                try
                {
                    _context.Update(tHora);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!THoraExists(tHora.HoraId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            
            return View(tHora);
        }

        //Eliminar

        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.THora == null)
            {
                return NotFound();
            }

            var tHora = await _context.THora
                .FirstOrDefaultAsync(m => m.HoraId == id);
            if (tHora == null)
            {
                return NotFound();
            }

            return View(tHora);
        }


        //Eliminar
        
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.THora == null)
            {
                return Problem("Entity set 'DB_RECOLECCION_RECICLAJEContext.THora'  is null.");
            }
            var tHora = await _context.THora.FindAsync(id);
            if (tHora != null)
            {
                _context.THora.Remove(tHora);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool THoraExists(int id)
        {
          return (_context.THora?.Any(e => e.HoraId == id)).GetValueOrDefault();
        }
    }
}
