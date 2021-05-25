using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace Playlistofy.Models.ViewModel
{
    public class UploadPlaylistTracks
    {
        public List<string> TracksIDs { get; set; }
        public List<Playlist> PersonalPlaylists { get; set; }
        public List<Playlist> SpotifyPlaylists { get; set; }
        public string Code { get; set; }
    }
}