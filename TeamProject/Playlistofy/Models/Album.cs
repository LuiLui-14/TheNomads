using System;
using System.Collections.Generic;

#nullable disable

namespace Playlistofy.Models
{
    public partial class Album
    {
        public Album()
        {
            ArtistAlbumMaps = new HashSet<ArtistAlbumMap>();
            TrackAlbumMaps = new HashSet<TrackAlbumMap>();
        }

        public string Id { get; set; }
        public string AlbumType { get; set; }
        public string Label { get; set; }
        public string Name { get; set; }
        public int? Popularity { get; set; }
        public string ReleaseDate { get; set; }
        public string ReleaseDatePrecision { get; set; }

        public virtual ICollection<ArtistAlbumMap> ArtistAlbumMaps { get; set; }
        public virtual ICollection<TrackAlbumMap> TrackAlbumMaps { get; set; }
    }
}
