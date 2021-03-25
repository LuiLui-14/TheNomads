using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
//using SpotifyAPI.Web;


#nullable disable

namespace Playlistofy.Models
{
    [Table("Track")]
    public partial class Track
    {
        public string Id { get; set; } = default!;
        public string PlaylistId { get; set; }
        public int DiscNumber { get; set; }
        public int DurationMs { get; set; }
        public bool Explicit { get; set; }
        public string Href { get; set; } = default!;
        public bool IsPlayable { get; set; }
        public string Name { get; set; } = default!;
        public int Popularity { get; set; }
        public string PreviewUrl { get; set; } = default!;
        public int TrackNumber { get; set; }
        public string Uri { get; set; } = default!;
        public bool IsLocal { get; set; }
    }
}