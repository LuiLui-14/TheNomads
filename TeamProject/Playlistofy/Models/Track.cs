using System;
using System.Collections.Generic;

#nullable disable

namespace Playlistofy.Models
{
    public partial class Track
    {
        public Track()
        {
            ArtistTrackMaps = new HashSet<ArtistTrackMap>();
            PlaylistTrackMaps = new HashSet<PlaylistTrackMap>();
        }

        public string Id { get; set; }
        public int? DiscNumber { get; set; }
        public int DurationMs { get; set; }
        public bool Explicit { get; set; }
        public string Href { get; set; }
        public bool IsPlayable { get; set; }
        public string Name { get; set; }
        public int? Popularity { get; set; }
        public string PreviewUrl { get; set; }
        public int TrackNumber { get; set; }
        public string Uri { get; set; }
        public bool IsLocal { get; set; }
        public string Duration { get; set; }

        public virtual ICollection<ArtistTrackMap> ArtistTrackMaps { get; set; }
        public virtual ICollection<PlaylistTrackMap> PlaylistTrackMaps { get; set; }
        public virtual ICollection<TrackAlbumMap> TrackAlbumMaps { get; set; }
    }
}
