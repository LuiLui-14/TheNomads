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
using SpotifyAPI.Web;
using Playlistofy.Utils.LoadUpload_Information;

namespace Playlistofy.Controllers
{
    public class TracksController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ITrackRepository _tRepo;
        private readonly IAlbumRepository _alRepo;
        private readonly IPlaylistRepository _pRepo;
        private readonly IArtistRepository _aRepo;
        private readonly IConfiguration _config;

        private static string _spotifyClientId;
        private static string _spotifyClientSecret;

        public TracksController(IConfiguration config, UserManager<IdentityUser> userManager, ITrackRepository tRepo, IAlbumRepository alRepo, IPlaylistRepository pRepo, IArtistRepository aRepo)
        {
            _userManager = userManager;
            _tRepo = tRepo;
            _alRepo = alRepo;
            _pRepo = pRepo;
            _aRepo = aRepo;

            _config = config;

            _spotifyClientId = config["Spotify:ClientId"];
            _spotifyClientSecret = config["Spotify:ClientSecret"];
        }

        /// <summary>
        /// This is the index view method for Tracks. Should not be accessable to anyone for now
        /// </summary>
        /// <returns>A view of all tracks in the database</returns>
        [NonAction]
        public async Task<IActionResult> Index()
        {
            var spotifyDBContext = _tRepo.GetAll();
            return View(await spotifyDBContext.ToListAsync());
        }

        /// <summary>
        /// This is the Details view method for a specific track. Should be accessible to everyone.
        /// </summary>
        /// <param name="id">Track Id</param>
        /// <returns>A view of the details of the Track whos Id is inputted</returns>
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var track = _tRepo.GetAllWithTrackMap().Include("TrackAlbumMaps").FirstOrDefault(i => i.Id == id);

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


        /// <summary>
        /// The Create view method of a track. Should not be accessible to anyone
        /// </summary>
        /// <returns>A Create view for a Track</returns>
        [NonAction]
        public IActionResult Create()
        {
            //ViewData["PlaylistId"] = new SelectList(_context.Playlists, "Id", "Id");
            return View();
        }

        /// <summary>
        /// The action method of creating a track. Should not be accessible to anyone.
        /// </summary>
        /// <param name="track">The Track being created in the database</param>
        /// <returns>A creation view of the track being created</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [NonAction]
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

        /// <summary>
        /// The Edit view of a Track. Should not be accessible to anyone.
        /// </summary>
        /// <param name="id">The Track Id of the Track being editted</param>
        /// <returns>An Edit view for the track being editted</returns>
        [NonAction]
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

        /// <summary>
        /// The action method for editting a track. Should not be accessible to anyone.
        /// </summary>
        /// <param name="id">The Track Id of the track being editted</param>
        /// <param name="track">The Track being editted</param>
        /// <returns>An edit view of the track, or redirect to the index method</returns>
        [NonAction]
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
                    if (!await _tRepo.ExistsAsync(id))
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

        /// <summary>
        /// The delete view for a track. Should not be accessible to anyone but the spotify api
        /// </summary>
        /// <param name="TrackId">The Id of the Track being Deleted</param>
        /// <param name="PlaylistId">A Playlist Id</param>
        /// <returns></returns>
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

        
        /// <summary>
        /// The action method for deleting a Track. Should not be accessible to anyone but the spotify api.
        /// </summary>
        /// <param name="TrackId">The Id for the Track Being deleted</param>
        /// <param name="PlaylistId">The id of a Playlist</param>
        /// <returns></returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string TrackId, string PlaylistId)
        {
            //The following are all deletion of maps to avoid erros and exceptions becore deleteing a track
            //Removes Map for playlists and track
            await _tRepo.RemoveTrackPlaylistMap(TrackId, PlaylistId);

            //Removes Map for Artist and track
            var ArtistMap = _aRepo.GetArtistTrackMap(TrackId);
            await _aRepo.DeleteArtistTrackMapAsync(ArtistMap);

            //Removes Map for Album and track
            SpotifyClient spotty = getSpotifyClient.makeSpotifyClient(_spotifyClientId, _spotifyClientSecret);
            var AlbumMap = await _alRepo.GetAlbumTrackMap(TrackId, spotty);
            await _alRepo.DeleteAlbumTrackMapAsync(AlbumMap);

            var track = await _tRepo.FindByIdAsync(TrackId);
            await _tRepo.DeleteAsync(track);
            return RedirectToAction("SearchTracks", "Tracks", new { id = PlaylistId });
        }

        private Task<IdentityUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);

        [HttpGet]
        public async Task<IActionResult> SearchTracks(string SearchKeyword, string id, string username)
        {
            var PlaylistTracks = new userPlaylistsTracks();
            
            PlaylistTracks.PlaylistsDB = await _pRepo.FindByIdAsync(id);
            PlaylistTracks.PlaylistId = id;
            ViewBag.SearchKeyword = SearchKeyword;

            if (username != null && username.Length > 0)
            {
                var trackMap = _pRepo.GetPlaylistTrackMap(username, id);
                if (trackMap == null)
                {
                    await _tRepo.AddTrackPlaylistMap(username, id);
                }
            }

            var tracks = new List<Track>();
            var playlist = await _pRepo.FindByIdAsync(id);
            tracks = _pRepo.GetAllPlaylistTracks(playlist);

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
            var SearchTracks = await SearchSpotify.SearchTracks(_spotifyClient, SearchKeyword, tracks);

            foreach(var track in SearchTracks)
            {
                if (await _tRepo.FindByIdAsync(track.Id) == null)
                {
                    await _tRepo.AddAsync(track);
                }
            }

            foreach (var track in tracks)
            {
                var _track = SearchTracks.Where(i => i.Id == track.Id).FirstOrDefault();
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