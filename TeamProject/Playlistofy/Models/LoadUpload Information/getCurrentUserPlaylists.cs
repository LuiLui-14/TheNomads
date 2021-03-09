using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SpotifyAPI.Web;

namespace Playlistofy.Models
{
    public class getCurrentUserPlaylists
    {
        private readonly UserManager<User> _userManager;
        private static string _spotifyClientId;
        private static string _spotifyClientSecret;

        public getCurrentUserPlaylists(UserManager<User> userManager, string spotifyClientId, string spotifyClientPassword)
        {
            _userManager = userManager;
            _spotifyClientId = spotifyClientId;
            _spotifyClientSecret = spotifyClientPassword;
        }

        [HttpGet]
        public async Task<string> GetCurrentUserId(User _user)
        {
            var personalData = new Dictionary<string, string>();
            var logins = await _userManager.GetLoginsAsync(_user);

            string key = "";
            foreach (var l in logins)
            {
                personalData.Add($"{l.LoginProvider} external login provider key", l.ProviderKey);
                key = l.ProviderKey;
            }

            return key;
        }

        public SpotifyClient makeSpotifyClient(string spotifyClientId, string spotifyClientSecret)
        {
            SpotifyClientConfig config = SpotifyClientConfig
                .CreateDefault()
                .WithAuthenticator(new ClientCredentialsAuthenticator(spotifyClientId, spotifyClientSecret));

            SpotifyClient spotify = new SpotifyClient(config);

            return spotify;
        }

        public async Task<List<Playlist>> GetCurrentUserPlaylists(SpotifyClient spotifyClient, string userSpotifyId)
        {
            List<Playlist> spotifyPlaylists = new List<Playlist>();
            var playlists = await spotifyClient.Playlists.GetUsers(userSpotifyId);
            FullPlaylist fullplaylist = null;
            foreach (var playlist in playlists.Items)
            {
                fullplaylist = await spotifyClient.Playlists.Get(playlist.Id);
                
                spotifyPlaylists.Add(new Playlist()
                {

                    Name = fullplaylist.Name,
                    Id = fullplaylist.Id,
                    Description = fullplaylist.Description,
                    Public = fullplaylist.Public,
                    Collaborative = fullplaylist.Collaborative,
                    Href = fullplaylist.Href,
                    Uri = fullplaylist.Uri
                    
                }) ;
                
            }
            

            return spotifyPlaylists;
        }
    }  
}
