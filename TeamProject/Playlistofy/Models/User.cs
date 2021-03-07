using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Playlistofy.Models
{
    [Table("User")]
    public partial class User
    {
        public User()
        {
            Playlists = new HashSet<Playlist>();
        }

        [Key]
        public string Id { get; set; }
        [StringLength(256)]
        public string UserName { get; set; }
        [StringLength(256)]
        public string NormalizedUserName { get; set; }
        [StringLength(256)]
        public string Email { get; set; }
        [StringLength(256)]
        public string NormalizedEmail { get; set; }
        public bool EmailConfirmed { get; set; }
        public string PasswordHash { get; set; }
        public string SecurityStamp { get; set; }
        public string ConcurrencyStamp { get; set; }
        public string PhoneNumber { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
        public bool TwoFactorEnabled { get; set; }
        public DateTimeOffset? LockoutEnd { get; set; }
        public bool LockoutEnabled { get; set; }
        public int? AccessFailedCount { get; set; }

        //--------ADDED------------------------------
        public int Followers { get; set; }
        public string DisplayName { get; set; }
        public List<SpotifyAPI.Web.Image> Images { get; set; }
        public string ImageUrl { get; set; }
        public string SpotifyUserId { get; set; }
        //-------------------------------------------

        [InverseProperty(nameof(Playlist.User))]
        public virtual ICollection<Playlist> Playlists { get; set; }
    }
}
