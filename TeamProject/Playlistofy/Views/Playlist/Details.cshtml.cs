using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Playlistofy.Models
{
    public class DetailsModel : PageModel
    {
        private readonly Playlistofy.Models.SpotifyDBContext _context;

        public DetailsModel(Playlistofy.Models.SpotifyDBContext context)
        {
            _context = context;
        }

        public Playlist Playlist { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Playlist = await _context.Playlists
                .Include(p => p.User).FirstOrDefaultAsync(m => m.Id == id);

            if (Playlist == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
