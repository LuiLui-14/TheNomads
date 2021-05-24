using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Playlistofy.Data.Abstract;
using Playlistofy.Models;

namespace Playlistofy.Data.Concrete
{
    public class PlaylistofyUserRepository : Repository<PUser>, IPlaylistofyUserRepository
    {
        public PlaylistofyUserRepository(SpotifyDbContext ctx) : base(ctx)
        {

        }

        public virtual bool Exists(PUser pUser)
        {
            return _dbSet.Any(x => x.Id == pUser.Id);
        }

        public virtual PUser GetPUserByID(string ID)
        {
            return _dbSet.Include("LikedPlaylists").Include("FollowedPlaylists").Where(u => u.Id == ID).FirstOrDefault();
        }

        public PUser GetPUserByUsername(string userName)
        {
            return _dbSet.Where(u => u.DisplayName == userName).FirstOrDefault();
        }


    }
}
