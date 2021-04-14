using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Playlistofy.Models;
using Playlistofy.Utils;

namespace Playlistofy.Utils
{
    public class UserData
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IConfiguration _config;
        private static SpotifyDBContext _context;
        private static string _spotifyClientId;
        private static string _spotifyClientSecret;
        IdentityUser _usr;

        public UserData(IConfiguration config, UserManager<IdentityUser> userManager, SpotifyDBContext context, IdentityUser usr)
        {
            _userManager = userManager;
            _config = config;
            _context = context;
            _spotifyClientId = config["Spotify:ClientId"];
            _spotifyClientSecret = config["Spotify:ClientSecret"];
            _usr = usr;
        }
        
        public async Task SetUserData()
        {
            var getUserPlaylists = new getCurrentUserPlaylists(_userManager, _spotifyClientId, _spotifyClientSecret);
            var getUserTracks = new getCurrentUserTracks(_userManager, _spotifyClientId, _spotifyClientSecret);
            var _spotifyClient = getUserPlaylists.makeSpotifyClient(_spotifyClientId, _spotifyClientSecret);
            string _userSpotifyId = await getUserPlaylists.GetCurrentUserId(_usr);
            List<Playlist> Playlists = await getUserPlaylists.GetCurrentUserPlaylists(_spotifyClient, _userSpotifyId, _usr.Id);
            if (_context.Pusers.Find(_usr.Id) == null)
            {
                _context.Pusers.Add(new PUser()
                {
                    Id = _usr.Id,
                    UserName = _usr.UserName,
                    NormalizedUserName = _usr.NormalizedUserName,
                    Email = _usr.Email,
                    NormalizedEmail = _usr.NormalizedEmail,
                    EmailConfirmed = _usr.EmailConfirmed,
                    PasswordHash = _usr.PasswordHash,
                    SecurityStamp = _usr.SecurityStamp,
                    ConcurrencyStamp = _usr.ConcurrencyStamp,
                    PhoneNumber = _usr.PhoneNumber,
                    PhoneNumberConfirmed = _usr.PhoneNumberConfirmed,
                    TwoFactorEnabled = _usr.TwoFactorEnabled,
                    LockoutEnd = _usr.LockoutEnd,
                    LockoutEnabled = _usr.LockoutEnabled,
                    AccessFailedCount = _usr.AccessFailedCount,
                    Followers = 0,
                    DisplayName = null,
                    ImageUrl = null,
                    SpotifyUserId = null,
                    Href = null
                });
            }
            foreach (Playlist i in Playlists)
            {
                if (_context.Playlists.Find(i.Id) == null)
                {
                    List<Track> Tracks = await getUserTracks.GetPlaylistTrack(_spotifyClient, _userSpotifyId, i.Id);
                    _context.Playlists.Add(i);
                    foreach (Track j in Tracks)
                    {
                        if (_context.Tracks.Find(j.Id) == null)
                        {
                            _context.Tracks.Add(j);
                            _context.PlaylistTrackMaps.Add(
                                new PlaylistTrackMap()
                                {
                                    PlaylistId = i.Id,
                                    TrackId = j.Id
                                }
                                );
                        }
                    }
                }

            }

            _context.SaveChanges();
        }
    }
}
