using System;
using System.Collections.Generic;

#nullable disable

namespace Playlistofy.Models
{
    public partial class TrackAlbumMap
    {
        public int Id { get; set; }
        public string AlbumId { get; set; }
        public string TrackId { get; set; }

        public virtual Album Album { get; set; }
    }
}
