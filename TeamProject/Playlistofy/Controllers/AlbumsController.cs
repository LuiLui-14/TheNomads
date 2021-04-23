using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Playlistofy.Models;
using Playlistofy.Data.Concrete;
using Playlistofy.Data.Abstract;
using Playlistofy.Models.ViewModel;
using Microsoft.Extensions.Configuration;
using Playlistofy.Utils;

namespace Playlistofy.Controllers
{
    public class AlbumsController : Controller
    {
        //private readonly SpotifyDbContext _context;
        private readonly IConfiguration _config;
        private readonly IAlbumRepository _albumRepo;
        private static string _spotifyClientId;
        private static string _spotifyClientSecret;

        public AlbumsController(IConfiguration config, IAlbumRepository albumRepo)
        {
            _albumRepo = albumRepo;
            _spotifyClientId = config["Spotify:ClientId"];
            _spotifyClientSecret = config["Spotify:ClientSecret"];
        }

        // GET: Albums
        public async Task<IActionResult> Index()
        {
            return View(await _albumRepo.GetAll().ToListAsync());
        }

        // GET: Albums/Details/5
        public async Task<IActionResult> Details(string id)
        {
            var _spotifyClient = getCurrentUserPlaylists.makeSpotifyClient(_spotifyClientId, _spotifyClientSecret);
            if (id == null)
            {
                return NotFound();
            }

            var a = await _albumRepo.FindByIdAsync(id);
            List<Track> t = await _albumRepo.GetAllAlbumTracks(_spotifyClient, a);
            if (a == null)
            {
                return NotFound();
            }
            ArtistForAlbum viewModel = new ArtistForAlbum()
            {
                album = a,
                tracks = t
            };

            return View(viewModel);
        }

        // GET: Albums/Create
        /*public IActionResult Create()
        {
            return View();
        }*/

        // POST: Albums/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        /* No need for creating albums
         * [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,AlbumType,Label,Name,Popularity,ReleaseDate,ReleaseDatePrecision")] Album album)
        {
            if (ModelState.IsValid)
            {
                _context.Add(album);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(album);
        }*/

        // GET: Albums/Edit/5
        /* No need to edit album
         * public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var album = await _context.Albums.FindAsync(id);
            if (album == null)
            {
                return NotFound();
            }
            return View(album);
        }*/

        // POST: Albums/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        /*[HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,AlbumType,Label,Name,Popularity,ReleaseDate,ReleaseDatePrecision")] Album album)
        {
            if (id != album.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(album);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AlbumExists(album.Id))
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
            return View(album);
        }*/

        // GET: Albums/Delete/5
        /* At this point no need to delete albums
         * public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var album = await _context.Albums
                .FirstOrDefaultAsync(m => m.Id == id);
            if (album == null)
            {
                return NotFound();
            }

            return View(album);
        }

        // POST: Albums/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var album = await _context.Albums.FindAsync(id);
            _context.Albums.Remove(album);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AlbumExists(string id)
        {
            return _context.Albums.Any(e => e.Id == id);
        }*/
    }
}
