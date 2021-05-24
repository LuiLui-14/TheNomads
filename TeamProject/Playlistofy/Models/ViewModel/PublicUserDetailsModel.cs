using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Playlistofy.Models.ViewModel
{
    public class PublicUserDetailsModel
    {
        public List<Playlist> CreatedPlaylists { get; set; }
        public List<Playlist> FollowedPlaylists { get; set; }
        public List<Playlist> LikedPlaylists { get; set; }
        public PUser Puser { get; set; }
    }
}
