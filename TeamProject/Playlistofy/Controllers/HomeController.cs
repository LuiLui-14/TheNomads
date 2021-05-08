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

        private static string _spotifyClientId;
        private static string _spotifyClientSecret;

        private readonly IArtistRepository _arRepo;
        private readonly IAlbumRepository _aRepo;

        public HomeController(ILogger<HomeController> logger, IConfiguration config, UserManager<IdentityUser> userManager, IPlaylistofyUserRepository pURepo, IPlaylistRepository pRepo, ITrackRepository tRepo, IAlbumRepository aRepo, IArtistRepository arRepo)
        {
            _userManager = userManager;
            _logger = logger;
            _config = config;
            _pURepo = pURepo;
            _pRepo = pRepo;
            _tRepo = tRepo;
            _aRepo = aRepo;
            _arRepo = arRepo;
            _spotifyClientId = config["Spotify:ClientId"];
            _spotifyClientSecret = config["Spotify:ClientSecret"];
        }

        public async Task<IActionResult> IndexAsync()
        {
            var UserPlaylistPlaylist = new HomePageViewModel();

            UserPlaylistPlaylist.UserID = _userManager.GetUserId(User);

            //UserPlaylistPlaylist.RecentPlaylists = await _pRepo.GetMostRecentPlaylists_5Async();

            if (_userManager.GetUserId(User) != null)
            {
                //var uD = new UserData(_config, _userManager, _pURepo, _pRepo, _tRepo, _aRepo, _arRepo, _userManager.GetUserAsync(User).Result);

                //await uD.SetUserData();

                var tempUser = _userManager.GetUserId(User);
                var newPlaylist = _pRepo.GetAll().Include("PlaylistTrackMaps").Where(name => name.UserId == tempUser).ToList();

                UserPlaylistPlaylist.UserPlaylists = newPlaylist;
            }

            return View(UserPlaylistPlaylist);
            //return View(new List<Playlist>());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> IndexAsync([Bind("SearchingPlaylistParameter")] HomePageViewModel viewModel)
        {
            var UserPlaylistPlaylist = new HomePageViewModel();

            if (_userManager.GetUserId(User) != null)
            {
                //var uD = new UserData(_config, _userManager, _pURepo, _pRepo, _tRepo, _aRepo, _arRepo, _userManager.GetUserAsync(User).Result);

                //await uD.SetUserData();

                var tempUser = _userManager.GetUserId(User);
                var newPlaylist = _pRepo.GetAll().Include("PlaylistTrackMaps").Where(name => name.UserId == tempUser).ToList();

                UserPlaylistPlaylist.UserPlaylists = newPlaylist;
            }

            //Checks if the model coming in is valid with the specified parameter
            if (ModelState.IsValid)
            {
                //Gets the current logged in user's ID, where it will be passed to the view. If null then, certain features won't work
                UserPlaylistPlaylist.UserID = _userManager.GetUserId(User);

                //If the search parameter coming in is null, then this will fail
                if (viewModel.SearchingPlaylistParameter != null || viewModel.SearchingPlaylistParameter.Length >= 1)
                {
                    //Get's the playlists depending on the searched parameter
                    var searchedPlaylists = _pRepo.FindPlaylistsBySearch(viewModel.SearchingPlaylistParameter);

                    //Create new list of strings that will hold the user for each playlist. Different ways of doing this, and not the best way, cause I essentially have to lists
                    var UserNameList = new List<string>();

                    //Counter so that no more than 5 playlists will be rendered at a time
                    var count = 0;
                    foreach (var playlist in searchedPlaylists)
                    {
                        //Once it hits 5 it will break
                        if (count > 4) { break; }

                        // If the playlist does not equal nyull then it will pull the userId from every playlist and then use that to find the user
                        // Once finding the user, we can extract the email out of it
                        if (playlist != null)
                        {
                            var userId = playlist.UserId;
                            var playlistUser = await _userManager.FindByIdAsync(userId);
                            UserNameList.Add(playlistUser.Email);
                        }
                        ++count;
                    }
                    //List of the users that were found in the above code are stored in this list
                    UserPlaylistPlaylist.SearchedPlaylistsUsers = UserNameList;

                    //List of the actual playlists with set information except the user email of the one who created them. Hence the list above does this
                    UserPlaylistPlaylist.SearchedPlaylists = searchedPlaylists;
                }
                //UserPlaylistPlaylist.RecentPlaylists = await _pRepo.GetMostRecentPlaylists_5Async();
                return View(UserPlaylistPlaylist);
            }
            //UserPlaylistPlaylist.RecentPlaylists = await _pRepo.GetMostRecentPlaylists_5Async();

            return View(UserPlaylistPlaylist);
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

        [HttpGet]
        public async Task<IActionResult> WebPlayer(string id)
        {
            var viewModel = new userPlaylistsTracks();

            //Finds current logged in user using identity 
            IdentityUser usr = await GetCurrentUserAsync();
            if (usr == null) { return RedirectToPage("/Account/Login", new { area = "Identity" }); }

            //Instantiates the Model to call it's functions - Finds current logged in user's spotify ID
            var getUserPlaylists = new getCurrentUserPlaylists(_userManager, _spotifyClientId, _spotifyClientSecret);
            string _userSpotifyId = await getUserPlaylists.GetCurrentUserId(usr);
            if (_userSpotifyId == null || _userSpotifyId == "") { return RedirectToPage("/Account/Login", new { area = "Identity" }); }

            //Create's client and then finds all playlists for current logged in user
            var _spotifyClient = getCurrentUserPlaylists.makeSpotifyClient(_spotifyClientId, _spotifyClientSecret);
            viewModel.Playlists = await getUserPlaylists.GetCurrentUserPlaylists(_spotifyClient, _userSpotifyId, usr.Id);

            //Get current logged in user's information
            var getUserInfo = new getCurrentUserInformation(_userManager, _spotifyClientId, _spotifyClientSecret);
            viewModel.User = await getUserInfo.GetCurrentUserInformation(_spotifyClient, _userSpotifyId);

            //Get current logged in user's tracks
            var getUserTracks = new getCurrentUserTracks(_userManager, _spotifyClientId, _spotifyClientSecret);

            if (await _pRepo.FindByIdAsync(id) == null)
            {
                var newPlaylist = viewModel.Playlists.Where(name => name.Id == id);
                foreach (var playlist in newPlaylist)
                {
                    Console.WriteLine(playlist.Id);

                    if (await _pRepo.FindByIdAsync(id) == null)
                    {
                        List<Track> Tracks = await getUserTracks.GetPlaylistTrack(_spotifyClient, _userSpotifyId, playlist.Id);
                        await _pRepo.AddAsync(playlist);
                        foreach (Track track in Tracks)
                        {
                            Console.WriteLine(track.Id);
                            if (await _tRepo.FindByIdAsync(track.Id) == null)
                            {
                                await _tRepo.AddAsync(track);
                            }
                            await _tRepo.AddTrackPlaylistMap(track.Id, playlist.Id);
                        }
                    }
                }
            }

            var currentUserID = await _userManager.GetUserIdAsync(usr);
            var userPlaylists = _pRepo.GetAllWithUser().Where(i => i.User.Id == currentUserID);

            viewModel._PlaylistsDB = await userPlaylists.ToListAsync();
            return View("WebPlayer", viewModel);
        }
    }
}
