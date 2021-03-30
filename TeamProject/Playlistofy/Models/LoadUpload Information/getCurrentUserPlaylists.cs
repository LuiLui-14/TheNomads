using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SpotifyAPI.Web;
using Playlistofy.Models; 

namespace Playlistofy.Models
{
    public class getCurrentUserPlaylists
    {
        private readonly UserManager<User> _userManager;
        private static string _spotifyClientId;
        private static string _spotifyClientSecret;

        public getCurrentUserPlaylists(UserManager<User> userManager, string spotifyClientId, string spotifyClientPassword)
        {
            _userManager = userManager;
            _spotifyClientId = spotifyClientId;
            _spotifyClientSecret = spotifyClientPassword;
        }

        [HttpGet]
        public async Task<string> GetCurrentUserId(User _user)
        {
            var personalData = new Dictionary<string, string>();
            var logins = await _userManager.GetLoginsAsync(_user);

            string key = "";
            foreach (var l in logins)
            {
                personalData.Add($"{l.LoginProvider} external login provider key", l.ProviderKey);
                key = l.ProviderKey;
            }
            return key;
        }

        public SpotifyClient makeSpotifyClient(string spotifyClientId, string spotifyClientSecret)
        {
            SpotifyClientConfig config = SpotifyClientConfig
                .CreateDefault()
                .WithAuthenticator(new ClientCredentialsAuthenticator(spotifyClientId, spotifyClientSecret));

            SpotifyClient spotify = new SpotifyClient(config);

            return spotify;
        }

        public async Task<List<Playlist>> GetCurrentUserPlaylists(SpotifyClient spotifyClient, string userSpotifyId)
        {
            //This creates an instance of the model getCurrentUserTracks.cs to later call below in the foreach loop
            var playlistTracks = new getCurrentUserTracks(_userManager);

            List<Playlist> spotifyPlaylists = new List<Playlist>();
            var playlists = await spotifyClient.Playlists.GetUsers(userSpotifyId);
            FullPlaylist fullplaylist = null;
            foreach (var playlist in playlists.Items)
            {
                fullplaylist = await spotifyClient.Playlists.Get(playlist.Id);
                spotifyPlaylists.Add(new Playlist()
                {
<<<<<<< HEAD
                    Name = fullplaylist.Name,
                    Id = fullplaylist.Id,
                    Description = fullplaylist.Description,
                    Public = fullplaylist.Public,
                    Collaborative = fullplaylist.Collaborative,
                    Href = fullplaylist.Href,
                    Uri = fullplaylist.Uri,
                    Tracks = await GetPlaylistTrack(spotifyClient, userSpotifyId, fullplaylist.Id)
                });

=======
                    Name = playlist.Name,
                    Id = playlist.Id,
                    Description = playlist.Description,
                    Public = playlist.Public,
                    Collaborative = playlist.Collaborative,
                    Href = playlist.Href,
                    Uri = playlist.Uri,
                    UserId = userSpotifyId,
                    Tracks = await playlistTracks.GetPlaylistTrack(spotifyClient, userSpotifyId, playlist.Id)
                });
>>>>>>> 8957ec8a5391f5ff66626eeb479bae5f4b033815
            }
            return spotifyPlaylists;
        }

        public async Task<List<Track>> GetPlaylistTrack(SpotifyClient spotifyClient, string userSpotifyId, string playlistId)
        {
            List<Track> playlistTracks = new List<Track>();
            var playlists = await spotifyClient.Playlists.GetUsers(userSpotifyId);
            FullPlaylist fullplaylist = null;
            foreach (var playlist in playlists.Items)
            {
                if (playlist.Id == playlistId)
                {
                    fullplaylist = await spotifyClient.Playlists.Get(playlist.Id);
                    var j = fullplaylist.Tracks;
                    foreach (var k in j.Items)
                    {
                        FullTrack m = (FullTrack)k.Track;
                        playlistTracks.Add(new Track()
                        {
                            DiscNumber = m.DiscNumber,
                            DurationMs = m.DurationMs,
                            Explicit = m.Explicit,
                            Href = m.Href,
                            Id = m.Id,
                            IsPlayable = m.IsPlayable,
                            Name = m.Name,
                            Popularity = m.Popularity,
                            PreviewUrl = m.PreviewUrl,
                            TrackNumber = m.TrackNumber,
                            Uri = m.Uri,
                            IsLocal = m.IsLocal
                        });

                    }
                }
            }

            return playlistTracks;
        }
    }  
}




