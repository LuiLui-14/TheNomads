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

        public Playlist PlaylistsDB { get; set; }
        public IEnumerable<Playlist> _PlaylistsDB { get; set; }
        public List<Track> TracksDb { get; set; }
        public string PlaylistId { get; set; }
    }
}