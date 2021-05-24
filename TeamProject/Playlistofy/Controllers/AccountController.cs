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
using Playlistofy.Models.ViewModel;
using Playlistofy.Utils;
using SpotifyAPI.Web.Auth;

namespace Playlistofy.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        

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

        public async Task<IActionResult> AccountPage(string Redirect)
        {
            if(Redirect == "redirect")
            {
                // Make sure "spotifyapi.web.oauth://token" is in your applications redirect URIs!
                var loginRequest = new LoginRequest(
                    new Uri("https://playlistofy.azurewebsites.net/Playlists/UploadPlaylistofyPlaylists/"),
                    /*new Uri("https://localhost:5001/Playlists/UploadPlaylistofyPlaylists/"),*/
                    _spotifyClientId,
                    LoginRequest.ResponseType.Code
                )
                {
                    Scope = new[] { Scopes.PlaylistModifyPrivate, Scopes.PlaylistModifyPublic, Scopes.PlaylistReadCollaborative, Scopes.UserFollowModify,
                        Scopes.PlaylistReadPrivate, Scopes.UserLibraryModify, Scopes.UserModifyPlaybackState}
                };
                var TempUri = loginRequest.ToUri();
                //return TempUri;
                // This call requires Spotify.Web.Auth
                BrowserUtil.Open(TempUri);
            }

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


            //---------Testing------------
            //var SearchSpotify = new searchSpotify(_userManager, _spotifyClientId, _spotifyClientSecret);
            //var SearchTracks = SearchSpotify.SearchTracks(usr, _spotifyClient);
            //---------Ending Testing----------------


            //return viewModel with information regarding playlists, tracks, and personal user's information
            return View(viewModel);
        }

        private Task<IdentityUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);
    }
}