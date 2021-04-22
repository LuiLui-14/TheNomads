using System;
using System.Collections.Generic;

#nullable disable

namespace Playlistofy.Models
{
    public partial class ArtistAlbumMap
    {
        public int Id { get; set; }
        public string ArtistId { get; set; }
        public string AlbumId { get; set; }

        public virtual Album Album { get; set; }
        public virtual Artist Artist { get; set; }
    }
}
