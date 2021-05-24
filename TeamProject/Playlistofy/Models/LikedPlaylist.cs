using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Playlistofy.Models
{
    [Table("LikedPlaylist")]
    public partial class LikedPlaylist
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(450)]
        public string PlaylistId { get; set; }
        [Required]
        [StringLength(450)]
        public string PUserId { get; set; }

        [ForeignKey(nameof(PlaylistId))]
        [InverseProperty("LikedPlaylists")]
        public virtual Playlist playlist { get; set; }
        [ForeignKey(nameof(PUserId))]
        [InverseProperty("LikedPlaylists")]
        public virtual PUser pUser { get; set; }
    }
}
