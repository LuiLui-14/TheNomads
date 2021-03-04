using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SpotifyAPI.Web;
using Playlistofy.Models;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.IO;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Identity;
using System;

using System.Linq;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Playlistofy.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        static string privateKey;

        private readonly ILogger<AccountController> _logger;
        private readonly IConfiguration _config;

        private static string _spotifyClientId;
        private static string _spotifyClientSecret;

        public AccountController(ILogger<AccountController> logger, IConfiguration config, UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
            _logger = logger;
            _config = config;

            _spotifyClientId = config["Spotify:ClientId"];
            _spotifyClientSecret = config["Spotify:ClientSecret"];
        }

        [HttpGet]
        public async Task<string> GetCurrentUserId()
        {
            IdentityUser usr = await GetCurrentUserAsync();
            var personalData = new Dictionary<string, string>();
            // var user = await _userManager.GetUserAsync(User);
            var logins = await _userManager.GetLoginsAsync(usr);

            string key = "";
            foreach (var l in logins)
            {
                personalData.Add($"{l.LoginProvider} external login provider key", l.ProviderKey);
                key = l.ProviderKey;
            }
            foreach (var data in personalData)
            {
                Console.WriteLine(data);
            }

            Console.WriteLine(privateKey);
            Console.WriteLine(usr.Id);
            Console.WriteLine(usr.UserName);

            return key;
        }

        private Task<IdentityUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);

        public async Task<IActionResult> AccountPage()
        {
            string temp1 = await GetCurrentUserId();

            List<Playlist> spotifyPlaylists = new List<Playlist>();

            var config = SpotifyClientConfig
                .CreateDefault()
                .WithAuthenticator(new ClientCredentialsAuthenticator(_spotifyClientId, _spotifyClientSecret));

            //var conf = SpotifyClientConfig.CreateDefault(privateKey);
            //string temp = privateKey.ToString();
            var spotify = new SpotifyClient(config);
            //var playlists = await spotify.Playlists.CurrentUsers();
            if(temp1 == null || temp1 == "")
            {
                return View("~/Views/Home/Privacy.cshtml");
            }
            var playlists = await spotify.Playlists.GetUsers(temp1);

            //var user = await spotify.UserProfile.Get(temp1);

            ViewBag.Total = playlists.Total;
            foreach (var playlist in playlists.Items)
            {
                spotifyPlaylists.Add(new Playlist()
                {
                    Name = playlist.Name,
                    Id = playlist.Id,
                    Description = playlist.Description,
                    Public = playlist.Public,
                    Collaborative = playlist.Collaborative,
                    Href = playlist.Href
                });
            }

            Console.WriteLine(privateKey);
            return View(spotifyPlaylists);
        } 
        
    }
}

