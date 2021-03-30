using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
<<<<<<< HEAD
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
//using SpotifyAPI.Web;

=======
>>>>>>> 8957ec8a5391f5ff66626eeb479bae5f4b033815

#nullable disable

namespace Playlistofy.Models
{
    [Table("Track")]
    public partial class Track
    {
<<<<<<< HEAD
        //public SimpleAlbum Album { get; set; } = default!;
        //public List<SimpleArtist> Artists { get; set; } = default!;
        //public List<string> AvailableMarkets { get; set; } = default!;
        public int DiscNumber { get; set; }
        public int DurationMs { get; set; }
        public bool Explicit { get; set; }
        //public Dictionary<string, string> ExternalIds { get; set; } = default!;
        //public Dictionary<string, string> ExternalUrls { get; set; } = default!;
        public string Href { get; set; } = default!;
        public string Id { get; set; } = default!;
        public bool IsPlayable { get; set; }
        //public LinkedTrack LinkedFrom { get; set; } = default!;
        //public Dictionary<string, string> Restrictions { get; set; } = default!;
        public string Name { get; set; } = default!;
        public int Popularity { get; set; }
        public string PreviewUrl { get; set; } = default!;
        public int TrackNumber { get; set; }

        //[JsonConverter(typeof(StringEnumConverter))]
        //public ItemType Type { get; set; }
        public string Uri { get; set; } = default!;
        public bool IsLocal { get; set; }

        public string PlaylistId { get; set; }

        public virtual Playlist Playlist { get; set; }

    }
}
=======
        [Key]
        public string Id { get; set; }
        [Required]
        [StringLength(450)]
        public string PlaylistId { get; set; }
        public int? DiscNumber { get; set; }
        public int DurationMs { get; set; }
        public bool Explicit { get; set; }
        [StringLength(450)]
        public string Href { get; set; }
        public bool IsPlayable { get; set; }
        [StringLength(450)]
        public string Name { get; set; }
        public int? Popularity { get; set; }
        [StringLength(450)]
        public string PreviewUrl { get; set; }
        public int TrackNumber { get; set; }
        [StringLength(450)]
        public string Uri { get; set; }
        public bool IsLocal { get; set; }

        [ForeignKey(nameof(PlaylistId))]
        [InverseProperty("Tracks")]
        public virtual Playlist Playlist { get; set; }
    }
}
>>>>>>> 8957ec8a5391f5ff66626eeb479bae5f4b033815
