using Microsoft.AspNetCore.Mvc;

namespace Playlistofy.Controllers
{
    public class ArtistController : Controller
    {
        // GET
        public IActionResult Index()
        {
            return View();
        }
    }
}