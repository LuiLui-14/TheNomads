using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Playlistofy.Models
{
    [Table("TrackAlbumMap")]
    public partial class TrackAlbumMap
    {
        [Key]
        public int Id { get; set; }
        [StringLength(450)]
        public string TrackId { get; set; }
        [StringLength(450)]
        public string AlbumId { get; set; }

        [ForeignKey(nameof(AlbumId))]
        [InverseProperty("TrackAlbumMaps")]
        public virtual Album Album { get; set; }
        [ForeignKey(nameof(TrackId))]
        [InverseProperty("TrackAlbumMaps")]
        public virtual Track Track { get; set; }
    }
}
