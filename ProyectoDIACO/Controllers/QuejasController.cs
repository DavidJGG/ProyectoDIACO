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
    public class QuejasController : Controller
    {
        private readonly ProyectoDIACOContext _context;
        private readonly AutenticacionService _autenticacionService;
        public QuejasController(ProyectoDIACOContext context, AutenticacionService autenticacionService)
        {
            _context = context;
            _autenticacionService = autenticacionService;
        }

        // GET: Quejas
        public async Task<IActionResult> Index()
        {
            _autenticacionService.fillViewData(ViewData, HttpContext);

            var proyectoDIACOContext = _context.Queja.Include(q => q.Sucursal).Include(q => q.Usuario);
            return View(await proyectoDIACOContext.ToListAsync());
        }

        public async Task<IActionResult> Misquejas()
        {
            if (!_autenticacionService.isLogin(HttpContext))
            {                
                return RedirectToAction("Index", "Login");
            }

            if (_autenticacionService.isAdmin(HttpContext))
            {
                return RedirectToAction("Index", "Home");
            }

            _autenticacionService.fillViewData(ViewData, HttpContext);

            var proyectoDIACOContext = _context.Queja.Where(q=>q.UsuarioId==HttpContext.Session.GetInt32("usuarioId")).Include(q => q.Sucursal).Include(q => q.Usuario);
            return View(await proyectoDIACOContext.ToListAsync());
        }

        // GET: Quejas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            _autenticacionService.fillViewData(ViewData, HttpContext);

            if (id == null || _context.Queja == null)
            {
                return NotFound();
            }

            var queja = await _context.Queja
                .Include(q => q.Sucursal.Comercio)
                .Include(q => q.Usuario)
                .FirstOrDefaultAsync(m => m.QuejaId == id);
            if (queja == null)
            {
                return NotFound();
            }

            return View(queja);
        }

        // GET: Quejas/Create
        public IActionResult Create()
        {

            _autenticacionService.fillViewData(ViewData, HttpContext);

            ViewData["id_usuario"] = HttpContext.Session.GetInt32("usuarioId");
            ViewData["Comercios"] = new SelectList(_context.Comercio.Where(c => c.Estado == 1).ToList(), "ComercioId", "Nombre");

            return View();
        }

        // POST: Quejas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("QuejaId,Detalle,Fecha,Estado,UsuarioId,SucursalId")] Queja queja)
        {
            _autenticacionService.fillViewData(ViewData, HttpContext);

            if (ModelState.IsValid)
            {
                queja.Estado = 1;
                if (queja.UsuarioId == 0) queja.UsuarioId = null;
                _context.Add(queja);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Comercios"] = new SelectList(_context.Comercio.Where(c=>c.Estado==1).ToList(), "ComercioId", "Nombre");
            ViewData["UsuarioId"] = new SelectList(_context.Usuario, "usuarioId", "Apellido", queja.UsuarioId);
            return View(queja);
        }

        // GET: Quejas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (!_autenticacionService.isAdmin(HttpContext))
            {
                if (_autenticacionService.isLogin(HttpContext))
                    return RedirectToAction("Index", "Home");

                return RedirectToAction("Index", "Login");
            }

            _autenticacionService.fillViewData(ViewData, HttpContext);

            if (id == null || _context.Queja == null)
            {
                return NotFound();
            }

            var queja = await _context.Queja.Where(q=>q.QuejaId==id).Include(q=>q.Sucursal.Comercio).FirstOrDefaultAsync();
            if (queja == null)
            {
                return NotFound();
            }

            ViewData["id_usuario"] = HttpContext.Session.GetInt32("usuarioId");
            ViewData["SucursalId"] = new SelectList(_context.Sucursal, "SucursalId", "Direccion", queja.SucursalId);
            ViewData["UsuarioId"] = new SelectList(_context.Usuario, "usuarioId", "Apellido", queja.UsuarioId);
            return View(queja);
        }

        // POST: Quejas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("QuejaId,Detalle,Fecha,Estado,Resultado,UsuarioId,SucursalId")] Queja queja)
        {
            if (!_autenticacionService.isAdmin(HttpContext))
            {
                if (_autenticacionService.isLogin(HttpContext))
                    return RedirectToAction("Index", "Home");

                return RedirectToAction("Index", "Login");
            }


            _autenticacionService.fillViewData(ViewData, HttpContext);

            if (id != queja.QuejaId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(queja);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!QuejaExists(queja.QuejaId))
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

            ViewData["SucursalId"] = new SelectList(_context.Sucursal, "SucursalId", "Direccion", queja.SucursalId);
            ViewData["UsuarioId"] = new SelectList(_context.Usuario, "usuarioId", "Apellido", queja.UsuarioId);
            return View(queja);
        }

        // GET: Quejas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (!_autenticacionService.isAdmin(HttpContext))
            {
                if (_autenticacionService.isLogin(HttpContext))
                    return RedirectToAction("Index", "Home");

                return RedirectToAction("Index", "Login");
            }


            _autenticacionService.fillViewData(ViewData, HttpContext);

            if (id == null || _context.Queja == null)
            {
                return NotFound();
            }

            var queja = await _context.Queja
                .Include(q => q.Sucursal)
                .Include(q => q.Usuario)
                .FirstOrDefaultAsync(m => m.QuejaId == id);
            if (queja == null)
            {
                return NotFound();
            }

            return View(queja);
        }

        // POST: Quejas/Delete/5
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

            if (_context.Queja == null)
            {
                return Problem("Entity set 'ProyectoDIACOContext.Queja'  is null.");
            }
            var queja = await _context.Queja.FindAsync(id);
            if (queja != null)
            {
                _context.Queja.Remove(queja);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool QuejaExists(int id)
        {
          return (_context.Queja?.Any(e => e.QuejaId == id)).GetValueOrDefault();
        }
    }
}
