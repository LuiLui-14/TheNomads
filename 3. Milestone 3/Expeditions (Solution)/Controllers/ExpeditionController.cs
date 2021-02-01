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
    public class ExpeditionController : Controller
    {
        private readonly ILogger<ExpeditionController> _logger;
        private readonly ExpeditionsDbContext _db;

        public ExpeditionController(ILogger<ExpeditionController> logger, ExpeditionsDbContext context)
        {
            _logger = logger;
            _db = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Search_Peak(string id)
        {
            ViewBag.CurrentFilter = id;

            var mountains = _db.Peaks
                .Include(x => x.Expeditions)
                .AsQueryable();

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

        [HttpGet]
        public IActionResult Search_Expedition(int? id)
        {
            ViewBag.CurrentFilter = id;

            var mountains = _db.Peaks
                .Include(x => x.Expeditions)
                .ThenInclude(y => y.TrekkingAgency)
                .AsQueryable();

            if (id != null)
            {
                mountains = mountains.Where(s => s.Id.Equals(id));
            }

            return View(mountains);
        }
    }
}
