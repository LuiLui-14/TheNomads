using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Playlistofy.Data.Abstract;
using Playlistofy.Models;

namespace Playlistofy.Data.Concrete
{
    public class ArtistRepository : Repository<Artist>, IArtistRepository
    {
        private DbSet<ArtistTrackMap> ArtistMaps;

        public ArtistRepository(SpotifyDbContext ctx) : base(ctx)
        {
            ArtistMaps = _context.Set<ArtistTrackMap>();
        }

        public async Task AddArtistTrackMap(string aId, string tId)
        {
            _context.Add<ArtistTrackMap>(new ArtistTrackMap()
            {
                ArtistId = aId,
                TrackId = tId
            });
            await _context.SaveChangesAsync();
        }

        public async Task AddArtistAlbumMap(string aId, string alBId)
        {
            _context.Add<ArtistAlbumMap>(new ArtistAlbumMap()
            {
                ArtistId = aId,
                AlbumId = alBId
            });
            await _context.SaveChangesAsync();
        }

        public List<Artist> FindArtistsBySearch(string searchQuery)
        {
            var t = _dbSet.Where(a => a.Name.Contains(searchQuery)).ToList();
            return t;
        }

        public ArtistTrackMap GetArtistTrackMap(string tId)
        {
            var map = ArtistMaps.Where(i => i.TrackId == tId).FirstOrDefault();
            return map;
        }

        public virtual async Task DeleteArtistTrackMapAsync(ArtistTrackMap ArtisttrackMap)
        {
            if (ArtisttrackMap == null)
            {
                throw new Exception("Entity to delete was null");
            }
            else
            {

                ArtistMaps.Remove(ArtisttrackMap);
                await _context.SaveChangesAsync();
            }
            return;
        }
    }
}
