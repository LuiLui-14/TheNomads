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
        private readonly ITrackRepository _tRepo;
        private readonly IAlbumRepository _alRepo;
        private readonly IPlaylistRepository _pRepo;
        private readonly IConfiguration _config;

        private static string _spotifyClientId;
        private static string _spotifyClientSecret;

        public TracksController(IConfiguration config, UserManager<IdentityUser> userManager, ITrackRepository tRepo, IAlbumRepository alRepo, IPlaylistRepository pRepo)
        {
            _userManager = userManager;
            _tRepo = tRepo;
            _alRepo = alRepo;
            _pRepo = pRepo;

            _config = config;

            _spotifyClientId = config["Spotify:ClientId"];
            _spotifyClientSecret = config["Spotify:ClientSecret"];
        }

        // GET: Tracks
        public async Task<IActionResult> Index()
        {
            var spotifyDBContext = _tRepo.GetAll();
            return View(await spotifyDBContext.ToListAsync());
        }

        // GET: Tracks/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var track = _tRepo.GetAllWithTrackMap().Include("TrackAlbumMaps").Where(i => i.Id == id).FirstOrDefault();

            if (track == null)
            {
                return NotFound();
            }
            List<Album> albums = new List<Album>();
            foreach(var j in track.TrackAlbumMaps)
            { 
                Album a = await _alRepo.FindByIdAsync(j.AlbumId);
                albums.Add(a);
            }
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
                await _tRepo.AddAsync(track);
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

            var track = await _tRepo.FindByIdAsync(id);
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
                    await _tRepo.UpdateAsync(track);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await TrackExists(track.Id))
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

            var track = await _tRepo.FindByIdAsync(TrackId);
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
            await _tRepo.RemoveTrackPlaylistMap(TrackId, PlaylistId);
            var track = await _tRepo.FindByIdAsync(TrackId);
            await _tRepo.DeleteAsync(track);
            return RedirectToAction("SearchTracks", "Tracks", new { id = PlaylistId });
        }

        private async Task<bool> TrackExists(string id)
        {
            return await _tRepo.ExistsAsync(id);
        }

        private Task<IdentityUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);

        [HttpGet]
        public async Task<IActionResult> SearchTracks(string SearchKeyword, string id, string username)
        {
            var PlaylistTracks = new userPlaylistsTracks();

            
            PlaylistTracks.PlaylistsDB = await _pRepo.FindByIdAsync(id);
            PlaylistTracks.PlaylistId = id;
            ViewBag.SearchKeyword = SearchKeyword;

            //var Tracks = from track in _context.Tracks
            //             join PlaylistTrackMap in _context.PlaylistTrackMaps on track.Id equals PlaylistTrackMap.TrackId
            //             where (PlaylistTrackMap.PlaylistId == id)
            //             select track;

            var alltracks = _tRepo.GetAllWithTrackMap();
            List<PlaylistTrackMap> Tracks = new List<PlaylistTrackMap>();
            List<Track> tracks = new List<Track>();
            foreach(Track t in alltracks)
            {
                foreach (PlaylistTrackMap i in t.PlaylistTrackMaps)
                {
                    if(i.PlaylistId == id)
                    {
                        Tracks.Add(i);
                        tracks.Add(alltracks.Where(j => j.Id == i.TrackId).FirstOrDefault());
                    }    
                }
            }

            PlaylistTracks.TracksDb = tracks;

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
            var SearchTracks = await SearchSpotify.SearchTracks(_spotifyClient, SearchKeyword, alltracks);

            foreach(var track in SearchTracks)
            {
                if (await _tRepo.FindByIdAsync(track.Id) == null)
                {
                    await _tRepo.AddAsync(track);
                }
            }

            if (username != null && username.Length > 0)
            {
                var trackMap = _pRepo.GetPlaylistTrackMap(username, id);
                if (trackMap == null)
                {
                    await _tRepo.AddTrackPlaylistMap(username, id);
                }
            }

            foreach (var track in Tracks)
            {
                var _track = SearchTracks.Where(i => i.Id == track.TrackId).FirstOrDefault();
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