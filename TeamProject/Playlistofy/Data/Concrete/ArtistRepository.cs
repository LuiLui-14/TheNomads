using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Playlistofy.Data.Abstract;
using Playlistofy.Models;

namespace Playlistofy.Data.Concrete
{
    public class ArtistRepository : Repository<Artist>, IArtistRepository
    {
        public ArtistRepository(SpotifyDbContext ctx) : base(ctx)
        {

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
    }
}
