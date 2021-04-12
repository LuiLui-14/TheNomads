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
    public class PlaylistsController : Controller
    {
        private readonly SpotifyDBContext _context;

        public PlaylistsController(SpotifyDBContext context)
        {
            _context = context;
        }

        // GET: Playlists
        public async Task<IActionResult> Index()
        {
            var spotifyDBContext = _context.Playlists.Include(p => p.User);
            return View(await spotifyDBContext.ToListAsync());
        }

        // GET: Playlists/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            
            var playlist = await _context.Playlists
                .Include(p => p.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (playlist == null)
            {
                return NotFound();
            }
            var Tracks = from track in _context.Tracks
                             join PlaylistTrackMap in _context.PlaylistTrackMaps on track.Id equals PlaylistTrackMap.TrackId
                             where (PlaylistTrackMap.PlaylistId == playlist.Id)
                             select track;
            foreach(Track t in Tracks)
            {
                t.Duration = ConvertMsToMinSec(t.DurationMs);
            }
            var TracksForPlaylistModel = new TracksForPlaylist
            {
                Playlist = playlist,
                Tracks = Tracks.ToList()
            };
            return View(TracksForPlaylistModel);
        }

        // GET: Playlists/Create
        public IActionResult Create()
        {
            ViewData["UserId"] = new SelectList(_context.Pusers, "Id", "Id");
            return View();
        }

        // POST: Playlists/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UserId,Description,Href,Name,Public,Collaborative,Uri")] Playlist playlist)
        {
            if (ModelState.IsValid)
            {
                _context.Add(playlist);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserId"] = new SelectList(_context.Pusers, "Id", "Id", playlist.UserId);
            return View(playlist);
        }

        // GET: Playlists/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var playlist = await _context.Playlists.FindAsync(id);
            if (playlist == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(_context.Pusers, "Id", "Id", playlist.UserId);
            return View(playlist);
        }

        // POST: Playlists/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,UserId,Description,Href,Name,Public,Collaborative,Uri")] Playlist playlist)
        {
            if (id != playlist.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(playlist);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PlaylistExists(playlist.Id))
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
            ViewData["UserId"] = new SelectList(_context.Pusers, "Id", "Id", playlist.UserId);
            return View(playlist);
        }

        // GET: Playlists/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var playlist = await _context.Playlists
                .Include(p => p.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (playlist == null)
            {
                return NotFound();
            }

            return View(playlist);
        }

        // POST: Playlists/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var playlist = await _context.Playlists.FindAsync(id);
            _context.Playlists.Remove(playlist);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PlaylistExists(string id)
        {
            return _context.Playlists.Any(e => e.Id == id);
        }

        [NonAction]
        public static string ConvertMsToMinSec(double timeInMs)
        {
            string str;
            if (timeInMs < 0)
            {
                str = "00:00";
            }
            else
            {
                try
                {
                    TimeSpan timeSpan = TimeSpan.FromMilliseconds(timeInMs);
                    str = timeSpan.ToString(@"mm\:ss");
                }
                catch (OverflowException)
                {
                    str = "Track Length Too Long";
                }
                catch (ArgumentException)
                {
                    str = "Length Not in Correct Format";
                }
            }
            return str;
        }
    }
}
