using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Playlistofy.Models.ViewModel.SearchViewModels
{
    public class TrackSearchViewModel
    {
        public IEnumerable<Track> tracks { get; set; }
        public string searchTerm { get; set; }
    }
}
