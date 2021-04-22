using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Playlistofy.Models;
using Playlistofy.Data.Abstract;

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
    }
}
