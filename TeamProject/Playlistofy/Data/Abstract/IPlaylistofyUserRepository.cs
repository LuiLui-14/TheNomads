using Playlistofy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Playlistofy.Data.Abstract
{
    public interface IPlaylistofyUserRepository: IRepository<PUser>
    {
        PUser GetPUserByID(string ID);
        bool Exists(PUser pu);

    }
}
