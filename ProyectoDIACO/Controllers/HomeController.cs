﻿using Microsoft.AspNetCore.Mvc;
using ProyectoDIACO.Models;
using ProyectoDIACO.Services;
using System.Diagnostics;

namespace ProyectoDIACO.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AutenticacionService _autenticacionService;
        

        public HomeController(ILogger<HomeController> logger, AutenticacionService ss)
        {
            _logger = logger;
            _autenticacionService = ss;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}