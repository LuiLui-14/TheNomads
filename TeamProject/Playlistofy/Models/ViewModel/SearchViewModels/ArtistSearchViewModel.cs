using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Playlistofy.Models.ViewModel.SearchViewModels
{
    public class ArtistSearchViewModel
    {
        public IEnumerable<Artist> Artists { get; set; }
        public string SearchTerm { get; set; }
    }
}
