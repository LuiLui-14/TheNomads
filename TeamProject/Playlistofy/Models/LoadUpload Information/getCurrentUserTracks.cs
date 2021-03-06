using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SpotifyAPI.Web;

namespace Playlistofy.Models
{
    public class getCurrentUserTracks
    {
        private readonly UserManager<IdentityUser> _userManager;
        private static string _spotifyClientId;
        private static string _spotifyClientSecret;

        public getCurrentUserTracks(UserManager<IdentityUser> userManager, string spotifyClientId, string spotifyClientPassword)
        {
            _userManager = userManager;
            _spotifyClientId = spotifyClientId;
            _spotifyClientSecret = spotifyClientPassword;
        }



        public async Task<List<Track>> GetCurrentUserTrack(SpotifyClient spotifyClient, string userSpotifyId)
        {
            List<Track> spotifyPlaylists = new List<Track>();
            var playlists = await spotifyClient.Playlists.GetUsers(userSpotifyId);

            foreach (var playlist in playlists.Items)
            {
                
            }

            return spotifyPlaylists;
        }
    }
}
