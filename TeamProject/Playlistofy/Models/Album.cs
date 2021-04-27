using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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

        [Required]
        public string Id { get; set; }
        [RegularExpression(@"album|single|compilation")]
        public string AlbumType { get; set; }
        public string Label { get; set; }
        public string Name { get; set; }
        [Range(1,100)]
        public int? Popularity { get; set; }
        public string ReleaseDate { get; set; }
        public string ReleaseDatePrecision { get; set; }

        public virtual ICollection<ArtistAlbumMap> ArtistAlbumMaps { get; set; }
        public virtual ICollection<TrackAlbumMap> TrackAlbumMaps { get; set; }
    }
}
