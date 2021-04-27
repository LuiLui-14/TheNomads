using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SpotifyAPI.Web;

namespace Playlistofy.Models.ViewModel
{
    public class userPlaylistsTracks
    {
        public List<Playlist> Playlists { get; set; }
        public List<Track> Tracks { get; set; }
        public PUser User { get; set; }

        public IEnumerable<Playlist> PlaylistsDB { get; set; }
        public IEnumerable<Track> TracksDb { get; set; }
        public string PlaylistId { get; set; }
    }
}