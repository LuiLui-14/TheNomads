using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Playlistofy.Data.Abstract;
using Playlistofy.Models;
using Playlistofy.Models.ViewModel;
using SpotifyAPI.Web;
using SpotifyAPI.Web.Http;

namespace Playlistofy.Utils
{
    public class UploadToSpotifyUtil
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IPlaylistRepository _pRepo;
        private readonly ITrackRepository _tRepo;
        private static string _spotifyClientId;
        private static string _spotifyClientSecret;
        IdentityUser _usr;

        public UploadToSpotifyUtil(UserManager<IdentityUser> userManager, IPlaylistRepository pRepo, ITrackRepository tRepo, IdentityUser usr, string spotifyClientId, string SpotifyClientSecret)
        {
            _userManager = userManager;
            _pRepo = pRepo;
            _tRepo = tRepo;
            _usr = usr;
            _spotifyClientId = spotifyClientId;
            _spotifyClientSecret = SpotifyClientSecret;
        }

        public async Task<List<Playlist>> browsePlaylists(SearchingSpotifyPlaylists viewModel)
        {
            Console.WriteLine("Getting through");
            //Creates searchSpotify folder with necessary functions to use later
            var SearchSpotify = new searchSpotify(_userManager, _spotifyClientId, _spotifyClientSecret);
            //Creates spotify client
            var _spotifyClient = SearchSpotify.makeSpotifyClient(_spotifyClientId, _spotifyClientSecret);
            //Search and return a list of tracks
            var browsePlaylists = await SearchSpotify.GetTopPlaylists(_spotifyClient, viewModel.PersonalPlaylists);

            return browsePlaylists;
        }

        
    }
}
