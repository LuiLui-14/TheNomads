using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Playlistofy.Data.Abstract;
using Playlistofy.Data.Concrete;
using Playlistofy.Models;
using Playlistofy.Utils;
using Playlistofy.Utils.LoadUpload_Information;

namespace Playlistofy.Utils
{
    public class UserData
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IConfiguration _config;
        private readonly IPlaylistofyUserRepository _pURepo;
        private readonly IPlaylistRepository _pRepo;
        private readonly ITrackRepository _tRepo;
        private readonly IAlbumRepository _aRepo;
        private static string _spotifyClientId;
        private static string _spotifyClientSecret;
        IdentityUser _usr;

        public UserData(IConfiguration config, UserManager<IdentityUser> userManager, IPlaylistofyUserRepository pURepo, IPlaylistRepository pRepo, ITrackRepository tRepo, IAlbumRepository aRepo, IdentityUser usr)
        {
            _userManager = userManager;
            _config = config;
            _pURepo = pURepo;
            _pRepo = pRepo;
            _tRepo = tRepo;
            _aRepo = aRepo;
            _spotifyClientId = config["Spotify:ClientId"];
            _spotifyClientSecret = config["Spotify:ClientSecret"];
            _usr = usr;
        }
        
        public async Task SetUserData()
        {
            var getUserPlaylists = new getCurrentUserPlaylists(_userManager, _spotifyClientId, _spotifyClientSecret);
            var getUserTracks = new getCurrentUserTracks(_userManager, _spotifyClientId, _spotifyClientSecret);
            var _spotifyClient = getCurrentUserPlaylists.makeSpotifyClient(_spotifyClientId, _spotifyClientSecret);
            string _userSpotifyId = await getUserPlaylists.GetCurrentUserId(_usr);
            List<Playlist> Playlists = await getUserPlaylists.GetCurrentUserPlaylists(_spotifyClient, _userSpotifyId, _usr.Id);
            if (!await _pURepo.ExistsAsync(_usr.Id))
            {
                await _pURepo.AddAsync(await getNewUser.GetANewUser(_spotifyClient, _userSpotifyId, _usr));
            }
            foreach (Playlist i in Playlists)
            {
                if (!await _pRepo.ExistsAsync(i.Id))
                {
                    List<Track> Tracks = await getUserTracks.GetPlaylistTrack(_spotifyClient, _userSpotifyId, i.Id);
                    await _pRepo.AddAsync(i);
                    foreach (Track j in Tracks)
                    {
                        if (!await _tRepo.ExistsAsync(j.Id))
                        {
                            await _tRepo.AddAsync(j);
                            await _tRepo.AddTrackPlaylistMap(j.Id, i.Id);
                        }
                        Album a = _aRepo.GetTrackAlbum(_spotifyClient, j.Id);
                        if (!await _aRepo.ExistsAsync(a.Id))
                        {
                            await _aRepo.AddAsync(a);
                            await _aRepo.AddAlbumTrackMap(a, j);
                        }
                    }
                }
            }
            
        }
    }
}
