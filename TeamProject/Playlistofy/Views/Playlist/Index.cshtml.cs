using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Playlistofy.Models
{
    public class IndexModel : PageModel
    {
        private readonly Playlistofy.Models.SpotifyDBContext _context;

        public IndexModel(Playlistofy.Models.SpotifyDBContext context)
        {
            _context = context;
        }

        public IList<Playlist> Playlist { get;set; }

        public async Task OnGetAsync()
        {
            Playlist = await _context.Playlists
                .Include(p => p.User).ToListAsync();
        }
    }
}
