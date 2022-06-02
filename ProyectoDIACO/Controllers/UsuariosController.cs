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
using ProyectoDIACO.Herramientas;

namespace ProyectoDIACO.Controllers
{
    public class UsuariosController : Controller
    {
        private readonly ProyectoDIACOContext _context;
        private readonly AutenticacionService _autenticacionService;
        private readonly CreateUserService _createUserService;
        private readonly UpdateUserService _updateUserService;

        public UsuariosController(ProyectoDIACOContext context, AutenticacionService autenticacionService, CreateUserService createUserService, UpdateUserService updateUserService)
        {
            _context = context;
            _autenticacionService = autenticacionService;
            _createUserService = createUserService;
            _updateUserService = updateUserService;
        }

        // GET: Usuarios
        public async Task<IActionResult> Index()
        {
            if (!_autenticacionService.isAdmin(HttpContext))
            {
                if (_autenticacionService.isLogin(HttpContext))
                    return RedirectToAction("Index", "Home");

                return RedirectToAction("Index", "Login");
            }               
        

            _autenticacionService.fillViewData(ViewData, HttpContext);

            return _context.Usuario != null ? 
                          View(await _context.Usuario.ToListAsync()) :
                          Problem("Entity set 'ProyectoDIACOContext.Usuario'  is null.");
        }

        // GET: Usuarios/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (!_autenticacionService.isAdmin(HttpContext))
            {
                if (_autenticacionService.isLogin(HttpContext))
                    return RedirectToAction("Index", "Home");

                return RedirectToAction("Index", "Login");
            }

            _autenticacionService.fillViewData(ViewData, HttpContext);

            if (id == null || _context.Usuario == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuario
                .FirstOrDefaultAsync(m => m.usuarioId == id);
            if (usuario == null)
            {
                return NotFound();
            }

            return View(usuario);
        }

        // GET: Usuarios/Create
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

        // POST: Usuarios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("usuarioId,Nombre,Apellido,Correo,Contrasena,Nit,Fecha,Rol")] Usuario usuario)
        {
            if (!_autenticacionService.isAdmin(HttpContext))
            {
                if (_autenticacionService.isLogin(HttpContext))
                    return RedirectToAction("Index", "Home");

                return RedirectToAction("Index", "Login");
            }

            if (ModelState.IsValid)
            {
                var result = await _createUserService.create(_context, usuario);

                if ((bool)result["err"] == true)
                {
                    ViewData["err"] = true;
                    ViewData["msg"] = result["msg"];
                    return View(usuario);
                }

                return RedirectToAction(nameof(Index));
            }
            return View(usuario);
        }

        // GET: Usuarios/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (!_autenticacionService.isAdmin(HttpContext))
            {
                if (_autenticacionService.isLogin(HttpContext))
                    return RedirectToAction("Index", "Home");

                return RedirectToAction("Index", "Login");
            }

            _autenticacionService.fillViewData(ViewData, HttpContext);

            if (id == null || _context.Usuario == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuario.FindAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }
            return View(usuario);
        }

        // POST: Usuarios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("usuarioId,Nombre,Apellido,Correo,Contrasena,Nit,Fecha,Rol")] Usuario usuario)
        {
            if (!_autenticacionService.isAdmin(HttpContext))
            {
                if (_autenticacionService.isLogin(HttpContext))
                    return RedirectToAction("Index", "Home");

                return RedirectToAction("Index", "Login");
            }


            if (id != usuario.usuarioId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var result = await _updateUserService.update(_context, usuario);

                if ((bool)result["err"]==true)
                {
                    ViewData["err"] = true;
                    ViewData["msg"] = result["msg"];
                    return View(usuario);
                }

                return RedirectToAction(nameof(Index));
            }

            return View(usuario);
        }

        // GET: Usuarios/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (!_autenticacionService.isAdmin(HttpContext))
            {
                if (_autenticacionService.isLogin(HttpContext))
                    return RedirectToAction("Index", "Home");

                return RedirectToAction("Index", "Login");
            }

            _autenticacionService.fillViewData(ViewData, HttpContext);

            if (id == null || _context.Usuario == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuario
                .FirstOrDefaultAsync(m => m.usuarioId == id);
            if (usuario == null)
            {
                return NotFound();
            }

            return View(usuario);
        }

        // POST: Usuarios/Delete/5
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

            if (_context.Usuario == null)
            {
                return Problem("Entity set 'ProyectoDIACOContext.Usuario'  is null.");
            }
            var usuario = await _context.Usuario.FindAsync(id);
            if (usuario != null)
            {
                _context.Usuario.Remove(usuario);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UsuarioExists(int id)
        {
          return (_context.Usuario?.Any(e => e.usuarioId == id)).GetValueOrDefault();
        }
    }
}
