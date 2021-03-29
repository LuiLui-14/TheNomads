using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Playlistofy.Models;
using SpotifyAPI.Web;

namespace Playlistofy.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;

        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration _config;
        private readonly UserManager<User> _userManager;
        private static string _spotifyClientId;
        private static string _spotifyClientSecret;
        private ApplicationDbContext _context;

<<<<<<< HEAD
        public HomeController(ILogger<HomeController> logger, IConfiguration config, UserManager<User> userManager, ApplicationDbContext context)
=======
        private static string _spotifyClientId;
        private static string _spotifyClientSecret;

        public HomeController(ILogger<HomeController> logger, IConfiguration config, UserManager<IdentityUser> userManager)
>>>>>>> 8957ec8a5391f5ff66626eeb479bae5f4b033815
        {
            _userManager = userManager;
            _logger = logger;
            _config = config;
<<<<<<< HEAD
            _userManager = userManager;
            _spotifyClientId = config["Spotify:ClientId"];
            _spotifyClientSecret = config["Spotify:ClientSecret"];
            _context = context;
=======

            _spotifyClientId = config["Spotify:ClientId"];
            _spotifyClientSecret = config["Spotify:ClientSecret"];
>>>>>>> 8957ec8a5391f5ff66626eeb479bae5f4b033815
        }

        public async Task<IActionResult> Index()
        {
            if (_userManager.GetUserId(User) != null)
            {
                await SetUserData();
            }
            return View();
        }

        public async Task<IActionResult> SpotifyProfile()
        {
            var spotifyUserId = await GetUserId();
<<<<<<< HEAD


            return Redirect("https://open.spotify.com/user/" + spotifyUserId);
=======

            return Redirect("https://open.spotify.com/user/" + spotifyUserId);
        }

        [HttpGet]
        private async Task<string> GetUserId()
        {
            IdentityUser usr = await GetCurrentUserAsync();

            var getUserPlaylists = new getCurrentUserPlaylists(_userManager, _spotifyClientId, _spotifyClientSecret);
            string _userSpotifyId = await getUserPlaylists.GetCurrentUserId(usr);

            var spotifyClient = getUserPlaylists.makeSpotifyClient(_spotifyClientId, _spotifyClientSecret);

            var spotifyUserInfo = await spotifyClient.UserProfile.Get(_userSpotifyId);

            string user = spotifyUserInfo.Id;

            return user;
        }

        private Task<IdentityUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);
        public IActionResult Privacy()
        {
            return View();
>>>>>>> 8957ec8a5391f5ff66626eeb479bae5f4b033815
        }

        [HttpGet]
        private async Task<string> GetUserId()
        {
            var getUserPlaylists = new getCurrentUserPlaylists(_userManager, _spotifyClientId, _spotifyClientSecret);
            var usr = await GetCurrentUserAsync();
            string _userSpotifyId = await getUserPlaylists.GetCurrentUserId(usr);

            var spotifyClient = getUserPlaylists.makeSpotifyClient(_spotifyClientId, _spotifyClientSecret);

            var spotifyUserInfo = await spotifyClient.UserProfile.Get(_userSpotifyId);

            var user = spotifyUserInfo.Id;

            return user;
        }


        //This Method retrieves and returns the access code needed by spotify for a user to be able to access the API.
        private async Task<string> GetAccessToken()
        {
            SpotifyToken token = new SpotifyToken();

            string postString = string.Format("grant_type=client_credentials");
            byte[] byteArray = Encoding.UTF8.GetBytes(postString);

            string url = "https://accounts.spotify.com/api/token";
            WebRequest request = WebRequest.Create(url);
            request.Method = "POST";
            request.Headers.Add("Authorization", "Basic ODhjMThhYTQyMTYxNGVlNDhjZGFkNWIyNDRiZmI0NDM6MzUwNjY1YjNiZDdiNGZkMmJlYWE5NDM5ZDM3ZGU5NGU=");
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = byteArray.Length;
            using (Stream dataStream = request.GetRequestStream())
            {
                dataStream.Write(byteArray, 0, byteArray.Length);
                using (WebResponse response = await request.GetResponseAsync())
                {
                    using (Stream responseStream = response.GetResponseStream())
                    {
                        using (StreamReader reader = new StreamReader(responseStream))
                        {
                            string responseFromServer = reader.ReadToEnd();
                            token = JsonConvert.DeserializeObject<SpotifyToken>(responseFromServer);
                        }
                    }
                }
            }
            return token.access_token;
            }

        public async Task SetUserData()
        {
            var getUserPlaylists = new getCurrentUserPlaylists(_userManager, _spotifyClientId, _spotifyClientSecret);
            var getUserTracks = new getCurrentUserTracks(_userManager, _spotifyClientId, _spotifyClientSecret);
            var _spotifyClient = getUserPlaylists.makeSpotifyClient(_spotifyClientId, _spotifyClientSecret);
            User usr = await GetCurrentUserAsync();
            string _userSpotifyId = await getUserPlaylists.GetCurrentUserId(usr);
            List<Playlist> Playlists = await getUserPlaylists.GetCurrentUserPlaylists(_spotifyClient, _userSpotifyId);
            List<Track> Tracks = await getUserTracks.GetPlaylistTrack(_spotifyClient, _userSpotifyId);
            foreach (Playlist i in Playlists) {
                if (!_context.Playlist.Any(x => x.Id == i.Id))
                {
                    _context.Playlist.Add(i);
                }
            }
            foreach (Track i in Tracks)
            {
                if (_context.Tracks.Find(i.Id) == null)
                {
                    _context.Tracks.Add(i);
                }
            }

            var userInfo = await _spotifyClient.UserProfile.Get(_userSpotifyId);
            usr.Followers = userInfo.Followers.Total;
            usr.DisplayName = userInfo.DisplayName;
            usr.SpotifyUserId = userInfo.Id;

            _context.SaveChanges();
            var t = new getCurrentUserTracks(_userManager, _spotifyClientId, _spotifyClientSecret);
        }
        private Task<User> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);

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


