using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MyTaskManager.Models;

namespace MyTaskManager.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
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
        public IActionResult Error(int? statusCode = null)
        {
            if (statusCode.HasValue)
            {
                ViewData["ErrorMessage"] = statusCode switch
                {
                    404 => "Página não encontrada.",
                    500 => "Ocorreu um erro no servidor.",
                    401 => "Você precisa estar autenticado para acessar esta página.",
                    403 => "Você não tem permissão para acessar esta página.",
                    _ => "Ocorreu um erro inesperado."
                };
            }
            else
            {
                ViewData["ErrorMessage"] = "Ocorreu um erro inesperado.";
            }

            return View();
        }
    }
}
