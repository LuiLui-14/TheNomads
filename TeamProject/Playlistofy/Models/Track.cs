﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using SpotifyAPI.Web;


#nullable disable

namespace Playlistofy.Models
{
    [Table("Track")]
    public partial class Track
    {
        public SimpleAlbum Album { get; set; } = default!;
        public List<SimpleArtist> Artists { get; set; } = default!;
        public List<string> AvailableMarkets { get; set; } = default!;
        public int DiscNumber { get; set; }
        public int DurationMs { get; set; }
        public bool Explicit { get; set; }
        public Dictionary<string, string> ExternalIds { get; set; } = default!;
        public Dictionary<string, string> ExternalUrls { get; set; } = default!;
        public string Href { get; set; } = default!;
        public string Id { get; set; } = default!;
        public bool IsPlayable { get; set; }
        public LinkedTrack LinkedFrom { get; set; } = default!;
        public Dictionary<string, string> Restrictions { get; set; } = default!;
        public string Name { get; set; } = default!;
        public int Popularity { get; set; }
        public string PreviewUrl { get; set; } = default!;
        public int TrackNumber { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public ItemType Type { get; set; }
        public string Uri { get; set; } = default!;
        public bool IsLocal { get; set; }

    }
}