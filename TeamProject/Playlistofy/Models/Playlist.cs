using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

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

        [ForeignKey(nameof(UserId))]
        [InverseProperty(nameof(PUser.Playlists))]
        public virtual PUser User { get; set; }
        [InverseProperty(nameof(Track.Playlist))]
        public virtual ICollection<Track> Tracks { get; set; }
    }
}
