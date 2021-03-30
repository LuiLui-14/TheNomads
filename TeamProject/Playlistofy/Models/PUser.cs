<<<<<<< HEAD:TeamProject/Playlistofy/Models/User.cs
﻿using System;
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
=======
﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Playlistofy.Models
{
    [Table("PUser")]
    public partial class PUser
    {
        public PUser()
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
        public int Followers { get; set; }
        [StringLength(256)]
        public string DisplayName { get; set; }
        [StringLength(256)]
        public string ImageUrl { get; set; }
        [StringLength(256)]
        public string SpotifyUserId { get; set; }
        [StringLength(256)]
        public string Href { get; set; }

        [InverseProperty(nameof(Playlist.User))]
        public virtual ICollection<Playlist> Playlists { get; set; }
    }
}
>>>>>>> 8957ec8a5391f5ff66626eeb479bae5f4b033815:TeamProject/Playlistofy/Models/PUser.cs
