using Playlistofy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Playlistofy.Data.Abstract
{
    public interface IArtistRepository : IRepository<Artist>
    {
        Task AddArtistTrackMap(string artistId, string trackId);
        Task AddArtistAlbumMap(string artistId, string albumId);
    }
}
