using Expeditions.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace Expeditions.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ExpeditionsDbContext _context;

        public HomeController(ILogger<HomeController> logger, ExpeditionsDbContext context)
        {
            _logger = logger;
            _context = context;

        }

        public IActionResult Index()
        {
            ViewBag.News = _context.NewsArticles;
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
