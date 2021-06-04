using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Playlistofy.Models.ViewModel.SearchViewModels
{
    public class AlbumSearchViewModel
    {
        public IEnumerable<Album> Albums { get; set; }
        public string SearchTerm { get; set; }
    }
}
