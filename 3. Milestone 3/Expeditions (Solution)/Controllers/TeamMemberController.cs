using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Expeditions.Models;
using Microsoft.EntityFrameworkCore;

namespace Expeditions.Controllers
{
    public class TeamMemberController : Controller
    {
        private readonly ExpeditionsDbContext db;

        public TeamMemberController(ExpeditionsDbContext context)
        {
            db = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult NothingHere()
        {
            return View();
        }

        //[HttpGet]
        //public IActionResult SearchTeamMember(string teamMemberName)
        //{

        //    if (string.IsNullOrEmpty(teamMemberName) == false)
        //    {
        //        if (db.TeamMembers.Where(a => a.LastName.Contains(teamMemberName)).Count() < 25)
        //        {
        //            var teamMembers = db.TeamMembers.Where(c => c.LastName.Contains(teamMemberName));
        //            return View(teamMembers);
        //        }
        //        else if (db.TeamMembers.Where(a => a.LastName.Contains(teamMemberName)).Count() == 0)
        //        {
        //            return View("NothingHere");
        //        }
        //        else
        //        {

        //            return View("teamMemberError");
        //        }
        //    }
        //    return View("Index");
        //}

        [HttpGet]
        public async Task<IActionResult> SearchTeamMember(string teamMemberName)
        {
            ViewBag.CurrentFilter = teamMemberName;

            var climbers = db.TeamMembers
                .AsQueryable();

            if (!String.IsNullOrEmpty(teamMemberName))
            {
                //id = UppercaseFirst(teamMemberName);
                climbers = climbers.Where(s => s.FirstName.StartsWith(teamMemberName)|| s.LastName.StartsWith(teamMemberName));
            }
            else
            {
                climbers = climbers.Where(s => s.FirstName.Contains(null) || s.LastName.StartsWith(teamMemberName));

                return View(await climbers.ToListAsync());
            }

            return View(await climbers.ToListAsync());
        }
    }
}
