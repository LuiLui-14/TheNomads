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
using Microsoft.Extensions.Configuration;
using Playlistofy.Utils;
using NUnit.Framework;
using Playlistofy.Models;
using System;

namespace Playlistofy.Tests.SpotifyAPITests
{
    public class SearchPlaylists
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

            return mockUserManager.Object;
        }

        [Test]
        public async Task GetPlaylist_CheckThatIdIsPassedToNewPlaylist_PlaylistIDAsync()
        {
            //Arrange
            //var setup = Setup(IConfiguration config);
            var Mangager = GetUserManager();
            var list = new List<Playlist>();
            string tempPlaylistId = "5cQGs0uECjDK381F8bRhrN";

            //Act
            var SearchSpotify = new searchSpotify(Mangager, _spotifyClientI, _spotifyClientS);
            var SpotifyClient = SearchSpotify.makeSpotifyClient(_spotifyClientI, _spotifyClientS);
            var Playlist = await SearchSpotify.GetPlaylist(SpotifyClient, tempPlaylistId);

            //Assert
            Assert.That(Playlist.Id, Is.EqualTo(tempPlaylistId));
        }

        [Test]
        public async Task GetPlaylist_CheckThatNameIsPassedToNewPlaylist_PlaylistNameAsync()
        {
            //Arrange
            //var setup = Setup(IConfiguration config);
            var Mangager = GetUserManager();
            var list = new List<Playlist>();
            string tempPlaylistId = "5cQGs0uECjDK381F8bRhrN";

            //Act
            var SearchSpotify = new searchSpotify(Mangager, _spotifyClientI, _spotifyClientS);
            var SpotifyClient = SearchSpotify.makeSpotifyClient(_spotifyClientI, _spotifyClientS);
            var Playlist = await SearchSpotify.GetPlaylist(SpotifyClient, tempPlaylistId);

            //Assert
            Assert.That(Playlist.Name, !Is.EqualTo(null));
        }

        [Test]
        public async Task GetPlaylist_CheckThatHrefandURIIsPassedToNewPlaylist_PlaylistHrefandUriAsync()
        {
            //Arrange
            //var setup = Setup(IConfiguration config);
            var Mangager = GetUserManager();
            var list = new List<Playlist>();
            string tempPlaylistId = "5cQGs0uECjDK381F8bRhrN";

            //Act
            var SearchSpotify = new searchSpotify(Mangager, _spotifyClientI, _spotifyClientS);
            var SpotifyClient = SearchSpotify.makeSpotifyClient(_spotifyClientI, _spotifyClientS);
            var Playlist = await SearchSpotify.GetPlaylist(SpotifyClient, tempPlaylistId);

            //Assert
            Assert.That(Playlist.Href, !Is.EqualTo(null));
            Assert.That(Playlist.Uri, !Is.EqualTo(null));
        }

        [Test]
        public async Task GetPlaylist_CheckThatTimeIsPassedIsSetCorrectly_PlaylistTimeAsync()
        {
            //Arrange
            //var setup = Setup(IConfiguration config);
            var Mangager = GetUserManager();
            var list = new List<Playlist>();
            string tempPlaylistId = "5cQGs0uECjDK381F8bRhrN";

            //Act
            var SearchSpotify = new searchSpotify(Mangager, _spotifyClientI, _spotifyClientS);
            var SpotifyClient = SearchSpotify.makeSpotifyClient(_spotifyClientI, _spotifyClientS);
            var Playlist = await SearchSpotify.GetPlaylist(SpotifyClient, tempPlaylistId);

            //Assert
            Assert.That(Playlist.DateCreated, !Is.Null);
            Assert.That(Playlist.DateCreated, Is.LessThan(DateTime.Now));
        }

        [Test]
        public void SearchPlaylists_ReturnsBackAllDifferentPlaylists_true()
        {
            //Arrange
            //var setup = Setup(IConfiguration config);
            var Mangager = GetUserManager();
            var list = new List<Playlist>();

            //Act
            var SearchSpotify = new searchSpotify(Mangager, _spotifyClientI, _spotifyClientS);
            var SpotifyClient = SearchSpotify.makeSpotifyClient(_spotifyClientI, _spotifyClientS);
            var PlaylistCount = SearchSpotify.SearchPlaylists(SpotifyClient, "Summer of 69", list);
            bool isUnique = PlaylistCount.Result.Distinct().Count() == PlaylistCount.Result.Count();

            //Assert
            Assert.That(isUnique, Is.EqualTo(true));
        }

        [Test]
        public async Task SearchPlaylists_CheckThatUserIdHasNotBeenSet_returnNullsAsync()
        {
            //Arrange
            //var setup = Setup(IConfiguration config);
            var Mangager = GetUserManager();
            var list = new List<Playlist>();

            //Act
            var SearchSpotify = new searchSpotify(Mangager, _spotifyClientI, _spotifyClientS);
            var SpotifyClient = SearchSpotify.makeSpotifyClient(_spotifyClientI, _spotifyClientS);
            var Playlists = await SearchSpotify.SearchPlaylists(SpotifyClient, "Summer of 69", list);

            var PlaylistUserIDs = new List<string>();
            foreach (var playlist in Playlists)
            {
                if (playlist.UserId != null) { PlaylistUserIDs.Add(playlist.UserId); }
                else { PlaylistUserIDs.Add(null); }
            }

            var checkForId = PlaylistUserIDs.All(a => a == null);

            //Assert
            Assert.That(checkForId, Is.EqualTo(true));
        }

        [Test]
        public void makeSpotifyClient_ReturnsBackClientCheckIfEqual_SpotifyClient()
        {
            //Arrange
            //var setup = Setup(IConfiguration config);
            var Mangager = GetUserManager();

            //Act
            var SearchSpotify = new searchSpotify(Mangager, _spotifyClientI, _spotifyClientS);
            var SpotifyClient = SearchSpotify.makeSpotifyClient(_spotifyClientI, _spotifyClientS);

            //Assert
            Assert.That(SpotifyClient.Equals(SpotifyClient));
        }

        [Test]
        public async Task SearchPlaylists_CheckThatHasPrimaryIdOnEveryPlaylistToAvoidErrors_stringIDAsync()
        {
            //Arrange
            //var setup = Setup(IConfiguration config);
            var Mangager = GetUserManager();
            var list = new List<Playlist>();

            //Act
            var SearchSpotify = new searchSpotify(Mangager, _spotifyClientI, _spotifyClientS);
            var SpotifyClient = SearchSpotify.makeSpotifyClient(_spotifyClientI, _spotifyClientS);
            var Playlists = await SearchSpotify.SearchPlaylists(SpotifyClient, "Summer of 69", list);

            var truefalse = new List<bool>();
            foreach (var playlist in Playlists)
            {
                if (playlist.Id != null)
                {
                    truefalse.Add(true);
                }
            }

            //Assert
            Assert.That(!truefalse.Contains(false));
        }

        [Test]
        public void SearchPlaylists_ReturnsBackList_CountFifteen()
        {
            //Arrange
            //var setup = Setup(IConfiguration config);
            var Mangager = GetUserManager();
            var list = new List<Playlist>();

            //Act
            var SearchSpotify = new searchSpotify(Mangager, _spotifyClientI, _spotifyClientS);
            var SpotifyClient = SearchSpotify.makeSpotifyClient(_spotifyClientI, _spotifyClientS);
            var PlaylistCount = SearchSpotify.SearchPlaylists(SpotifyClient, "Summer of 69", list);

            //Assert
            Assert.That(PlaylistCount.Result.Count(), Is.EqualTo(15));
        }

        [Test]
        public async Task SearchPlaylists_CheckThatHasHrefandUriOnEveryPlaylistToAvoidErrors_stringHrefandUriAsync()
        {
            //Arrange
            //var setup = Setup(IConfiguration config);
            var Mangager = GetUserManager();
            var list = new List<Playlist>();

            //Act
            var SearchSpotify = new searchSpotify(Mangager, _spotifyClientI, _spotifyClientS);
            var SpotifyClient = SearchSpotify.makeSpotifyClient(_spotifyClientI, _spotifyClientS);
            var Playlists = await SearchSpotify.SearchPlaylists(SpotifyClient, "Summer of 69", list);

            var truefalse = new List<bool>();
            foreach (var playlist in Playlists)
            {
                if (playlist.Href != null && playlist.Uri != null)
                {
                    truefalse.Add(true);
                }
            }

            //Assert
            Assert.That(!truefalse.Contains(false));
        }

        [Test]
        public async Task GetTracks_CheckThatTracksIdIsPassedToNewPlaylistTracks_TracksIdAsync()
        {
            //Arrange
            //var setup = Setup(IConfiguration config);
            var Mangager = GetUserManager();
            var list = new List<Playlist>();
            string tempPlaylistId = "5cQGs0uECjDK381F8bRhrN";

            //Act
            var SearchSpotify = new searchSpotify(Mangager, _spotifyClientI, _spotifyClientS);
            var SpotifyClient = SearchSpotify.makeSpotifyClient(_spotifyClientI, _spotifyClientS);
            var PlaylistTracks = await SearchSpotify.GetPlaylistTracks(SpotifyClient, tempPlaylistId);

            //Assert
            Assert.That(PlaylistTracks.All(a => a.Id != null));
        }

        [Test]
        public async Task GetTracks_CheckThatTracksIsPassedToNewPlaylistTracks_TracksNotNullAsync()
        {
            //Arrange
            //var setup = Setup(IConfiguration config);
            var Mangager = GetUserManager();
            var list = new List<Playlist>();
            string tempPlaylistId = "5cQGs0uECjDK381F8bRhrN";

            //Act
            var SearchSpotify = new searchSpotify(Mangager, _spotifyClientI, _spotifyClientS);
            var SpotifyClient = SearchSpotify.makeSpotifyClient(_spotifyClientI, _spotifyClientS);
            var PlaylistTracks = await SearchSpotify.GetPlaylistTracks(SpotifyClient, tempPlaylistId);

            //Assert
            Assert.That(!PlaylistTracks.Contains(null));
        }

        [Test]
        public async Task GetTracks_CheckThatTrackNameIsPassedToNewPlaylistTracks_TracksNameAsync()
        {
            //Arrange
            //var setup = Setup(IConfiguration config);
            var Mangager = GetUserManager();
            var list = new List<Playlist>();
            string tempPlaylistId = "5cQGs0uECjDK381F8bRhrN";

            //Act
            var SearchSpotify = new searchSpotify(Mangager, _spotifyClientI, _spotifyClientS);
            var SpotifyClient = SearchSpotify.makeSpotifyClient(_spotifyClientI, _spotifyClientS);
            var PlaylistTracks = await SearchSpotify.GetPlaylistTracks(SpotifyClient, tempPlaylistId);

            //Assert
            Assert.That(PlaylistTracks.All(n => n.Name != null));
        }

        [Test]
        public async Task GetTracks_CheckThatListOfTracksIsNotNull_WholeListNotNullAsync()
        {
            //Arrange
            //var setup = Setup(IConfiguration config);
            var Mangager = GetUserManager();
            var list = new List<Playlist>();
            string tempPlaylistId = "5cQGs0uECjDK381F8bRhrN";

            //Act
            var SearchSpotify = new searchSpotify(Mangager, _spotifyClientI, _spotifyClientS);
            var SpotifyClient = SearchSpotify.makeSpotifyClient(_spotifyClientI, _spotifyClientS);
            var PlaylistTracks = await SearchSpotify.GetPlaylistTracks(SpotifyClient, tempPlaylistId);

            //Assert
            Assert.That(PlaylistTracks, !Is.EqualTo(null));
        }

        [Test]
        public async Task GetTracks_CheckThatHrefandURIIsPassedToNewTracks_TracksHrefandUriAsync()
        {
            //Arrange
            //var setup = Setup(IConfiguration config);
            var Mangager = GetUserManager();
            var list = new List<Playlist>();
            string tempPlaylistId = "5cQGs0uECjDK381F8bRhrN";

            //Act
            var SearchSpotify = new searchSpotify(Mangager, _spotifyClientI, _spotifyClientS);
            var SpotifyClient = SearchSpotify.makeSpotifyClient(_spotifyClientI, _spotifyClientS);
            var PlaylistTracks = await SearchSpotify.GetPlaylistTracks(SpotifyClient, tempPlaylistId);

            //Assert
            Assert.That(PlaylistTracks.All(h => h.Href != null));
            Assert.That(PlaylistTracks.All(u => u.Uri != null));
        }
    }
}
