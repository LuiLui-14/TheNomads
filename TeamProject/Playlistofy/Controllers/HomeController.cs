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
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Playlistofy.Data.Abstract;
using Playlistofy.Data.Concrete;
using Playlistofy.Models;
using Playlistofy.Utils;
using Playlistofy.Utils.LoadUpload_Information;
using SpotifyAPI.Web;

namespace Playlistofy.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;

        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration _config;
        private readonly IPlaylistofyUserRepository _pURepo;
        private readonly IPlaylistRepository _pRepo;
        private readonly ITrackRepository _tRepo;
<<<<<<< HEAD
        private static string _spotifyClientId;
        private static string _spotifyClientSecret;

        public HomeController(ILogger<HomeController> logger, IConfiguration config, UserManager<IdentityUser> userManager, IPlaylistofyUserRepository pURepo, IPlaylistRepository pRepo, ITrackRepository tRepo)
=======
        private readonly IArtistRepository _arRepo;
        private static string _spotifyClientId;
        private static string _spotifyClientSecret;

        public HomeController(ILogger<HomeController> logger, IConfiguration config, UserManager<IdentityUser> userManager, IPlaylistofyUserRepository pURepo, IPlaylistRepository pRepo, ITrackRepository tRepo, IArtistRepository arRepo)
>>>>>>> mcbride_artist_creation
        {
            _userManager = userManager;
            _logger = logger;
            _config = config;
            _pURepo = pURepo;
            _pRepo = pRepo;
            _tRepo = tRepo;
<<<<<<< HEAD
=======
            _arRepo = arRepo;
>>>>>>> mcbride_artist_creation
            _spotifyClientId = config["Spotify:ClientId"];
            _spotifyClientSecret = config["Spotify:ClientSecret"];
        }

        public async Task<IActionResult> Index()
        {
            if(_userManager.GetUserId(User) != null)
            {
<<<<<<< HEAD
                var uD = new UserData(_config, _userManager, _pURepo, _pRepo, _tRepo, _userManager.GetUserAsync(User).Result);
=======
                var uD = new UserData(_config, _userManager, _pURepo, _pRepo, _tRepo, _arRepo, _userManager.GetUserAsync(User).Result);
>>>>>>> mcbride_artist_creation
                await uD.SetUserData();
            }
            return View();
        }

        public async Task<IActionResult> SpotifyProfile()
        {
            var spotifyUserId = await GetUserId();

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

        [NonAction]
        private Task<IdentityUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        /*Delete upon Confirmation of working version in UTIL folder*/
        /*public async Task SetUserData()
        {
            var getUserPlaylists = new getCurrentUserPlaylists(_userManager, _spotifyClientId, _spotifyClientSecret);
            var getUserTracks = new getCurrentUserTracks(_userManager, _spotifyClientId, _spotifyClientSecret);
            var _spotifyClient = getUserPlaylists.makeSpotifyClient(_spotifyClientId, _spotifyClientSecret);
            IdentityUser usr = await GetCurrentUserAsync();
            string _userSpotifyId = await getUserPlaylists.GetCurrentUserId(usr);
            List<Playlist> Playlists = await getUserPlaylists.GetCurrentUserPlaylists(_spotifyClient, _userSpotifyId, usr.Id);
            if (_context.Pusers.Find(usr.Id) == null)
            {
                _context.Pusers.Add(await getNewUser.GetANewUser(_spotifyClient,_userSpotifyId, usr));
            }
            foreach (Playlist i in Playlists)
            {
                //if (_context.Playlists.Find(i.Id) == null)
                //{
                    List<Track> Tracks = await getUserTracks.GetPlaylistTrack(_spotifyClient, _userSpotifyId, i.Id);
                if (_context.Playlists.Find(i.Id) == null)
                {
                    _context.Playlists.Add(i);
                }
                    foreach (Track j in Tracks)
                    {
                        if (_context.Tracks.Find(j.Id) == null)
                        {
                            _context.Tracks.Add(j);
                            _context.PlaylistTrackMaps.Add(
                                new PlaylistTrackMap()
                                {
                                    PlaylistId = i.Id,
                                    TrackId = j.Id
                                }
                                );
                        }
                        Album a = getUserTracks.GetTrackAlbum(_spotifyClient, j.Id);
                        if (_context.Albums.Find(a.Id) == null)
                        {
                            _context.Albums.Add(a);
                            _context.TrackAlbumMaps.Add(
                                new TrackAlbumMap()
                                {
                                    AlbumId = a.Id,
                                    TrackId = j.Id
                                });
                        }
                    }
                //}
                
            }
            
            _context.SaveChanges();
        }*/
    }
}
