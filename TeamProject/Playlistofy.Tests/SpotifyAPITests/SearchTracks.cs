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

namespace Playlistofy.Tests
{
    public class SearchTracks
    {
        //private SpotifyDbContext _context;
        private IConfiguration _config;

        private static string _spotifyClientId = "6bb2b5250a82433891189e5784c48253";
        private static string _spotifyClientSecret = "b7b40274492944ddbe84bbeb1b25f690";

        private static UserManager<IdentityUser> GetUserManager()
        {
            // Mock a user store, which the user manager needs to access the data layer, "contains methods for adding, removing and retrieving user claims."
            var mockStore = new Mock<IUserStore<IdentityUser>>();
            mockStore.Setup(x => x.FindByIdAsync("aabbcc", CancellationToken.None))
                .ReturnsAsync(new IdentityUser()
                {
                    UserName = "test@email.com",
                    Id = "aabbcc"
                });

            // Mock the user manager, only so far as it returns one valid user (can change this to return user not found for other tests)
            Mock<UserManager<IdentityUser>> mockUserManager = new Mock<UserManager<IdentityUser>>(mockStore.Object, null, null, null, null, null, null, null, null);

            mockUserManager.Setup(um => um.GetUserAsync(It.IsAny<ClaimsPrincipal>())).ReturnsAsync(
                new IdentityUser
                {
                    Id = "aabbcc",
                    Email = "test@email.com"
                });

            //// Mock the HttpContext since quite a bit of functionality comes from it
            //Mock<HttpContext> mockContext = new Mock<HttpContext>();
            //mockContext.SetupGet(ctx => ctx.User.Identity.Name).Returns("test@email.com");

            //HomeController controller = new HomeController(null, mockUserManager.Object, null, mockAppleRepo.Object)
            //{
            //    ControllerContext = new ControllerContext
            //    {
            //        HttpContext = mockContext.Object
            //    }
            //};

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

        [Test]
        public void makeSpotifyClient_ReturnsBackClientCheckIfEqual_SpotifyClient()
        {
            //Arrange
            //var setup = Setup(IConfiguration config);
            var Mangager = GetUserManager();

            //Act
            var SearchSpotify = new searchSpotify(Mangager, _spotifyClientId, _spotifyClientSecret);
            var SpotifyClient = SearchSpotify.makeSpotifyClient(_spotifyClientId, _spotifyClientSecret);

            //Assert
            Assert.That(SpotifyClient.Equals(SpotifyClient));
        }

        [Test]
        public void SearchTracks_ReturnsBackListCount_15()
        {
            //Arrange
            //var setup = Setup(IConfiguration config);
            var Mangager = GetUserManager();
            var list = Enumerable.Empty<Track>().AsQueryable();

            //Act
            var SearchSpotify = new searchSpotify(Mangager, _spotifyClientId, _spotifyClientSecret);
            var SpotifyClient = SearchSpotify.makeSpotifyClient(_spotifyClientId, _spotifyClientSecret);
            var trackCount = SearchSpotify.SearchTracks(SpotifyClient, "Summer of 69", list);
            //foreach(var track in trackCount.Result)
            //{

            //}
            
            //Assert
            Assert.That(trackCount.Result.Count(), Is.EqualTo(10));
        }

        [Test]
        public void SearchTracks_ReturnsBackAllDifferentTracks_true()
        {
            //Arrange
            //var setup = Setup(IConfiguration config);
            var Mangager = GetUserManager();
            var list = Enumerable.Empty<Track>().AsQueryable();

            //Act
            var SearchSpotify = new searchSpotify(Mangager, _spotifyClientId, _spotifyClientSecret);
            var SpotifyClient = SearchSpotify.makeSpotifyClient(_spotifyClientId, _spotifyClientSecret);
            var trackCount = SearchSpotify.SearchTracks(SpotifyClient, "Summer of 69", list);
            bool isUnique = trackCount.Result.Distinct().Count() == trackCount.Result.Count();

            //Assert
            Assert.That(isUnique, Is.EqualTo(true));
        }
    }
}
