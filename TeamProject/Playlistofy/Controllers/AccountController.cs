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
<<<<<<< HEAD
        private readonly UserManager<User> _userManager;
=======
        private readonly UserManager<IdentityUser> _userManager;
        private SpotifyDBContext _SpotifyDB;
>>>>>>> 8957ec8a5391f5ff66626eeb479bae5f4b033815

        private readonly ILogger<AccountController> _logger;
        private readonly IConfiguration _config;

        private static string _spotifyClientId;
        private static string _spotifyClientSecret;

<<<<<<< HEAD
        public AccountController(ILogger<AccountController> logger, IConfiguration config, UserManager<User> userManager)
=======
        public AccountController(ILogger<AccountController> logger, IConfiguration config, UserManager<IdentityUser> userManager, SpotifyDBContext SpotifyDB)
>>>>>>> 8957ec8a5391f5ff66626eeb479bae5f4b033815
        {
            _userManager = userManager;
            _SpotifyDB = SpotifyDB;

            _logger = logger;
            _config = config;

            _spotifyClientId = config["Spotify:ClientId"];
            _spotifyClientSecret = config["Spotify:ClientSecret"];
        }

        public async Task<IActionResult> AccountPage()
        {
<<<<<<< HEAD
            //Instantiat viewModel
            var viewModel = new userPlaylistsTracks();

            //Finds current logged in user using identity 
            User usr = await GetCurrentUserAsync();
=======
            var viewModel = new userPlaylistsTracks();

            //Finds current logged in user using identity 
            IdentityUser usr = await GetCurrentUserAsync();
>>>>>>> 8957ec8a5391f5ff66626eeb479bae5f4b033815
            if (usr == null) { return View("~/Views/Home/Privacy.cshtml"); }

            //Instantiates the Model to call it's functions - Finds current logged in user's spotify ID
            var getUserPlaylists = new getCurrentUserPlaylists(_userManager, _spotifyClientId, _spotifyClientSecret);
            string _userSpotifyId = await getUserPlaylists.GetCurrentUserId(usr);
            if (_userSpotifyId == null || _userSpotifyId == "") { return View("~/Views/Home/Privacy.cshtml"); }

            //Create's client and then finds all playlists for current logged in user
            var _spotifyClient = getUserPlaylists.makeSpotifyClient(_spotifyClientId, _spotifyClientSecret);
            viewModel.Playlists = await getUserPlaylists.GetCurrentUserPlaylists(_spotifyClient, _userSpotifyId);

            //Get current logged in user's information
            var getUserInfo = new getCurrentUserInformation(_userManager, _spotifyClientId, _spotifyClientSecret);
            viewModel.User = await getUserInfo.GetCurrentUserInformation(_spotifyClient, _userSpotifyId);
<<<<<<< HEAD
=======

            /*
            //---------Testing With Database------------
            _SpotifyDB.Pusers.Add(viewModel.User);
            _SpotifyDB.SaveChanges();
            foreach (var playlist in viewModel.Playlists)
            {
                _SpotifyDB.Playlists.Add(playlist);
                _SpotifyDB.SaveChanges();
            }
            //---------Ending Testing----------------
            */
>>>>>>> 8957ec8a5391f5ff66626eeb479bae5f4b033815

            //return viewModel with information regarding playlists, tracks, and personal user's information
            return View(viewModel);
        }

        private Task<User> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);
    }
}