using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using SpotifyAPI.Web;

#nullable disable

namespace Playlistofy.Models
{
    [Table("Playlist")]
    public partial class Playlist
    {
        public Playlist()
        {
            Tracks = new HashSet<Track>();
        }

        [Key]
        public string Id { get; set; }
        [Required]
        [StringLength(450)]
        public string UserId { get; set; }
        [StringLength(450)]
        public string Description { get; set; }
        [Required]
        public string Href { get; set; }
        [StringLength(450)]
        public string Name { get; set; }
        public bool? Public { get; set; }
        public bool? Collaborative { get; set; }
        [Column("URI")]
        public string Uri { get; set; }
        public int trackCount { get; set; }

        [ForeignKey(nameof(UserId))]
<<<<<<< HEAD
        [InverseProperty("Playlists")]
        public virtual User User { get; set; }

        //Still need to add to Database schema//
        public virtual List<Track> Tracks { get; set; }
=======
        [InverseProperty(nameof(PUser.Playlists))]
        public virtual PUser User { get; set; }
        [InverseProperty(nameof(Track.Playlist))]
        public virtual ICollection<Track> Tracks { get; set; }
>>>>>>> 8957ec8a5391f5ff66626eeb479bae5f4b033815
    }
}
