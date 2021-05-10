using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SpotifyAPI.Web;
using Playlistofy.Models;
using Microsoft.AspNetCore.Http;
using Playlistofy.Controllers;
using System.Linq;

namespace Playlistofy.Utils
{
    public class searchSpotify
    {
        private readonly UserManager<IdentityUser> _userManager;
        private static string _spotifyClientId;
        private static string _spotifyClientSecret;

        public searchSpotify(UserManager<IdentityUser> userManager, string spotifyClientId, string spotifyClientPassword)
        {
            _userManager = userManager;
            _spotifyClientId = spotifyClientId;
            _spotifyClientSecret = spotifyClientPassword;
        }

        public SpotifyClient makeSpotifyClient(string spotifyClientId, string spotifyClientSecret)
        {
            SpotifyClientConfig config = SpotifyClientConfig
                .CreateDefault()
                .WithAuthenticator(new ClientCredentialsAuthenticator(spotifyClientId, spotifyClientSecret));

            SpotifyClient spotify = new SpotifyClient(config);

            return spotify;
        }

        public async Task<List<Track>> SearchTracks(SpotifyClient _spotifyClient, string SearchKeyword, List<Track> tracks)
        {
            var searchTracks = await _spotifyClient.Search.Item(new SearchRequest(
              SearchRequest.Types.Track, SearchKeyword
            ));

            List<Track> TrackList = new List<Track>();

            int count = 0;
            await foreach (var item in _spotifyClient.Paginate(searchTracks.Tracks, (s) => s.Tracks))
            {
                var _track = tracks.FirstOrDefault(i => i.Id == item.Id);
                if (_track == null)
                {
                    if (count >= 10)
                    {
                        break;
                    }
                    var tempTrack = new Track();
                    tempTrack.Name = item.Name;
                    tempTrack.Id = item.Id;
                    tempTrack.IsPlayable = item.IsPlayable;
                    tempTrack.IsLocal = item.IsLocal;
                    tempTrack.Popularity = item.Popularity;
                    tempTrack.Uri = item.Uri;
                    tempTrack.DurationMs = item.DurationMs;
                    foreach (var artists in item.Artists)
                    {
                        tempTrack.Href = tempTrack.Href + " " + artists.Name;
                    }

                    TrackList.Add(tempTrack);
                    ++count;
                }
            }

            return TrackList;
        }
    }
}
