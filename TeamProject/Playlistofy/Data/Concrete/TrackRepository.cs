using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Playlistofy.Models;
using Playlistofy.Data.Abstract;
using Microsoft.EntityFrameworkCore;

namespace Playlistofy.Data.Concrete
{
    public class TrackRepository : Repository<Track>, ITrackRepository
    {
        public TrackRepository(SpotifyDbContext ctx): base(ctx)
        {

        }

        public async Task AddTrackPlaylistMap(string tId, string pId)
        {
            _context.Add<PlaylistTrackMap>(new PlaylistTrackMap()
            {
                TrackId = tId,
                PlaylistId = pId
            });
            await _context.SaveChangesAsync();
        }

        public async Task RemoveTrackPlaylistMap(string tId, string pId)
        {
            _context.Remove<PlaylistTrackMap>(_context.Set<PlaylistTrackMap>().FirstOrDefault(i => i.TrackId == tId && i.PlaylistId == pId));
            await _context.SaveChangesAsync();
        }

        public List<Track> FindTracksBySearch(string searchQuery)
        {
            var t = _dbSet.Where(a => a.Name.Contains(searchQuery)).ToList();
            return t;
        }

        public List<PlaylistTrackMap> GetPlaylistTrackMaps(string Id)
        {
            List<Track> playlists = _dbSet.Include("PlaylistTrackMaps").ToList();
            Track t = _dbSet.Find(Id);
            List<PlaylistTrackMap> maps = t.PlaylistTrackMaps.ToList();
            return maps;
        }
        public IQueryable<Track> GetAllWithTrackMap()
        {
            var set = _context.Set<Track>().Include("PlaylistTrackMaps");
            return set;
        }

        public IQueryable<Track> GetAllWithTrackAlbumMap()
        {
            var set = _context.Set<Track>().Include("TrackAlbumMaps");
            return set;
        }

        
    }
}
