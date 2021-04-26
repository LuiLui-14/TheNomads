using System;
using System.Collections.Generic;

#nullable disable

namespace Playlistofy.Models
{
<<<<<<< HEAD
    public partial class ArtistTrackMap
=======
public partial class ArtistTrackMap
>>>>>>> mcbride_artist_creation
    {
        public int Id { get; set; }
        public string ArtistId { get; set; }
        public string TrackId { get; set; }

        public virtual Artist Artist { get; set; }
        public virtual Track Track { get; set; }
    }
}
