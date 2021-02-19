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
        private readonly ILogger<AccountController> _logger;
        private readonly IConfiguration _config;

        private static string _spotifyClientId;
        private static string _spotifyClientSecret;

        public AccountController(ILogger<AccountController> logger, IConfiguration config)
        {
            _logger = logger;
            _config = config;

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
            var playlists = await spotify.Playlists.GetUsers("Jose");

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
                    Href = playlist.Href,
                    SnapshotId = playlist.SnapshotId,
                    Type = playlist.Type,
                    Uri = playlist.Uri,

                    Images = playlist.Images,
                    Tracks = playlist.Tracks
                });
            }

            return View(spotifyPlaylists);
        }
    }
}

