using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Playlistofy.Models
{
    public partial class Artist
    {
        [Key]
        public string Id { get; set; }
        public string Name { get; set; }
        public int Popularity { get; set; }
        public string Uri { get; set; }

        public virtual ICollection<ArtistTrackMap> ArtistTrackMaps { get; set; }
    }
}