using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using Playlistofy.Models;
using Playlistofy.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using System.Threading;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Playlistofy.Controllers;
using SpotifyAPI.Web;

namespace Playlistofy.Tests
{
    public class UploadPlaylists
    {
        //private SpotifyDbContext _context;
        private IConfiguration _config;

        private static string _spotifyClientI = "";
        private static string _spotifyClientS = "";

        private static UserManager<IdentityUser> GetUserManager()
        {
            // Mock a user store, which the user manager needs to access the data layer, "contains methods for adding, removing and retrieving user claims."
            var mockStore = new Mock<IUserStore<IdentityUser>>();
            mockStore.Setup(x => x.FindByIdAsync("aabbcc", CancellationToken.None))
                .ReturnsAsync(new IdentityUser() {
                    UserName = "test@email.com",
                    Id = "aabbcc" });

            // Mock the user manager, only so far as it returns one valid user (can change this to return user not found for other tests)
            Mock<UserManager<IdentityUser>> mockUserManager = new Mock<UserManager<IdentityUser>>(mockStore.Object, null, null, null, null, null, null, null, null);

            mockUserManager.Setup(um => um.GetUserAsync(It.IsAny<ClaimsPrincipal>())).ReturnsAsync(
                new IdentityUser {
                    Id = "aabbcc",
                    Email = "test@email.com" });

            return mockUserManager.Object;
        }

        [SetUp]
        public void Setup()
        {
            //IConfiguration config;
            //_config = config;

            //_spotifyClientId = config["Spotify:ClientId"];
            //_spotifyClientSecret = config["Spotify:ClientSecret"];
        }

        public static async Task<bool> IsAdmin(IUserProfileClient userProfileClient)
        {
            // get logged in user
            var user = await userProfileClient.Current();

            // only my user id is an admin
            return user.Id == "1122095781";
        }

        [Test]
        public async Task IsAdmin_SuccessTest()
        {
            Mock<IUserProfileClient> userProfileClient = new Mock<IUserProfileClient>();
            userProfileClient.Setup(user => user.Current()).Returns(
              Task.FromResult(new PrivateUser
              {
                  Id = "1122095781"
              })
            );

            Assert.AreEqual(true, await IsAdmin(userProfileClient.Object));
        }

        [Test]
        public void GetTopPlaylists_GetsFifteenTopPlaylists_Returns15()
        {
            //Arrange
            //var setup = Setup(IConfiguration config);
            var Mangager = GetUserManager();
            var list = new List<Playlist>();

            //Act
            var SearchSpotify = new searchSpotify(Mangager, _spotifyClientI, _spotifyClientS);
            var SpotifyClient = SearchSpotify.makeSpotifyClient(_spotifyClientI, _spotifyClientS);
            var FeaturedPlaylists = SearchSpotify.GetTopPlaylists(SpotifyClient, list);

            //Assert
            Assert.That(FeaturedPlaylists.Result.Count(), Is.LessThanOrEqualTo(15));
        }

        [Test]
        public void GetTopPlaylists_GetsNothingBackIfPlaylistsAreTheSame_ReturnsBackCount0()
        {
            //Arrange
            //var setup = Setup(IConfiguration config);
            var Mangager = GetUserManager();
            var list = new List<Playlist>();

            //Act
            var SearchSpotify = new searchSpotify(Mangager, _spotifyClientI, _spotifyClientS);
            var SpotifyClient = SearchSpotify.makeSpotifyClient(_spotifyClientI, _spotifyClientS);
            var FeaturedPlaylists = SearchSpotify.GetTopPlaylists(SpotifyClient, list);

            var SamePlaylists = SearchSpotify.GetTopPlaylists(SpotifyClient, FeaturedPlaylists.Result);

            //Assert
            Assert.That(SamePlaylists.Result.Count(), Is.EqualTo(0));
        }
    }
}
