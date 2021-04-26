using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Playlistofy.Models;
using Playlistofy.Models.ViewModel;

namespace Playlistofy.Controllers
{
    public class ArtistController : Controller
    {
        private readonly SpotifyDbContext _context;

        public ArtistController(SpotifyDbContext context)
        {
            _context = context;
        }

        // GET: Artists
        public async Task<IActionResult> Index()
        {
            var spotifyDBContext = _context.Artists;
            return View(await spotifyDBContext.ToListAsync());
        }

        // GET: Artists/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var artist = await _context.Artists
                .Include(t => t.ArtistTrackMaps)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (artist == null)
            {
                return NotFound();
            }
            var Artists =
                from track in _context.Tracks
                join ArtistTrackMap in _context.ArtistTrackMaps on track.Id equals ArtistTrackMap.TrackId
                where (ArtistTrackMap.ArtistId == artist.Id)
                select artist; 


            var InfoForArtistsModel = new InfoForArtists
            {
                Artist = artist,
                ArtistTrackMaps = artist.ArtistTrackMaps
            };
            return View(InfoForArtistsModel);
        }

        // GET: Artists/Create
        public IActionResult Create()
        {
            //ViewData["PlaylistId"] = new SelectList(_context.Playlists, "Id", "Id");
            return View();
        }

        // POST: Artists/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,DiscNumber,DurationMs,Explicit,Href,IsPlayable,Name,Popularity,PreviewUrl,ArtistNumber,Uri,IsLocal")] Artist Artist)
        {
            if (ModelState.IsValid)
            {
                _context.Add(Artist);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            //ViewData["PlaylistId"] = new SelectList(_context.Playlists, "Id", "Id", Artist.PlaylistId);
            return View(Artist);
        }

        // GET: Artists/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Artist = await _context.Artists.FindAsync(id);
            if (Artist == null)
            {
                return NotFound();
            }
            //ViewData["PlaylistId"] = new SelectList(_context.Playlists, "Id", "Id", Artist.PlaylistId);
            return View(Artist);
        }

        // POST: Artists/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,DiscNumber,DurationMs,Explicit,Href,IsPlayable,Name,Popularity,PreviewUrl,ArtistNumber,Uri,IsLocal")] Artist Artist)
        {
            if (id != Artist.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(Artist);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ArtistExists(Artist.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            //ViewData["PlaylistId"] = new SelectList(_context.Playlists, "Id", "Id", Artist.PlaylistId);
            return View(Artist);
        }

        // GET: Artists/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Artist = await _context.Artists
                //.Include(t => t.Playlist)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (Artist == null)
            {
                return NotFound();
            }

            return View(Artist);
        }

        // POST: Artists/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var Artist = await _context.Artists.FindAsync(id);
            _context.Artists.Remove(Artist);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ArtistExists(string id)
        {
            return _context.Artists.Any(e => e.Id == id);
        }
    }
}
