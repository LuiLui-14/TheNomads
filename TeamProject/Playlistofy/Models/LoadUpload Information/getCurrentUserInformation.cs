using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SpotifyAPI.Web;
using Playlistofy.Models;

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
    }
}