using System;
using System.Collections.Generic;

#nullable disable

namespace Playlistofy.Models
{
    public partial class Artist
    {
        public Artist()
        {
            ArtistAlbumMaps = new HashSet<ArtistAlbumMap>();
            ArtistTrackMaps = new HashSet<ArtistTrackMap>();
        }

        public string Id { get; set; }
        public string Name { get; set; }
        public int? Popularity { get; set; }
        public string Uri { get; set; }

        public virtual ICollection<ArtistAlbumMap> ArtistAlbumMaps { get; set; }
        public virtual ICollection<ArtistTrackMap> ArtistTrackMaps { get; set; }
    }
}
