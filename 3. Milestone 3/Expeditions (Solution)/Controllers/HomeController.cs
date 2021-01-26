using Expeditions.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;

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
            ViewBag.News = _db.NewsArticles;
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

        [HttpGet]
        public async Task<IActionResult> Search_Database(string id)
        {
            ViewBag.CurrentFilter = id;

            var mountains = _db.Peaks
<<<<<<< HEAD
                                .Include(x => x.Expeditions)
                                .AsQueryable();
=======
                .Include(x => x.Expeditions)
                .AsQueryable();
>>>>>>> 3b9a34d3e7a86c4880d4412c39979a07a5e41d01

            if (!String.IsNullOrEmpty(id))
            {
                id = UppercaseFirst(id);
                mountains = mountains.Where(s => s.Name.StartsWith(id));
            }
            else
            {
                mountains = mountains.Where(s => s.Name.Contains(null));

                return View(await mountains.ToListAsync());
            }

            return View(await mountains.ToListAsync());
        }

        private string UppercaseFirst(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return string.Empty;
            }
            return char.ToUpper(id[0]) + id.Substring(1);
        }
    }
}
