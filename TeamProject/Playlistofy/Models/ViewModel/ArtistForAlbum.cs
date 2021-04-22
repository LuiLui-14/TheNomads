using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Playlistofy.Models.ViewModel
{
    public class ArtistForAlbum
    {
        public Album album { get; set; }
        public List<Artist> artists { get; set; }
    }
}
