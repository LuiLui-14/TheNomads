using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SpotifyAPI.Web;

namespace Playlistofy.Models
{
    public class getCurrentUserInformation
    {
        private readonly UserManager<IdentityUser> _userManager;
        private static string _spotifyClientId;
        private static string _spotifyClientSecret;

        public getCurrentUserInformation(UserManager<IdentityUser> userManager, string spotifyClientId, string spotifyClientPassword)
        {
            _userManager = userManager;
            _spotifyClientId = spotifyClientId;
            _spotifyClientSecret = spotifyClientPassword;
        }

        public async Task<PUser> GetCurrentUserInformation(SpotifyClient spotifyClient, string userSpotifyId)
        {
            PUser currentSpotifyUserInfo = new PUser();
            var userInfo = await spotifyClient.UserProfile.Get(userSpotifyId);

            currentSpotifyUserInfo.Href = userInfo.Href;
            currentSpotifyUserInfo.DisplayName = userInfo.DisplayName;
            currentSpotifyUserInfo.Followers = userInfo.Followers.Total;
            //currentSpotifyUserInfo.Images = userInfo.Images;
            currentSpotifyUserInfo.SpotifyUserId = userInfo.Id;
            foreach (var next in userInfo.Images)
            {
                currentSpotifyUserInfo.ImageUrl = next.Url;
            }

            return currentSpotifyUserInfo;
        }


        /*[HttpGet]
        public async Task<string> GetCurrentUserId(IdentityUser _user)
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
        }*/
    }
}