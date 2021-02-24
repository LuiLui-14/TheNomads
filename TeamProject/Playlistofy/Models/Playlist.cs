using System;
using System.Collections.Generic;
using SpotifyAPI.Web;

namespace Playlistofy.Models
{
    public class Playlist
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public string? Description { get; set; }
        public string Href { get; set; }
        public string? Name { get; set; }
        public bool? Public { get; set; }
        public bool? Collaborative { get; set; }
    }
}
