using Playlistofy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Playlistofy.Data.Abstract
{
    public interface ITrackRepository : IRepository<Track>
    {
        Task AddTrackPlaylistMap(string TrackId, string PlaylistId);
        public Task RemoveTrackPlaylistMap(string tId, string pId);
        public List<Track> FindTracksBySearch(string searchQuery);
        public List<PlaylistTrackMap> GetPlaylistTrackMaps(string Id);
        public IQueryable<Track> GetAllWithTrackMap();
        public IQueryable<Track> GetAllWithTrackAlbumMap();
    }
}
