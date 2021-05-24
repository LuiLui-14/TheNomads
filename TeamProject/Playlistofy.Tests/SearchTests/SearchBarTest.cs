/*
 * Benjamin Spencer
 * ID #177080482
 * As a user, I want to be able to search items on the webpage, so that I can find the playlists, tracks, artists, and/or albums I am trying to find more information about.
 */
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using Playlistofy.Data.Abstract;
using Playlistofy.Data.Concrete;
using Playlistofy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Playlistofy.Tests.SearchTests
{
    class SearchBarTest
    {
        private Mock<DbSet<T>> GetMockDbSet<T>(IQueryable<T> entities) where T : class
        {
            var mockSet = new Mock<DbSet<T>>();
            mockSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(entities.Provider);
            mockSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(entities.Expression);
            mockSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(entities.ElementType);
            mockSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(entities.GetEnumerator());
            return mockSet;
        }
        [Test]
        public void Search_ShouldReturn_ListofResults()
        {
            // Create a List of Playlists to use for DbSet
            List<Playlist> playlist = new List<Playlist>
            {
                new Playlist {Id = "a", UserId = "a", Href = "a", Name = "Test"},
                new Playlist {Id = "b", UserId = "b", Href = "b", Name = "AlsoTest"},
                new Playlist {Id = "c", UserId = "c", Href = "c", Name = "Bananas"}
            };
            //Mock the DbSet using the list above
            Mock<DbSet<Playlist>> mockPlaylistSet = GetMockDbSet(playlist.AsQueryable());
            //Mock the Context
            Mock<SpotifyDbContext> mockContext = new Mock<SpotifyDbContext>();
            //Ensure that a call to ctx returns the mocked DbSet
            mockContext.Setup(ctx => ctx.Set<Playlist>()).Returns(mockPlaylistSet.Object);
            //Mock the Playlist Repo using the mocked context
            IPlaylistRepository playlistRepo = new PlaylistRepository(mockContext.Object);
            //Setup search string
            string searchQuery = "Test";

            //Create list of search results from the repo with mocked context and mocked dbset
            List<Playlist> playlistList = playlistRepo.FindPlaylistsBySearch(searchQuery);

            //Verify that two results were returned
            Assert.That(playlistList.Count, Is.EqualTo(2));
        }

        [Test]
        public void TagSearch_ShouldReturn_ListofResults()
        {
            // Create a List of Playlists to use for DbSet
            List<Playlist> playlist = new List<Playlist>
            {
                new Playlist {Id = "a", UserId = "a", Href = "a", Name = "Test"},
                new Playlist {Id = "b", UserId = "b", Href = "b", Name = "AlsoTest"},
                new Playlist {Id = "c", UserId = "c", Href = "c", Name = "Bananas"}
            };

            List<PlaylistHashtagMap> maps = new List<PlaylistHashtagMap>
            {
                new PlaylistHashtagMap {Id = 1, PlaylistId = "a", HashtagId = 1},
                new PlaylistHashtagMap {Id = 2, PlaylistId = "b", HashtagId = 1},
                new PlaylistHashtagMap {Id = 3, PlaylistId = "a", HashtagId = 2},
                new PlaylistHashtagMap {Id = 4, PlaylistId = "b", HashtagId = 2},
                new PlaylistHashtagMap {Id = 5, PlaylistId = "c", HashtagId = 2}
            };

            List<Hashtag> tags = new List<Hashtag>
            {
                new Hashtag {Id = 1, HashTag1 = "#One", PlaylistHashtagMaps = maps.Where(i => i.HashtagId == 1).ToList() },
                new Hashtag {Id = 2, HashTag1 = "#Two", PlaylistHashtagMaps = maps.Where(i => i.HashtagId == 2).ToList() }
            };

            
            //Mock the DbSet using the list above
            Mock<DbSet<Playlist>> mockPlaylistSet = GetMockDbSet(playlist.AsQueryable());
            Mock<DbSet<Hashtag>> mockTagSet = GetMockDbSet(tags.AsQueryable());
            Mock<DbSet<PlaylistHashtagMap>> mockMapSet = GetMockDbSet(maps.AsQueryable());
            //Mock the Context
            Mock<SpotifyDbContext> mockContext = new Mock<SpotifyDbContext>();
            //Ensure that a call to ctx returns the mocked DbSet
            mockContext.Setup(ctx => ctx.Set<Playlist>()).Returns(mockPlaylistSet.Object);
            mockContext.Setup(ctx => ctx.Set<Hashtag>()).Returns(mockTagSet.Object);
            mockContext.Setup(ctx => ctx.Set<PlaylistHashtagMap>()).Returns(mockMapSet.Object);
            //Mock the Playlist Repo using the mocked context
            IPlaylistRepository playlistRepo = new PlaylistRepository(mockContext.Object);
            IHashtagRepository hashtagRepo = new HashtagRepository(mockContext.Object);
            //Setup search string
            string searchQuery = "#One";

            //Create list of search results from the repo with mocked context and mocked dbset
            List<Playlist> playlistList = hashtagRepo.SearchForPlaylist(searchQuery);

            //Verify that two results were returned
            Assert.That(playlistList.Count, Is.EqualTo(2));
        }
    }
}
