using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SpotifyAPI.Web;
<<<<<<< HEAD
=======
using Playlistofy.Models;
>>>>>>> 8957ec8a5391f5ff66626eeb479bae5f4b033815

namespace Playlistofy.Models
{
    public class getCurrentUserInformation
    {
<<<<<<< HEAD
        private readonly UserManager<User> _userManager;
        private static string _spotifyClientId;
        private static string _spotifyClientSecret;

        public getCurrentUserInformation(UserManager<User> userManager, string spotifyClientId, string spotifyClientPassword)
=======
        private readonly UserManager<IdentityUser> _userManager;
        private static string _spotifyClientId;
        private static string _spotifyClientSecret;

        public getCurrentUserInformation(UserManager<IdentityUser> userManager, string spotifyClientId, string spotifyClientPassword)
>>>>>>> 8957ec8a5391f5ff66626eeb479bae5f4b033815
        {
            _userManager = userManager;
            _spotifyClientId = spotifyClientId;
            _spotifyClientSecret = spotifyClientPassword;
        }

<<<<<<< HEAD
        public async Task<User> GetCurrentUserInformation(SpotifyClient spotifyClient, string userSpotifyId)
        {
            User currentSpotifyUserInfo = new User();
=======
        public async Task<PUser> GetCurrentUserInformation(SpotifyClient spotifyClient, string userSpotifyId)
        {
            PUser currentSpotifyUserInfo = new PUser();
>>>>>>> 8957ec8a5391f5ff66626eeb479bae5f4b033815
            var userInfo = await spotifyClient.UserProfile.Get(userSpotifyId);

            currentSpotifyUserInfo.Href = userInfo.Href;
            currentSpotifyUserInfo.DisplayName = userInfo.DisplayName;
            currentSpotifyUserInfo.Followers = userInfo.Followers.Total;
            //currentSpotifyUserInfo.Images = userInfo.Images;
<<<<<<< HEAD
            currentSpotifyUserInfo.SpotifyUserId = userInfo.Id;
            //foreach(var next in currentSpotifyUserInfo.Images)
            //{
            //    currentSpotifyUserInfo.ImageUrl = next.Url;
           //}

            return currentSpotifyUserInfo;
        }


        //[HttpGet]
        //public async Task<string> GetCurrentUserId(IdentityUser _user)
        //{
        //    var personalData = new Dictionary<string, string>();
        //    var logins = await _userManager.GetLoginsAsync(_user);

        //    string key = "";
        //    foreach (var l in logins)
        //    {
        //        personalData.Add($"{l.LoginProvider} external login provider key", l.ProviderKey);
        //        key = l.ProviderKey;
        //    }

        //    return key;
        //}

        //public SpotifyClient makeSpotifyClient(string spotifyClientId, string spotifyClientSecret)
        //{
        //    SpotifyClientConfig config = SpotifyClientConfig
        //        .CreateDefault()
        //        .WithAuthenticator(new ClientCredentialsAuthenticator(spotifyClientId, spotifyClientSecret));

        //    SpotifyClient spotify = new SpotifyClient(config);

        //    return spotify;
        //}


    }
}
=======
            currentSpotifyUserInfo.Id = userInfo.Id;
            foreach (var next in userInfo.Images)
            {
                currentSpotifyUserInfo.ImageUrl = next.Url;
            }

            return currentSpotifyUserInfo;
        }
    }
}
>>>>>>> 8957ec8a5391f5ff66626eeb479bae5f4b033815
