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
            List<Playlist> playlist = new List<Playlist>
            {
                new Playlist {Id = "a", UserId = "a", Href = "a", Name = "Test"},
                new Playlist {Id = "b", UserId = "b", Href = "b", Name = "AlsoTest"},
                new Playlist {Id = "c", UserId = "c", Href = "c", Name = "Bananas"}
            };
            Mock<DbSet<Playlist>> mockPlaylistSet = GetMockDbSet(playlist.AsQueryable());
            Mock<SpotifyDbContext> mockContext = new Mock<SpotifyDbContext>();
            mockContext.Setup(ctx => ctx.Playlists).Returns(mockPlaylistSet.Object);
            IPlaylistRepository playlistRepo = new PlaylistRepository(mockContext.Object);
            string searchQuery = "Test";

            List<Playlist> playlistList = playlistRepo.FindPlaylistsBySearch(searchQuery);

            Assert.That(playlistList.Count, Is.EqualTo(2));

        }
    }
}
