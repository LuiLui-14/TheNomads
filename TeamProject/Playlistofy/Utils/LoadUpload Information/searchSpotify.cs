using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SpotifyAPI.Web;
using Playlistofy.Models;
using Microsoft.AspNetCore.Http;
using Playlistofy.Controllers;

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

        public async Task<List<Track>> SearchTracks(SpotifyClient _spotifyClient, string SearchKeyword)
        {
            if(SearchKeyword == null || SearchKeyword == "")
            {
                return null;
            }
            var searchTracks = await _spotifyClient.Search.Item(new SearchRequest(
              SearchRequest.Types.Track, SearchKeyword
            ));

            //await foreach (var item in _spotifyClient.Paginate(searchTracks.Tracks, (s) => s.Tracks))
            //{
            //    Console.WriteLine(item.Name);
            //    // you can use "break" here!
            //}

            List<Track> TrackList = new List<Track>();

            int count = 0;
            await foreach (var item in _spotifyClient.Paginate(searchTracks.Tracks, (s) => s.Tracks))
            {
                if(count > 10)
                {
                    break;
                }
                var tempTrack = new Track();
                tempTrack.Name = item.Name;
                tempTrack.Duration = item.DurationMs.ToString();
                foreach (var artists in item.Artists)
                {
                    tempTrack.Href = tempTrack.Href + " " + artists.Name;
                }
                

                TrackList.Add(tempTrack);

                //Console.WriteLine(item.Name);
                // you can use "break" here!
                ++count;
            }

            return TrackList;
        }
    }
}
