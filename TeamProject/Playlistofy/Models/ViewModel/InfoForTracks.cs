using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Playlistofy.Models.ViewModel
{
    public class InfoForTracks
    {
        public Track Track { get; set; }
        public Playlist Playlist { get; set; }
        public ICollection<PlaylistTrackMap> PlaylistTrackMaps { get; internal set; }
    }
}
