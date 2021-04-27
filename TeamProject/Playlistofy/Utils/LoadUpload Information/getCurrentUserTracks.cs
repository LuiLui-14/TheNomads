using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SpotifyAPI.Web;
using Playlistofy.Controllers;
using Playlistofy.Models;

namespace Playlistofy.Utils
{
    public class getCurrentUserTracks
    {
        private readonly UserManager<IdentityUser> _userManager; 
        private static string _spotifyClientId;
        private static string _spotifyClientSecret;

        public getCurrentUserTracks(UserManager<IdentityUser> userManager, string spotifyClientId, string spotifyClientPassword)
        {
            _userManager = userManager;
            _spotifyClientId = spotifyClientId;
            _spotifyClientSecret = spotifyClientPassword;

        }

        [HttpGet]
        public async Task<string> GetCurrentUserId(IdentityUser _user)
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
                            Duration = AlgorithmicOperations.MsConversion.ConvertMsToMinSec(m.DurationMs),
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
                        }) ;
                    }
                }
            }
            return playlistTracks;
        }

        public Album GetTrackAlbum(SpotifyClient _spotifyClient, string TrackId)
        {
            FullAlbum fullAlbum = _spotifyClient.Albums.Get(_spotifyClient.Tracks.Get(TrackId).Result.Album.Id).Result;
            Album album = new Album()
            {
                AlbumType = fullAlbum.AlbumType,
                Id = fullAlbum.Id,
                Label = fullAlbum.Label,
                Name = fullAlbum.Name,
                Popularity = fullAlbum.Popularity,
                ReleaseDate = fullAlbum.ReleaseDate,
                ReleaseDatePrecision = fullAlbum.ReleaseDatePrecision
            };
            return album;
        }

        public async Task<List<Track>> GetAlbumTracks(SpotifyClient spotifyClient, string AlbumId)
        {
            List<Track> AlbumTracks = new List<Track>();
            Paging<SimpleTrack> AlbumTracksPage = await spotifyClient.Albums.GetTracks(AlbumId);
            foreach(var i in AlbumTracksPage.Items)
            {
                FullTrack m = await spotifyClient.Tracks.Get(i.Id);
                AlbumTracks.Add(new Track()
                {
                    DiscNumber = m.DiscNumber,
                    DurationMs = m.DurationMs,
                    Duration = AlgorithmicOperations.MsConversion.ConvertMsToMinSec(m.DurationMs),
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
            return AlbumTracks;
        }
        public List<Artist> GetTrackArtist(SpotifyClient _spotifyClient, string TrackId)
        {
            List<Artist> artists = new List<Artist>();
            List<FullArtist> fullArtists = new List<FullArtist>();
            List<SimpleArtist> simpleArtists = _spotifyClient.Tracks.Get(TrackId).Result.Artists;
            foreach (SimpleArtist a in simpleArtists)
            {
                fullArtists.Add(_spotifyClient.Artists.Get(a.Id).Result);
            }
            foreach (FullArtist a in fullArtists)
            {
                artists.Add(new Artist()
                {
                    Id = a.Id,
                    Name = a.Name,
                    Popularity = a.Popularity,
                    Uri = a.Uri
                    //Images
                });
            }

            return artists;
        }
    }
}