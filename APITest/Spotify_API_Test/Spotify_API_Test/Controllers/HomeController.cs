using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Spotify_API_Test.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using SpotifyAPI.Web;

namespace Spotify_API_Test.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private SpotifyClient spotify;
        private SpotifyClientConfig config;
        private ClientCredentialsTokenResponse response;
        private PublicUser User;

        public HomeController(ILogger<HomeController> logger)
        {
            
            _logger = logger;
        }

       
        public async Task<IActionResult> IndexAsync()
        {
            config = SpotifyClientConfig.CreateDefault();
            var request = new ClientCredentialsRequest("88c18aa421614ee48cdad5b244bfb443", "350665b3bd7b4fd2beaa9439d37de94e");
            response = await new OAuthClient(config).RequestToken(request);
            spotify = new SpotifyClient(config.WithToken(response.AccessToken));
            User = await spotify.UserProfile.Get("9cy4eylvf0g00rfqj7ldxdtfo");
            ViewBag.Track = User.DisplayName;
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
