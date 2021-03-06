using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Playlistofy.Models
{
    [Table("Track")]
    public partial class Track
    {
        public int Duration { get; set; }
        public bool Explicit { get; set; }
        public int Popularity { get; set; }

        [Required]
        public string Href { get; set; }
        [StringLength(450)]
        public string Name { get; set; }
        [Column("URI")]
        public string Uri { get; set; }

    }
}
