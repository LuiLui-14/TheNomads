using Microsoft.AspNetCore.Mvc;

namespace Expeditions.Controllers
{
    public class ExpeditionController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
