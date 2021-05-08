using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Playlistofy.Models.ViewModel
{
    public class PlaylistsAndTracks
    {
        public List<Playlist> Playlists { get; set; }
        public List<Track> Tracks { get; set; }
    }
}