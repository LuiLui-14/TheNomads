using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Playlistofy.Models;
using Playlistofy.Models.ViewModel;
using Playlistofy.Utils;

namespace Playlistofy.Controllers
{
    public class PlaylistsController : Controller
    {
        private readonly SpotifyDBContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        private readonly IConfiguration _config;

        private static string _spotifyClientId;
        private static string _spotifyClientSecret;

        public PlaylistsController(SpotifyDBContext context, IConfiguration config, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;

            _config = config;

            _spotifyClientId = config["Spotify:ClientId"];
            _spotifyClientSecret = config["Spotify:ClientSecret"];
        }

        private Task<IdentityUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);

        // GET: Playlists
        public async Task<IActionResult> Index()
        {
            var spotifyDBContext = _context.Playlists.Include(p => p.User);
            return View(await spotifyDBContext.ToListAsync());
        }

        // GET: Playlists
        public async Task<IActionResult> UserPlaylists(string id)
        {
            var viewModel = new userPlaylistsTracks();

            //Finds current logged in user using identity 
            IdentityUser usr = await GetCurrentUserAsync();
            if (usr == null) { return View("~/Views/Home/Privacy.cshtml"); }

            //Instantiates the Model to call it's functions - Finds current logged in user's spotify ID
            var getUserPlaylists = new getCurrentUserPlaylists(_userManager, _spotifyClientId, _spotifyClientSecret);
            string _userSpotifyId = await getUserPlaylists.GetCurrentUserId(usr);
            if (_userSpotifyId == null || _userSpotifyId == "") { return View("~/Views/Home/Privacy.cshtml"); }

            //Create's client and then finds all playlists for current logged in user
            var _spotifyClient = getUserPlaylists.makeSpotifyClient(_spotifyClientId, _spotifyClientSecret);
            viewModel.Playlists = await getUserPlaylists.GetCurrentUserPlaylists(_spotifyClient, _userSpotifyId, usr.Id);

            //Get current logged in user's information
            var getUserInfo = new getCurrentUserInformation(_userManager, _spotifyClientId, _spotifyClientSecret);
            viewModel.User = await getUserInfo.GetCurrentUserInformation(_spotifyClient, _userSpotifyId);

            //Get current logged in user's tracks
            var getUserTracks = new getCurrentUserTracks(_userManager, _spotifyClientId, _spotifyClientSecret);

            if (_context.Playlists.Find(id) == null)
            {
                var newPlaylist = viewModel.Playlists.Where(name => name.Id == id);
                foreach (var playlist in newPlaylist)
                {
                    Console.WriteLine(playlist.Id);
                    
                    if (_context.Playlists.Find(playlist.Id) == null)
                    {
                        List<Track> Tracks = await getUserTracks.GetPlaylistTrack(_spotifyClient, _userSpotifyId, playlist.Id);
                        _context.Playlists.Add(playlist);
                        foreach (Track track in Tracks)
                        {
                            Console.WriteLine(track.Id);
                            if (_context.Tracks.Find(track.Id) == null)
                            {
                                _context.Tracks.Add(track);
                            }
                            _context.PlaylistTrackMaps.Add(
                                    new PlaylistTrackMap()
                                    {
                                        PlaylistId = playlist.Id,
                                        TrackId = track.Id
                                    }
                                );
                        }
                    }
                }
            }
            _context.SaveChanges();

            var currentUserID = await _userManager.GetUserIdAsync(usr);
            var userPlaylists = _context.Playlists.Include(p => p.User).Where(i => i.User.Id == currentUserID);
            
            viewModel.PlaylistsDB = await userPlaylists.ToListAsync();
            return View(viewModel);
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

            //Added to remove tracks too
            var listPlaylistTracks = _context.PlaylistTrackMaps.Where(i => i.PlaylistId == id);
            foreach(var map in listPlaylistTracks)
            {
                _context.PlaylistTrackMaps.Remove(map);
            }
            //----------------------------
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(UserPlaylists));
        }

        private bool PlaylistExists(string id)
        {
            return _context.Playlists.Any(e => e.Id == id);
        }
    }
}
