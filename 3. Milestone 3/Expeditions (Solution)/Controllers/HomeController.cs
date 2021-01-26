using Expeditions.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;

namespace Expeditions.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ExpeditionsDbContext _db;

        public HomeController(ILogger<HomeController> logger, ExpeditionsDbContext context)
        {
            _logger = logger;
            _db = context;
        }

        public IActionResult Index()
        {
            ViewBag.News = _context.NewsArticles;
            return View();
        }

        [HttpPost]
        public IActionResult DisplayInjuries()
        {
            string[] info = { };
            int counter = 0;

            foreach (var expo in _db.Expeditions)
            {
                if (expo.InjurySustained == false) continue;
                var nameAndYear = (expo.Peak.Name + " " + expo.Year);
                info[counter] = nameAndYear;
                ++counter;
            }

            return View("Index", info);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
