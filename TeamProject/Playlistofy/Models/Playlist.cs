using System;
using System.Collections.Generic;
using SpotifyAPI.Web;

namespace Playlistofy.Models
{
    public class Playlist
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public bool? Collaborative { get; set; }
        public bool? Public { get; set; }
        public string? Id { get; set; } = default!;
        public string? Href { get; set; } = default!;
        public string? SnapshotId { get; set; } = default!;
        public List<Image>? Images { get; set; } = default!;

        public Paging<PlaylistTrack<IPlayableItem>>? Tracks { get; set; } = default!;
        public string? Type { get; set; } = default!;
        public string? Uri { get; set; } = default!;
    }
}
