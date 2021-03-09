using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Playlistofy.Controllers;
using SpotifyAPI.Web;


namespace Playlistofy.Models
{
    public class SpotifyUtilities
    {
        private string _spotifyClientId;
        private string _spotifyClientSecret;

        public SpotifyUtilities(string spotifyClientId, string spotifyClientSecret)
        {
            _spotifyClientId = spotifyClientId;
            _spotifyClientSecret = spotifyClientSecret;
        }

        public async Task<PublicUser> GetSpotifyUserAsync(string providerKey)
        {
            var config = SpotifyClientConfig
                .CreateDefault()
                .WithAuthenticator(new ClientCredentialsAuthenticator(_spotifyClientId, _spotifyClientSecret));


            var spotify = new SpotifyClient(config);

            return await spotify.UserProfile.Get(providerKey);
        }
    }
}
