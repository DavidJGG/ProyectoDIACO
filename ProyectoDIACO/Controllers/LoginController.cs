using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProyectoDIACO.Data;
using ProyectoDIACO.Models;
using ProyectoDIACO.Services;

namespace ProyectoDIACO.Controllers
{
    public class LoginController : Controller
    {
        private readonly ProyectoDIACOContext _context;
        private readonly AutenticacionService _autenticacionService;
        private readonly CreateUserService _createUserService;

        public LoginController(ProyectoDIACOContext context, AutenticacionService autenticacionService, CreateUserService createUserService)
        {
            _context = context;
            _autenticacionService = autenticacionService;
            _createUserService = createUserService;
        }
        public IActionResult Index()
        {
            _autenticacionService.fillViewData(ViewData, HttpContext);
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login([Bind("Correo, Contrasena")] LoginModel loginModel)
        {
            ViewData["err"] = false;
            ViewData["msg"] = "";
            if(_autenticacionService.autenticar(HttpContext, _context, loginModel.Correo, loginModel.Contrasena))
            {
                return RedirectToAction("Index", "Usuarios");
            }

            ViewData["err"] = true;
            ViewData["msg"] = "Usuario o contraseña incorrecta";
            return View("Index");
        }

        public IActionResult Registrase()
        {
            _autenticacionService.fillViewData(ViewData, HttpContext);
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Crearusuario([Bind("usuarioId,Nombre,Apellido,Correo,Contrasena,Nit,Fecha")] Usuario usuario)
        {
            if (!ModelState.IsValid)
            {
                ViewData["err"] = true;
                ViewData["msg"] = "Error en inesperado en el servidor";
                return View("Registrase");
            }
            usuario.Rol = 2;

            Dictionary<string, object> result = await _createUserService.create(_context, usuario);

            if ((bool)result["err"] == true)
            {
                ViewData["err"] = true;
                ViewData["msg"] = result["msg"];
                return View("Registrase");
            }

            return View("Index");
        }

        public IActionResult Salir()
        {
            _autenticacionService.destruirSesion(HttpContext);
            return RedirectToAction("Index", "Login");
        }
    }
}
