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
    [Authorize(Roles = "Administrador")]
    public class TFechasController : Controller
    {
        private readonly DB_RECOLECCION_RECICLAJEContext _context;

        public TFechasController(DB_RECOLECCION_RECICLAJEContext context)
        {
            _context = context;
        }
        //Index TFechas

        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Index()
        {
              return _context.TFecha != null ? 
                          View(await _context.TFecha.ToListAsync()) :
                          Problem("Entity set 'DB_RECOLECCION_RECICLAJEContext.TFecha'  is null.");
        }

        
        //Detalles

        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.TFecha == null)
            {
                return NotFound();
            }

            var tFecha = await _context.TFecha
                .FirstOrDefaultAsync(m => m.FechaId == id);
            if (tFecha == null)
            {
                return NotFound();
            }

            return View(tFecha);
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
        public async Task<IActionResult> Create([Bind("FechaId,Fecha")] TFecha tFecha)
        {
            try
            {
                _context.Add(tFecha);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch 
            {
                throw;
            }
            return View(tFecha);
        }


        //Editar 
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.TFecha == null)
            {
                return NotFound();
            }

            var tFecha = await _context.TFecha.FindAsync(id);
            if (tFecha == null)
            {
                return NotFound();
            }
            return View(tFecha);
        }

        //Editar

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Edit(int id, [Bind("FechaId,Fecha")] TFecha tFecha)
        {
            if (id != tFecha.FechaId)
            {
                return NotFound();
            }

           
                try
                {
                    _context.Update(tFecha);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TFechaExists(tFecha.FechaId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                
                return RedirectToAction(nameof(Index));
            }
            return View(tFecha);
        }


        //Eliminar

        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.TFecha == null)
            {
                return NotFound();
            }

            var tFecha = await _context.TFecha
                .FirstOrDefaultAsync(m => m.FechaId == id);
            if (tFecha == null)
            {
                return NotFound();
            }

            return View(tFecha);
        }

        //Eliminar

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.TFecha == null)
            {
                return Problem("Entity set 'DB_RECOLECCION_RECICLAJEContext.TFecha'  is null.");
            }
            var tFecha = await _context.TFecha.FindAsync(id);
            if (tFecha != null)
            {
                _context.TFecha.Remove(tFecha);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TFechaExists(int id)
        {
          return (_context.TFecha?.Any(e => e.FechaId == id)).GetValueOrDefault();
        }
    }
}
