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
using Playlistofy.Models.ViewModel;
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
        private readonly IArtistRepository _arRepo;
        private readonly IAlbumRepository _aRepo;
        private readonly IHashtagRepository _hRepo;
        private readonly IKeywordRepository _kRepo;
        private static string _spotifyClientId;
        private static string _spotifyClientSecret;

        public HomeController(ILogger<HomeController> logger, IConfiguration config, UserManager<IdentityUser> userManager, IPlaylistofyUserRepository pURepo, IPlaylistRepository pRepo, ITrackRepository tRepo, IAlbumRepository aRepo, IArtistRepository arRepo, IHashtagRepository hRepo, IKeywordRepository kRepo)
        {
            _userManager = userManager;
            _logger = logger;
            _config = config;
            _pURepo = pURepo;
            _pRepo = pRepo;
            _tRepo = tRepo;
            _aRepo = aRepo;
            _arRepo = arRepo;
            _hRepo = hRepo;
            _kRepo = kRepo;
            _spotifyClientId = config["Spotify:ClientId"];
            _spotifyClientSecret = config["Spotify:ClientSecret"];
        }

        public IActionResult Index()
        {
            if(_userManager.GetUserId(User) != null)
            {
                var uD = new UserData(_config, _userManager, _pURepo, _pRepo, _tRepo, _aRepo, _arRepo, _userManager.GetUserAsync(User).Result);

                //await uD.SetUserData();

                var tempUser = _userManager.GetUserId(User);
                var newPlaylist = _pRepo.GetAll().Include("PlaylistTrackMaps").Where(name => name.UserId == tempUser).ToList();

                return View(newPlaylist);
            }

            return View(new List<Playlist>());
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

            var spotifyClient = getCurrentUserPlaylists.makeSpotifyClient(_spotifyClientId, _spotifyClientSecret);

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
        [HttpPost]
        public JsonResult AutoComplete(string prefix, string searchType)
        {
            if (searchType == "Album")
            {
                var albums = _aRepo.GetAll().Where(i => i.Name.StartsWith(prefix));
                var words = new List<ModelforAuto>();
                foreach(var a in albums)
                {
                    words.Add(new ModelforAuto()
                    {
                        Id = a.Id,
                        Label = a.Name
                    });
                }
                return Json(words);
            }
            else if (searchType == "Playlist")
            {
                var playlists = _pRepo.GetAll().Where(i => i.Name.StartsWith(prefix));
                var words = new List<ModelforAuto>();
                foreach (var a in playlists)
                {
                    words.Add(new ModelforAuto()
                    {
                        Id = a.Id,
                        Label = a.Name
                    });
                }
                return Json(words);
            }
            else if (searchType == "Track")
            {
                var tracks = _tRepo.GetAll().Where(i => i.Name.StartsWith(prefix));
                var words = new List<ModelforAuto>();
                foreach (var a in tracks)
                {
                    words.Add(new ModelforAuto()
                    {
                        Id = a.Id,
                        Label = a.Name
                    });
                }
                return Json(words);
            }
            else if (searchType == "Artist")
            {
                var artists = _arRepo.GetAll().Where(i => i.Name.StartsWith(prefix));
                var words = new List<ModelforAuto>();
                foreach (var a in artists)
                {
                    words.Add(new ModelforAuto()
                    {
                        Id = a.Id,
                        Label = a.Name
                    });
                }
                return Json(words);
            }
            else if (searchType == "Tags")
            {
                var hash = _hRepo.GetAll().Where(i => i.HashTag1.StartsWith(prefix));
                var key = _kRepo.GetAll().Where(i => i.Keyword1.StartsWith(prefix));
                var words = new List<TagsModel>();
                foreach (var h in hash)
                {
                    words.Add(new TagsModel()
                    {
                        Id = h.Id,
                        label = h.HashTag1
                    });
                }
                foreach (var k in key)
                {
                    words.Add(new TagsModel()
                    {
                        Id = k.Id,
                        label = k.Keyword1
                    });
                }

                return Json(words);
            }else
            {
                return null;
            }
        }
    }
}
