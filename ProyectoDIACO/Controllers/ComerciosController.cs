using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProyectoDIACO.Data;
using ProyectoDIACO.Models;
using ProyectoDIACO.Services;

namespace ProyectoDIACO.Controllers
{
    public class ComerciosController : Controller
    {
        private readonly ProyectoDIACOContext _context;
        private readonly AutenticacionService _autenticacionService;

        public ComerciosController(ProyectoDIACOContext context, AutenticacionService autenticacionService)
        {
            _context = context;
            _autenticacionService = autenticacionService;
        }

        // GET: Comercios
        public async Task<IActionResult> Index()
        {
            if (!_autenticacionService.isAdmin(HttpContext))
            {
                if (_autenticacionService.isLogin(HttpContext))
                    return RedirectToAction("Index", "Home");

                return RedirectToAction("Index", "Login");
            }

            _autenticacionService.fillViewData(ViewData, HttpContext);

            return _context.Comercio != null ? 
                          View(await _context.Comercio.ToListAsync()) :
                          Problem("Entity set 'ProyectoDIACOContext.Comercio'  is null.");
        }

        // GET: Comercios/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (!_autenticacionService.isAdmin(HttpContext))
            {
                if (_autenticacionService.isLogin(HttpContext))
                    return RedirectToAction("Index", "Home");

                return RedirectToAction("Index", "Login");
            }

            _autenticacionService.fillViewData(ViewData, HttpContext);

            if (id == null || _context.Comercio == null)
            {
                return NotFound();
            }

            var comercio = await _context.Comercio
                .FirstOrDefaultAsync(m => m.ComercioId == id);
            if (comercio == null)
            {
                return NotFound();
            }

            return View(comercio);
        }

        // GET: Comercios/Create
        public IActionResult Create()
        {
            if (!_autenticacionService.isAdmin(HttpContext))
            {
                if (_autenticacionService.isLogin(HttpContext))
                    return RedirectToAction("Index", "Home");

                return RedirectToAction("Index", "Login");
            }

            _autenticacionService.fillViewData(ViewData, HttpContext);

            return View();
        }

        // POST: Comercios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ComercioId,Nombre,Telefono")] Comercio comercio)
        {
            if (!_autenticacionService.isAdmin(HttpContext))
            {
                if (_autenticacionService.isLogin(HttpContext))
                    return RedirectToAction("Index", "Home");

                return RedirectToAction("Index", "Login");
            }

            _autenticacionService.fillViewData(ViewData, HttpContext);

            if (ModelState.IsValid)
            {
                comercio.Estado = 1;
                _context.Add(comercio);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(comercio);
        }

        // GET: Comercios/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (!_autenticacionService.isAdmin(HttpContext))
            {
                if (_autenticacionService.isLogin(HttpContext))
                    return RedirectToAction("Index", "Home");

                return RedirectToAction("Index", "Login");
            }

            _autenticacionService.fillViewData(ViewData, HttpContext);

            if (id == null || _context.Comercio == null)
            {
                return NotFound();
            }

            var comercio = await _context.Comercio.FindAsync(id);
            if (comercio == null)
            {
                return NotFound();
            }
            return View(comercio);
        }

        // POST: Comercios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ComercioId,Nombre,Telefono,Estado")] Comercio comercio)
        {
            if (!_autenticacionService.isAdmin(HttpContext))
            {
                if (_autenticacionService.isLogin(HttpContext))
                    return RedirectToAction("Index", "Home");

                return RedirectToAction("Index", "Login");
            }

            _autenticacionService.fillViewData(ViewData, HttpContext);

            if (id != comercio.ComercioId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(comercio);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ComercioExists(comercio.ComercioId))
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
            return View(comercio);
        }

        // GET: Comercios/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (!_autenticacionService.isAdmin(HttpContext))
            {
                if (_autenticacionService.isLogin(HttpContext))
                    return RedirectToAction("Index", "Home");

                return RedirectToAction("Index", "Login");
            }

            _autenticacionService.fillViewData(ViewData, HttpContext);

            if (id == null || _context.Comercio == null)
            {
                return NotFound();
            }

            var comercio = await _context.Comercio
                .FirstOrDefaultAsync(m => m.ComercioId == id);
            if (comercio == null)
            {
                return NotFound();
            }

            return View(comercio);
        }

        // POST: Comercios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (!_autenticacionService.isAdmin(HttpContext))
            {
                if (_autenticacionService.isLogin(HttpContext))
                    return RedirectToAction("Index", "Home");

                return RedirectToAction("Index", "Login");
            }

            _autenticacionService.fillViewData(ViewData, HttpContext);

            if (_context.Comercio == null)
            {
                return Problem("Entity set 'ProyectoDIACOContext.Comercio'  is null.");
            }
            var comercio = await _context.Comercio.FindAsync(id);
            if (comercio != null)
            {
                comercio.Estado = 0;
                _context.Update(comercio);
                await _context.SaveChangesAsync();

            }

            return RedirectToAction(nameof(Index));
        }

        private bool ComercioExists(int id)
        {
          return (_context.Comercio?.Any(e => e.ComercioId == id)).GetValueOrDefault();
        }
    }
}
