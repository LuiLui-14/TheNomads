using Playlistofy.Models;
using SpotifyAPI.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Playlistofy.Data.Abstract
{
    public interface IAlbumRepository: IRepository<Album>
    {
        public Task<Album> GetTrackAlbum(SpotifyClient _spotifyClient, string TrackId);
        public Task AddAlbumTrackMap(Album a, Track t);
        public Task AddArtistAlbumMap(string aId, string alId);
        public Task<List<Track>> GetAllAlbumTracks(SpotifyClient _spotifyClient, Album a);
        public List<Album> FindAlbumsBySearch(string searchQuery);

        public Task<TrackAlbumMap> GetAlbumTrackMap(string tId, SpotifyClient spotty);
        public Task DeleteAlbumTrackMapAsync(TrackAlbumMap AlbumTrackMap);
        public Task<Album> GetAlbumFromTrack(string tId, SpotifyClient spotty);
    }
}
