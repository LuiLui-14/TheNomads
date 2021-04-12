using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Humanizer;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Playlistofy.Models
{
    [Table("Track")]
    public partial class Track
    {
        [Key]
        public string Id { get; set; }
        //[Required]
        //[StringLength(450)]
        //public string PlaylistId { get; set; }
        public int? DiscNumber { get; set; }
        public int DurationMs { get; set; }
        public bool Explicit { get; set; }
        [StringLength(450)]
        public string Href { get; set; }
        public bool IsPlayable { get; set; }
        [StringLength(450)]
        public string Name { get; set; }
        public int? Popularity { get; set; }
        [StringLength(450)]
        public string PreviewUrl { get; set; }
        public int TrackNumber { get; set; }
        [StringLength(450)]
        public string Uri { get; set; }
        public bool IsLocal { get; set; }
<<<<<<< HEAD
        

        public string DurationSFunction()
        {
            var dur = TimeSpan.FromMilliseconds(DurationMs).Humanize(2);

            return dur;
        }
=======
        public string Duration { get; set; }
>>>>>>> a0a35d00f8d30431e1c2d3f7674d360f4e0c9d83

        //[ForeignKey(nameof(PlaylistId))]
        //[InverseProperty("Tracks")]
        //public virtual Playlist Playlist { get; set; }
        public virtual ICollection<PlaylistTrackMap> PlaylistTrackMaps { get; set; }
    }
}
