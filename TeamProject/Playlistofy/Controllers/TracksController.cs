using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Playlistofy.Models;
using Playlistofy.Models.ViewModel;
using Playlistofy.Utils;
using Microsoft.AspNetCore.Http;
using Playlistofy.Data.Abstract;

namespace Playlistofy.Controllers
{
    public class TracksController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SpotifyDbContext _context;
        private readonly ITrackRepository _tRepo;
        private readonly IAlbumRepository _alRepo;
        private readonly IConfiguration _config;

        private static string _spotifyClientId;
        private static string _spotifyClientSecret;

        public TracksController(IConfiguration config, UserManager<IdentityUser> userManager, SpotifyDbContext context, ITrackRepository tRepo, IAlbumRepository alRepo)
        {
            _userManager = userManager;
            _context = context;
            _tRepo = tRepo;
            _alRepo = alRepo;
            _config = config;

            _spotifyClientId = config["Spotify:ClientId"];
            _spotifyClientSecret = config["Spotify:ClientSecret"];
        }

        // GET: Tracks
        public async Task<IActionResult> Index()
        {
            var spotifyDBContext = _context.Tracks;//.Include(t => t.Playlist);
            return View(await spotifyDBContext.ToListAsync());
        }

        // GET: Tracks/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var track = await _context.Tracks
                .Include(t => t.PlaylistTrackMaps)
                .FirstOrDefaultAsync(m => m.Id == id);
            var trackAlbum = _context.Tracks.Include(t => t.TrackAlbumMaps).Where(t => t.Id == id).ToList();
            if (track == null)
            {
                return NotFound();
            }
            
            var Tracks =
                from playlist in _context.Playlists
                join PlaylistTrackMap in _context.PlaylistTrackMaps on playlist.Id equals PlaylistTrackMap.PlaylistId
                where (PlaylistTrackMap.TrackId == track.Id)
                select track;
            List<Album> albums = new List<Album>();
            foreach (var i in trackAlbum)
            {
                foreach (var j in i.TrackAlbumMaps)
                {
                    albums.Add(await _alRepo.FindByIdAsync(j.AlbumId));
                }
            };
            var InfoForTracksModel = new InfoForTracks
            {
                Track = track,
                PlaylistTrackMaps = track.PlaylistTrackMaps,
                Albums = albums
            };
            return View(InfoForTracksModel);
        }

        // GET: Tracks/Create
        public IActionResult Create()
        {
            //ViewData["PlaylistId"] = new SelectList(_context.Playlists, "Id", "Id");
            return View();
        }

        // POST: Tracks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,DiscNumber,DurationMs,Explicit,Href,IsPlayable,Name,Popularity,PreviewUrl,TrackNumber,Uri,IsLocal")] Track track)
        {
            if (ModelState.IsValid)
            {
                _context.Add(track);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            //ViewData["PlaylistId"] = new SelectList(_context.Playlists, "Id", "Id", track.PlaylistId);
            return View(track);
        }

        // GET: Tracks/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var track = await _context.Tracks.FindAsync(id);
            if (track == null)
            {
                return NotFound();
            }
            //ViewData["PlaylistId"] = new SelectList(_context.Playlists, "Id", "Id", track.PlaylistId);
            return View(track);
        }

        // POST: Tracks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,DiscNumber,DurationMs,Explicit,Href,IsPlayable,Name,Popularity,PreviewUrl,TrackNumber,Uri,IsLocal")] Track track)
        {
            if (id != track.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(track);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TrackExists(track.Id))
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
            //ViewData["PlaylistId"] = new SelectList(_context.Playlists, "Id", "Id", track.PlaylistId);
            return View(track);
        }

        // GET: Tracks/Delete/5
        public async Task<IActionResult> Delete(string TrackId, string PlaylistId)
        {
            if (TrackId == null)
            {
                return NotFound();
            }

            var track = await _context.Tracks
                //.Include(t => t.Playlist)
                .FirstOrDefaultAsync(m => m.Id == TrackId);
            if (track == null)
            {
                return NotFound();
            }
            ViewBag.playlistId = PlaylistId;

            return View(track);
        }

        // POST: Tracks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string TrackId, string PlaylistId)
        {
            var trackMap = await _context.PlaylistTrackMaps.FirstOrDefaultAsync(i => i.TrackId == TrackId && i.PlaylistId == PlaylistId);
            _context.PlaylistTrackMaps.Remove(trackMap);

            var track = await _context.Tracks.FindAsync(TrackId);
            _context.Tracks.Remove(track);
            await _context.SaveChangesAsync();
            return RedirectToAction("SearchTracks", "Tracks", new { id = PlaylistId });
        }

        private bool TrackExists(string id)
        {
            return _context.Tracks.Any(e => e.Id == id);
        }

        private Task<IdentityUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);

        [HttpGet]
        public async Task<IActionResult> SearchTracks(string SearchKeyword, string id, string username)
        {
            var PlaylistTracks = new userPlaylistsTracks();

            var userPlaylists = _context.Playlists.Where(i => i.Id == id);
            PlaylistTracks.PlaylistsDB = await userPlaylists.ToListAsync();
            PlaylistTracks.PlaylistId = id;
            ViewBag.SearchKeyword = SearchKeyword;

            var Tracks = from track in _context.Tracks
                         join PlaylistTrackMap in _context.PlaylistTrackMaps on track.Id equals PlaylistTrackMap.TrackId
                         where (PlaylistTrackMap.PlaylistId == id)
                         select track;

            PlaylistTracks.TracksDb = Tracks;

            ViewBag.searchword = SearchKeyword;
            if (SearchKeyword == null || SearchKeyword == "")
            {
                return View(PlaylistTracks);
            }
            //Checks if a user is logged in before proceeding, else takes them to login page
            IdentityUser usr = await GetCurrentUserAsync();
            if (usr == null) { return RedirectToPage("/Account/Login", new { area = "Identity" }); }

            //Creates searchSpotify folder with necessary functions to use later
            var SearchSpotify = new searchSpotify(_userManager, _spotifyClientId, _spotifyClientSecret);
            //Creates spotify client
            var _spotifyClient = SearchSpotify.makeSpotifyClient(_spotifyClientId, _spotifyClientSecret);
            //Search and return a list of tracks
            var SearchTracks = await SearchSpotify.SearchTracks(_spotifyClient, SearchKeyword, Tracks);

            foreach(var track in SearchTracks)
            {
                if (_context.Tracks.Find(track.Id) == null)
                {
                    _context.Tracks.Add(track);
                }
            }

            if (username != null && username.Length > 0)
            {
                var trackMap = await _context.PlaylistTrackMaps.FirstOrDefaultAsync(i => i.TrackId == username && i.PlaylistId == id);
                if (trackMap == null)
                {
                    _context.PlaylistTrackMaps.Add(
                            new PlaylistTrackMap() {
                                PlaylistId = id,
                                TrackId = username
                            }
                        );
                    _context.SaveChanges();
                }
            }

            foreach (var track in Tracks)
            {
                var _track = SearchTracks.FirstOrDefault(i => i.Id == track.Id);
                if (_track != null)
                {
                    SearchTracks.Remove(_track);
                }
            }
            PlaylistTracks.Tracks = SearchTracks;

            return View(PlaylistTracks);
        }
    }
}