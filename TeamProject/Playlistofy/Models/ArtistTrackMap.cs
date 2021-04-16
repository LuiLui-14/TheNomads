using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Playlistofy.Models
{
    public class ArtistTrackMap
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }
        [Required]
        [StringLength(450)]
        public string ArtistId { get; set; }
        [Required]
        [StringLength(450)]
        public string TrackId { get; set; }

        [ForeignKey(nameof(ArtistId))]
        [InverseProperty("ArtistTrackMaps")]
        public virtual Artist Artist { get; set; }

        [ForeignKey(nameof(TrackId))]
        [InverseProperty("ArtistTrackMaps")]
        public virtual Track Track { get; set; }
    }
}