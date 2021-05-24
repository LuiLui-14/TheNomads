using Playlistofy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Playlistofy.Data.Abstract
{
    public interface IPlaylistRepository : IRepository<Playlist>
    {
        public List<Playlist> FindPlaylistsBySearch(string searchQuery);
        public IQueryable<Playlist> GetAllWithUser();
        public List<Track> GetAllPlaylistTracks(Playlist playlist);
        public List<PlaylistTrackMap> GetPlaylistTrackMaps(string Id);
        public Playlist GetPlaylistWithAllMaps(string id);
        public Task DeleteTrackMapAsync(PlaylistTrackMap trackMap);
        public Task RemoveFollowedPlaylist(int Id);
        public PlaylistTrackMap GetPlaylistTrackMap(string tId, string pId);
        public Task<List<Playlist>> GetMostRecentPlaylists_5Async();
        public Task AddTrackPlaylistMap(string pUId, string pId);
        public Task DeletePlaylistMap(string Uid, string pId);
        public List<Playlist> GetAllForUser(string id);
        public List<Playlist> GetAllPublicForUser(string id);

        public Task<List<Playlist>> GetAllFollowedPlaylists(string id);
        public Task<List<Playlist>> GetAllLikedPlaylists(string id);
        public Task LikePlaylist(string DispName, string pId);
        public Task UnlikePlaylist(string DispName, string pId);
    }

    
}
