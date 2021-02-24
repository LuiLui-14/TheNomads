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

namespace Playlistofy.Controllers
{
    public class AccountController : Controller
    {
        private SpotifyDBContext spotifyDBContext;
        private readonly ILogger<AccountController> _logger;
        private readonly IConfiguration _config;

        private static string _spotifyClientId;
        private static string _spotifyClientSecret;

        public AccountController(ILogger<AccountController> logger, IConfiguration config, SpotifyDBContext spotifyDbContext)
        {
            _logger = logger;
            _config = config;
            spotifyDBContext = spotifyDbContext;

            _spotifyClientId = config["Spotify:ClientId"];
            _spotifyClientSecret = config["Spotify:ClientSecret"];
        }

        public async Task<IActionResult> AccountPage()
        {
            List<Playlist> spotifyPlaylists = new List<Playlist>();

            var config = SpotifyClientConfig
                .CreateDefault()
                .WithAuthenticator(new ClientCredentialsAuthenticator(_spotifyClientId, _spotifyClientSecret));

            var spotify = new SpotifyClient(config);
            var playlists = await spotify.Playlists.GetUsers("t478u0ocda142ua3oybwasoig");

            //--------------Just temp User so it works in Database ----------------

            var user = new User();
            var userInfo = await spotify.UserProfile.Get("t478u0ocda142ua3oybwasoig");
            user.Id = userInfo.Id;
            user.UserName = userInfo.DisplayName;
            user.Email = "tempEmail";
            user.EmailConfirmed = true;
            user.PhoneNumberConfirmed = false;
            user.TwoFactorEnabled = false;
            user.LockoutEnabled = false;
            spotifyDBContext.Users.Add(user);
            spotifyDBContext.SaveChanges();
            //------------------------------End Testing----------------------------

            foreach (var playlist in playlists.Items)
            {
                var tempPlaylist = new Playlist()
                {
                    Name = playlist.Name,
                    Id = playlist.Id,
                    Description = playlist.Description,
                    Public = playlist.Public,
                    Collaborative = playlist.Collaborative,
                    Href = playlist.Href,
                    UserId = playlist.Owner.Id,
                    Uri = playlist.Uri
                };

                spotifyPlaylists.Add(tempPlaylist);
                spotifyDBContext.Playlists.Add(tempPlaylist);
                spotifyDBContext.SaveChanges();
            }
            

            return View(spotifyPlaylists);
        }
    }
}

