
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Playlistofy.Data.Abstract;
using Playlistofy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Playlistofy.Data.Concrete
{
    public class AlbumRepository :Repository<Album>, IAlbumRepository
    {
        public AlbumRepository(SpotifyDbContext ctx) : base(ctx)
        {

        }
    }
}
