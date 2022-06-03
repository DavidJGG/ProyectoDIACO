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
    public class SucursalsController : Controller
    {
        private readonly ProyectoDIACOContext _context;
        private readonly AutenticacionService _autenticacionService;

        public SucursalsController(ProyectoDIACOContext context, AutenticacionService autenticacionService)
        {
            _context = context;
            _autenticacionService = autenticacionService;
        }

        // GET: Sucursals/id comercio
        public async Task<IActionResult> Index(int id)
        {
            if (!_autenticacionService.isAdmin(HttpContext))
            {
                if (_autenticacionService.isLogin(HttpContext))
                    return RedirectToAction("Index", "Home");

                return RedirectToAction("Index", "Login");
            }

            _autenticacionService.fillViewData(ViewData, HttpContext);

            ViewData["id_comercio"] = id;

            var proyectoDIACOContext = _context.Sucursal.Where(s=>s.ComercioId==id).Include(s => s.Comercio).Include(s => s.Ubicacion);
            return View(await proyectoDIACOContext.ToListAsync());
        }

        // GET: Sucursals/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (!_autenticacionService.isAdmin(HttpContext))
            {
                if (_autenticacionService.isLogin(HttpContext))
                    return RedirectToAction("Index", "Home");

                return RedirectToAction("Index", "Login");
            }

            _autenticacionService.fillViewData(ViewData, HttpContext);


            if (id == null || _context.Sucursal == null)
            {
                return NotFound();
            }

            var sucursal = await _context.Sucursal
                .Include(s => s.Comercio)
                .Include(s => s.Ubicacion)
                .FirstOrDefaultAsync(m => m.SucursalId == id);
            if (sucursal == null)
            {
                return NotFound();
            }

            return View(sucursal);
        }

        // GET: Sucursals/Create
        public IActionResult Create(int id)
        {
            if (!_autenticacionService.isAdmin(HttpContext))
            {
                if (_autenticacionService.isLogin(HttpContext))
                    return RedirectToAction("Index", "Home");

                return RedirectToAction("Index", "Login");
            }

            _autenticacionService.fillViewData(ViewData, HttpContext);

            ViewData["id_comercio"] = id;


            //ViewData["ComercioId"] = new SelectList(_context.Comercio, "ComercioId", "Nombre");
            
            ViewData["municipios"] = _context.Ubicacion.Where(u => u.Tipo==3).ToList();
            ViewData["departamentos"] = _context.Ubicacion.Where(u => u.Tipo == 2).ToList();
            ViewData["regiones"] = _context.Ubicacion.Where(u => u.Tipo == 1).ToList();
            return View();
        }

        // POST: Sucursals/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SucursalId,Nombre,Direccion,Telefono,MunicipioId,ComercioId")] Sucursal sucursal)
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
                sucursal.Estado = 1;
                _context.Add(sucursal);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Sucursals", new { id = sucursal.ComercioId});
            }
            ViewData["ComercioId"] = new SelectList(_context.Comercio, "ComercioId", "Nombre", sucursal.ComercioId);
            ViewData["MunicipioId"] = new SelectList(_context.Ubicacion, "UbicacionId", "UbicacionId", sucursal.MunicipioId);
            return View(sucursal);
        }

        // GET: Sucursals/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (!_autenticacionService.isAdmin(HttpContext))
            {
                if (_autenticacionService.isLogin(HttpContext))
                    return RedirectToAction("Index", "Home");

                return RedirectToAction("Index", "Login");
            }

            _autenticacionService.fillViewData(ViewData, HttpContext);

            if (id == null || _context.Sucursal == null)
            {
                return NotFound();
            }

            var sucursal = await _context.Sucursal.FindAsync(id);
            if (sucursal == null)
            {
                return NotFound();
            }
            //ViewData["ComercioId"] = new SelectList(_context.Comercio, "ComercioId", "Nombre", sucursal.ComercioId);

            Ubicacion municipio = _context.Ubicacion.Where(u => u.UbicacionId == sucursal.MunicipioId).FirstOrDefault();
            Ubicacion departamento = _context.Ubicacion.Where(u => u.UbicacionId == municipio.SububicacionId).FirstOrDefault();
            Ubicacion region = _context.Ubicacion.Where(u => u.UbicacionId == departamento.SububicacionId).FirstOrDefault();

            ViewData["muniOld"] = municipio.UbicacionId;
            ViewData["deptoOld"] = departamento.UbicacionId;
            ViewData["regOld"] = region.UbicacionId;

            ViewData["municipios"] = _context.Ubicacion.Where(u => u.Tipo == 3).ToList();
            ViewData["departamentos"] = _context.Ubicacion.Where(u => u.Tipo == 2).ToList();
            ViewData["regiones"] = _context.Ubicacion.Where(u => u.Tipo == 1).ToList();
            return View(sucursal);
        }

        // POST: Sucursals/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SucursalId,Nombre,Direccion,Telefono,Estado,MunicipioId,ComercioId")] Sucursal sucursal)
        {
            if (!_autenticacionService.isAdmin(HttpContext))
            {
                if (_autenticacionService.isLogin(HttpContext))
                    return RedirectToAction("Index", "Home");

                return RedirectToAction("Index", "Login");
            }

            _autenticacionService.fillViewData(ViewData, HttpContext);

            if (id != sucursal.SucursalId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sucursal);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SucursalExists(sucursal.SucursalId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index), new { id = sucursal.ComercioId});
            }
            ViewData["ComercioId"] = new SelectList(_context.Comercio, "ComercioId", "Nombre", sucursal.ComercioId);
            ViewData["MunicipioId"] = new SelectList(_context.Ubicacion, "UbicacionId", "UbicacionId", sucursal.MunicipioId);
            return View(sucursal);
        }

        // GET: Sucursals/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (!_autenticacionService.isAdmin(HttpContext))
            {
                if (_autenticacionService.isLogin(HttpContext))
                    return RedirectToAction("Index", "Home");

                return RedirectToAction("Index", "Login");
            }

            _autenticacionService.fillViewData(ViewData, HttpContext);

            if (id == null || _context.Sucursal == null)
            {
                return NotFound();
            }

            var sucursal = await _context.Sucursal
                .Include(s => s.Comercio)
                .Include(s => s.Ubicacion)
                .FirstOrDefaultAsync(m => m.SucursalId == id);
            if (sucursal == null)
            {
                return NotFound();
            }

            return View(sucursal);
        }

        // POST: Sucursals/Delete/5
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

            if (_context.Sucursal == null)
            {
                return Problem("Entity set 'ProyectoDIACOContext.Sucursal'  is null.");
            }
            var sucursal = await _context.Sucursal.FindAsync(id);
            if (sucursal != null)
            {
                if (sucursal.Estado == 1) sucursal.Estado = 0;
                else sucursal.Estado = 1;
                _context.Update(sucursal);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index), new { id=sucursal.ComercioId });
            }
            return RedirectToAction("Index","Usuarios");
        }

        public async Task<IEnumerable<Sucursal>> jsonSucursales(int id)
        {
            return _context.Sucursal.Where(s => s.ComercioId == id && s.Estado==1).Include(s=>s.Ubicacion);
        }

        private bool SucursalExists(int id)
        {
          return (_context.Sucursal?.Any(e => e.SucursalId == id)).GetValueOrDefault();
        }
    }
}
