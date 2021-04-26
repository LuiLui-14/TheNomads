using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Playlistofy.Models.ViewModel
{
    public class InfoForArtists
    {
        public Track Track { get; set; }
        public Artist Artist { get; set; }
        public ICollection<ArtistTrackMap> ArtistTrackMaps { get; internal set; }
    }
}