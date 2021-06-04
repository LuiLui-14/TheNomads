using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Playlistofy.Models.ViewModel.SearchViewModels
{
    public class PlaylistSearchViewModel
    {
        public IEnumerable<Playlist> Playlists { get; set; }
        public string SearchTerm { get; set; }
        public bool PlaylistOrTag { get; set; }
    }
}
