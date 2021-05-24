
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Playlistofy.Data.Abstract;
using Playlistofy.Models;
using SpotifyAPI.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Playlistofy.Utils.LoadUpload_Information;

namespace Playlistofy.Data.Concrete
{
    public class AlbumRepository : Repository<Album>, IAlbumRepository
    {
        private DbSet<TrackAlbumMap> AlbumMaps;

        public AlbumRepository(SpotifyDbContext ctx) : base(ctx)
        {
            AlbumMaps = _context.Set<TrackAlbumMap>();
        }

        public async Task<Album> GetTrackAlbum(SpotifyClient _spotifyClient, string TrackId)
        {
            var aId = await _spotifyClient.Tracks.Get(TrackId);
            FullAlbum fullAlbum = await _spotifyClient.Albums.Get(aId.Album.Id);
            Album album = new Album()
            {
                AlbumType = fullAlbum.AlbumType,
                Id = fullAlbum.Id,
                Label = fullAlbum.Label,
                Name = fullAlbum.Name,
                Popularity = fullAlbum.Popularity,
                ReleaseDate = fullAlbum.ReleaseDate,
                ReleaseDatePrecision = fullAlbum.ReleaseDatePrecision
            };
            return album;
        }

        public async Task AddAlbumTrackMap(Album a, Track t)
        {
            _context.Add(new TrackAlbumMap()
            {
                TrackId = t.Id,
                AlbumId = a.Id
            });
            await _context.SaveChangesAsync();
        }

        public async Task AddArtistAlbumMap(string aId, string alId)
        {
            _context.Add<ArtistAlbumMap>(new ArtistAlbumMap()
            {
                ArtistId = aId,
                AlbumId = alId
            });
            await _context.SaveChangesAsync();
        }

        public async Task<List<Track>> GetAllAlbumTracks(SpotifyClient _spotifyClient, Album a)
        {
            var t = await _spotifyClient.Albums.GetTracks(a.Id);
            List<Track> tracks = new List<Track>();
            foreach(var m in t.Items)
            {
                if(await _context.FindAsync<Track>(m.Id) == null)
                {
                    FullTrack n = await _spotifyClient.Tracks.Get(m.Id);
                    Track j = new Track()
                    {
                        DiscNumber = n.DiscNumber,
                        DurationMs = n.DurationMs,
                        Duration = Utils.AlgorithmicOperations.MsConversion.ConvertMsToMinSec(n.DurationMs),
                        Explicit = n.Explicit,
                        Href = n.Href,
                        Id = n.Id,
                        IsPlayable = n.IsPlayable,
                        Name = n.Name,
                        Popularity = n.Popularity,
                        PreviewUrl = n.PreviewUrl,
                        TrackNumber = n.TrackNumber,
                        Uri = n.Uri,
                        IsLocal = n.IsLocal
                    };
                    await _context.AddAsync<Track>(j);
                    await _context.SaveChangesAsync();
                    await AddAlbumTrackMap(a, j);
                }
                tracks.Add(await _context.FindAsync<Track>(m.Id));
            }
            return tracks;

        }

        public List<Album> FindAlbumsBySearch(string searchQuery)
        {
            var t = _dbSet.Where(a => a.Name.Contains(searchQuery)).ToList();
            return t;
        }

        public async Task<TrackAlbumMap> GetAlbumTrackMap(string tId, SpotifyClient spotty)
        {
            var map = AlbumMaps.Include("Album").Where(i => i.TrackId == tId).FirstOrDefault();
            if (map == null)
            {
                Track t = _context.Set<Track>().Where(i => i.Id == tId).FirstOrDefault();
                Album a = await GetTrackAlbum(spotty, tId);
                await AddAlbumTrackMap(a, t);
                var newMap = AlbumMaps.Include("Album").Where(i => i.TrackId == tId).FirstOrDefault();
                return newMap;
            }
            return map;
        }

        public virtual async Task DeleteAlbumTrackMapAsync(TrackAlbumMap AlbumTrackMap)
        {
            if (AlbumTrackMap == null)
            {
                throw new Exception("Entity to delete was null");
            }
            else
            {
                AlbumMaps.Remove(AlbumTrackMap);
                await _context.SaveChangesAsync();
            }
            return;
        }

        public async Task<Album> GetAlbumFromTrack(string tId, SpotifyClient spotty)
        {
            if (tId == null)
            {
                throw new Exception("Track Id provided is null");
            }

            var map = await GetAlbumTrackMap(tId, spotty);

            return map.Album;
        }
    }
}
