using System;
using System.Collections.Generic;

#nullable disable

namespace Playlistofy.Models
{
    public partial class PlaylistTrackMap
    {
        public int Id { get; set; }
        public string PlaylistId { get; set; }
        public string TrackId { get; set; }

        public virtual Playlist Playlist { get; set; }
        public virtual Track Track { get; set; }
    }
}
