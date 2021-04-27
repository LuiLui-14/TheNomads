using SpotifyAPI.Web;
using System;

namespace Playlistofy.Controllers
{
    public class SpotifyToken : IToken
    {
        public SpotifyToken()
        {

        }
        public string AccessToken { get; set; }
        public string TokenType { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsExpired { get; set; } 
    }
}