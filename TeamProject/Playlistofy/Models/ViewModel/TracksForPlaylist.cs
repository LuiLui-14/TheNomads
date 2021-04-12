using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Playlistofy.Models.ViewModel
{
    public class TracksForPlaylist
    {
        public Playlist Playlist { get; set; }
        public List<Track> Tracks { get; set; }
    }
}
