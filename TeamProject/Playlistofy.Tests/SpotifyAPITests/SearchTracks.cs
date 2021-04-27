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
        private SpotifyDbContext _context;
        private IConfiguration _config;

        private static string _spotifyClientId;
        private static string _spotifyClientSecret;

        //private static HomeController GetHomeControllerWithLoggedInUser()
        //{
        //    // Mock a user store, which the user manager needs to access the data layer, "contains methods for adding, removing and retrieving user claims."
        //    var mockStore = new Mock<IUserStore<IdentityUser>>();
        //    mockStore.Setup(x => x.FindByIdAsync("aabbcc", CancellationToken.None))
        //        .ReturnsAsync(new IdentityUser()
        //        {
        //            UserName = "test@email.com",
        //            Id = "aabbcc"
        //        });

        //    // Mock the user manager, only so far as it returns one valid user (can change this to return user not found for other tests)
        //    Mock<UserManager<IdentityUser>> mockUserManager = new Mock<UserManager<IdentityUser>>(mockStore.Object, null, null, null, null, null, null, null, null);

        //    mockUserManager.Setup(um => um.GetUserAsync(It.IsAny<ClaimsPrincipal>())).ReturnsAsync(
        //        new IdentityUser
        //        {
        //            Id = "aabbcc",
        //            Email = "test@email.com"
        //        });

        //    // Mock the HttpContext since quite a bit of functionality comes from it
        //    Mock<HttpContext> mockContext = new Mock<HttpContext>();
        //    mockContext.SetupGet(ctx => ctx.User.Identity.Name).Returns("test@email.com");

        //    HomeController controller = new HomeController(null, mockUserManager.Object, null, mockAppleRepo.Object)
        //    {
        //        ControllerContext = new ControllerContext
        //        {
        //            HttpContext = mockContext.Object
        //        }
        //    };

        //    return controller;
        //}

        [SetUp]
        public void Setup(SpotifyDbContext context, IConfiguration config)
        {
            _context = context;
            _config = config;

            _spotifyClientId = config["Spotify:ClientId"];
            _spotifyClientSecret = config["Spotify:ClientSecret"];
        }

        [Test]
        public void Test1()
        {
            Assert.Pass();
        }

        protected void tearDown()
        {
        }
    }
}
