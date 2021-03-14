using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Playlistofy.Models
{
    
    public class User : IdentityUser
    {
        public User()
        {
            Playlists = new HashSet<Playlist>();
        }

        //--------ADDED------------------------------
        public int Followers { get; set; }
        public string DisplayName { get; set; }
        //public List<SpotifyAPI.Web.Image> Images { get; set; }
        public string ImageUrl { get; set; }
        public string SpotifyUserId { get; set; }
        public string Href { get; set; }
        //-------------------------------------------

        [InverseProperty(nameof(Playlist.User))]
        public virtual ICollection<Playlist> Playlists { get; set; }
    }
}
