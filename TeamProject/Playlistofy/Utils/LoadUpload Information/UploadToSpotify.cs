using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using SpotifyAPI.Web;
using SpotifyAPI.Web.Auth;

namespace Playlistofy.Utils
{
    public class UploadToSpotify
    {
        private readonly UserManager<IdentityUser> _userManager;
        private static string _spotifyClientId;
        private static string _spotifyClientSecret;

        public UploadToSpotify(UserManager<IdentityUser> userManager, string spotifyClientId, string spotifyClientPassword)
        {
            _userManager = userManager;
            _spotifyClientId = spotifyClientId;
            _spotifyClientSecret = spotifyClientPassword;
        }

        public async Task<SpotifyClient> makeSpotifyClientAsync(string spotifyClientId, string spotifyClientSecret, string code)
        {
            var response = await new OAuthClient().RequestToken(
                new AuthorizationCodeTokenRequest(
                    _spotifyClientId, _spotifyClientSecret,
                    code,
                    //new Uri("https://playlistofy.azurewebsites.net/Playlists/UploadPlaylistofyPlaylists/")
                    new Uri("https://localhost:5001/Playlists/UploadPlaylistofyPlaylists/")
                    ));

            var spotify = new SpotifyClient(response.AccessToken);
            // Also important for later: response.RefreshToken

            return spotify;
        }

        public async Task<string> UploadPlaylist(SpotifyClient client, string UserID, string PlaylistName, List<string> TracksIDs)
        {
            var PlalistCreate = new PlaylistCreateRequest(PlaylistName);
            var playlist = await client.Playlists.Create(UserID, PlalistCreate);

            if (TracksIDs != null)
            {
                var ListURIs = new List<string>();
                foreach (var TrackID in TracksIDs)
                {
                    ListURIs.Add("spotify:track:" + TrackID);
                }
                await client.Playlists.AddItems(playlist.Id, new PlaylistAddItemsRequest(ListURIs));
            }

            return playlist.Id;
        }
    }
}
