using Playlistofy.Data.Abstract;
using Playlistofy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Playlistofy.Data.Concrete
{
    public class PlaylistRepository : Repository<Playlist>, IPlaylistRepository
    {
        public PlaylistRepository(SpotifyDbContext ctx) : base(ctx)
        {

        }
    }
}
