using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Playlistofy.Data.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Playlistofy.Controllers
{
    public class SearchController : Controller
    {
        private readonly IPlaylistRepository _pRepo;
        private readonly ITrackRepository _tRepo;
        private readonly IAlbumRepository _aRepo;
        private readonly IArtistRepository _arRepo;

        public SearchController(IPlaylistRepository pRepo, ITrackRepository tRepo, IAlbumRepository aRepo, IArtistRepository arRepo)
        {
            _pRepo = pRepo;
            _tRepo = tRepo;
            _aRepo = aRepo;
            _arRepo = arRepo;
        }

        public ActionResult Search(string searchType, string searchQuery)
        {
            if(searchType == "Album")
            {
                return RedirectToAction("AlbumSearch", new { searchTerm = searchQuery });
            }else if(searchType == "Playlist")
            {
                return RedirectToAction("PlaylistSearch", new { searchTerm = searchQuery });
            }
            else if (searchType == "Track")
            {
                return RedirectToAction("TrackSearch", new { searchTerm = searchQuery });
            }
            else if (searchType == "Artist")
            {
                return RedirectToAction("ArtistSearch", new { searchTerm = searchQuery });
            }
            return View();
        }
        public ViewResult PlaylistSearch(string searchTerm)
        {
            var a = _pRepo.FindPlaylistsBySearch(searchTerm);
            return View(a);
        }

        public ViewResult TrackSearch(string searchTerm)
        {
            var a = _tRepo.FindTracksBySearch(searchTerm);
            return View(a);
        }

        public ViewResult ArtistSearch(string searchTerm)
        {
            var a = _arRepo.FindArtistsBySearch(searchTerm);
            return View(a);
        }

        public ViewResult AlbumSearch(string searchTerm)
        {
            var a = _aRepo.FindAlbumsBySearch(searchTerm);
            return View(a);
        }
    }
}
