using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Playlistofy.Models
{
    public class CreateModel : PageModel
    {
        private readonly Playlistofy.Models.SpotifyDBContext _context;

        public CreateModel(Playlistofy.Models.SpotifyDBContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["UserId"] = new SelectList(_context.Pusers, "Id", "Id");
            return Page();
        }

        [BindProperty]
        public Playlist Playlist { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Playlists.Add(Playlist);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
