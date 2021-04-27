using Microsoft.AspNetCore.Identity;
using Playlistofy.Models;
using SpotifyAPI.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Playlistofy.Utils.LoadUpload_Information
{
    public class getNewUser
    {
        public getNewUser()
        {

        }
        public async static Task<PUser> GetANewUser(SpotifyClient spotifyClient, string userSpotifyId, IdentityUser usr)
        {
            //Creates new PUser based on the passed in user
            PUser currentSpotifyUserInfo = new PUser()
            {
                Id = usr.Id,
                UserName = usr.UserName,
                NormalizedUserName = usr.NormalizedUserName,
                Email = usr.Email,
                NormalizedEmail = usr.NormalizedEmail,
                EmailConfirmed = usr.EmailConfirmed,
                PasswordHash = usr.PasswordHash,
                SecurityStamp = usr.SecurityStamp,
                ConcurrencyStamp = usr.ConcurrencyStamp,
                PhoneNumber = usr.PhoneNumber,
                PhoneNumberConfirmed = usr.PhoneNumberConfirmed,
                TwoFactorEnabled = usr.TwoFactorEnabled,
                LockoutEnd = usr.LockoutEnd,
                LockoutEnabled = usr.LockoutEnabled,
                AccessFailedCount = usr.AccessFailedCount,
                
            };

            //Gets the user spotify info from the user Spotify Id passed in
            var userInfo = await spotifyClient.UserProfile.Get(userSpotifyId);

            //Assigns the Spotify info to the new user
            currentSpotifyUserInfo.Href = userInfo.Href;
            currentSpotifyUserInfo.DisplayName = userInfo.DisplayName;
            currentSpotifyUserInfo.Followers = userInfo.Followers.Total;
            currentSpotifyUserInfo.SpotifyUserId = userInfo.Id;

            //Returns the new user
            return currentSpotifyUserInfo;
        }
    }
}
