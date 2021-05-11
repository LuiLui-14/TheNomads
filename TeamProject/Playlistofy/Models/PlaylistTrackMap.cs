using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Playlistofy.Models
{
    [Table("PlaylistTrackMap")]
    public partial class PlaylistTrackMap
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }
        [Required]
        [Column("PlaylistID")]
        [StringLength(450)]
        public string PlaylistId { get; set; }
        [Required]
        [Column("TrackID")]
        [StringLength(450)]
        public string TrackId { get; set; }

        [ForeignKey(nameof(PlaylistId))]
        [InverseProperty("PlaylistTrackMaps")]
        public virtual Playlist Playlist { get; set; }
        [ForeignKey(nameof(TrackId))]
        [InverseProperty("PlaylistTrackMaps")]
        public virtual Track Track { get; set; }
    }
}
