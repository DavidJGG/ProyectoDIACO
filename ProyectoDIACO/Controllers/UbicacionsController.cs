using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProyectoDIACO.Data;
using ProyectoDIACO.Models;

namespace ProyectoDIACO.Controllers
{
    public class UbicacionsController : Controller
    {
        private readonly ProyectoDIACOContext _context;

        public UbicacionsController(ProyectoDIACOContext context)
        {
            _context = context;
        }

        // GET: Ubicacions
        public async Task<IActionResult> Index()
        {
              return _context.Ubicacion != null ? 
                          View(await _context.Ubicacion.ToListAsync()) :
                          Problem("Entity set 'ProyectoDIACOContext.Ubicacion'  is null.");
        }

        // GET: Ubicacions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Ubicacion == null)
            {
                return NotFound();
            }

            var ubicacion = await _context.Ubicacion
                .FirstOrDefaultAsync(m => m.UbicacionId == id);
            if (ubicacion == null)
            {
                return NotFound();
            }

            return View(ubicacion);
        }

        // GET: Ubicacions/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Ubicacions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UbicacionId,Nombre,Tipo,SububicacionId")] Ubicacion ubicacion)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ubicacion);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(ubicacion);
        }

        // GET: Ubicacions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Ubicacion == null)
            {
                return NotFound();
            }

            var ubicacion = await _context.Ubicacion.FindAsync(id);
            if (ubicacion == null)
            {
                return NotFound();
            }
            return View(ubicacion);
        }

        // POST: Ubicacions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("UbicacionId,Nombre,Tipo,SububicacionId")] Ubicacion ubicacion)
        {
            if (id != ubicacion.UbicacionId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ubicacion);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UbicacionExists(ubicacion.UbicacionId))
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
            return View(ubicacion);
        }

        // GET: Ubicacions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Ubicacion == null)
            {
                return NotFound();
            }

            var ubicacion = await _context.Ubicacion
                .FirstOrDefaultAsync(m => m.UbicacionId == id);
            if (ubicacion == null)
            {
                return NotFound();
            }

            return View(ubicacion);
        }

        // POST: Ubicacions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Ubicacion == null)
            {
                return Problem("Entity set 'ProyectoDIACOContext.Ubicacion'  is null.");
            }
            var ubicacion = await _context.Ubicacion.FindAsync(id);
            if (ubicacion != null)
            {
                _context.Ubicacion.Remove(ubicacion);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UbicacionExists(int id)
        {
          return (_context.Ubicacion?.Any(e => e.UbicacionId == id)).GetValueOrDefault();
        }
    }
}
