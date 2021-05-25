using Microsoft.EntityFrameworkCore;
using Playlistofy.Data.Abstract;
using Playlistofy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Castle.Core.Internal;

namespace Playlistofy.Data.Concrete
{
    public class PlaylistRepository : Repository<Playlist>, IPlaylistRepository
    {
        private DbSet<PlaylistTrackMap> trackMaps;

        public PlaylistRepository(SpotifyDbContext ctx) : base(ctx)
        {
            trackMaps = _context.Set<PlaylistTrackMap>();
        }

        public List<Playlist> FindPlaylistsBySearch(string searchQuery)
        {
            var t = _dbSet.Where(a => a.Name.Contains(searchQuery)).ToList();
            return t;
        }

        public IQueryable<Playlist> GetAllWithUser()
        {
            var set = _context.Set<Playlist>().Include("User");
            return set;
        }

        public List<Track> GetAllPlaylistTracks(Playlist playlist)
        {
            Playlist pl = _dbSet.Include("PlaylistTrackMaps").FirstOrDefault(i => (playlist == null) || i.Id == playlist.Id);
            List<Track> tracks = new List<Track>();
            foreach (var i in pl.PlaylistTrackMaps)
            {
                tracks.Add(_context.Set<Track>().Include("TrackAlbumMaps").Where(j => j.Id == i.TrackId).FirstOrDefault());
            }
            return tracks;
        }

        public List<PlaylistTrackMap> GetPlaylistTrackMaps(string Id)
        {
            List<Playlist> playlists = _dbSet.Include("PlaylistTrackMaps").ToList();
            Playlist pl = _dbSet.Find(Id);
            List<PlaylistTrackMap> maps = pl.PlaylistTrackMaps.ToList();
            return maps;
        }

        public virtual async Task DeleteTrackMapAsync(PlaylistTrackMap trackMap)
        {
            if (trackMap == null)
            {
                throw new Exception("Entity to delete was null");
            }
            else
            {
                
                trackMaps.Remove(trackMap);
                await _context.SaveChangesAsync();
            }
            return;
        }

        public PlaylistTrackMap GetPlaylistTrackMap(string tId, string pId)
        {
            var map = trackMaps.Where(i => i.TrackId == tId && i.PlaylistId == pId).FirstOrDefault();
            return map;
        }

        public async Task<List<Playlist>> GetMostRecentPlaylists_5Async()
        {
            var playlists = new List<Playlist>();

            var DBplaylist = await _dbSet.Include("PlaylistTrackMaps").ToListAsync();
            var list = DBplaylist.OrderBy(t => t.DateCreated);
            var countPlaylist = list.Count();

            for (int i = 0; i < 5; i++)
            {
                --countPlaylist;
                var playlist = list.ElementAtOrDefault(countPlaylist);
                playlists.Add(playlist);
            }

            return playlists;
        }

        public Playlist GetPlaylistWithAllMaps(string id)
        {
            Playlist playlist = _dbSet.Include("User").Include("PlaylistTrackMaps").Include("PlaylistKeywordMaps").Include("PlaylistHashtagMaps").Include("FollowedPlaylists").Where(i => i.Id == id).FirstOrDefault();
            return playlist;
        }

        public async Task RemoveFollowedPlaylist(int Id)
        {
            FollowedPlaylist follow = _context.Set<FollowedPlaylist>().Find(Id);
            _context.Remove<FollowedPlaylist>(follow);
            await _context.SaveChangesAsync();
        }

        public async Task AddTrackPlaylistMap(string pUId, string pId)
        {
            _context.Add(new FollowedPlaylist() 
            {
                PUserId = pUId,
                PlaylistId = pId
            });
            await _context.SaveChangesAsync();
        }

        public virtual async Task DeletePlaylistMap(string Uid, string pId)
        {
            PUser p = _context.Set<PUser>().Where(i => i.Id == Uid).FirstOrDefault();
            FollowedPlaylist followed = _context.Set<FollowedPlaylist>().Where(i => i.PlaylistId == pId && i.PUserId == p.Id).FirstOrDefault();
            _context.Remove<FollowedPlaylist>(followed);
            await _context.SaveChangesAsync();
        }

        public List<Playlist> GetAllForUser(string id)
        {
            List<Playlist> playlists = _dbSet.Include("User").Include("PlaylistTrackMaps").Include("PlaylistKeywordMaps").Include("PlaylistHashtagMaps").Include("FollowedPlaylists").Where(i => i.UserId == id).ToList();
            return playlists;
        }

        public List<Playlist> GetAllPublicForUser(string id)
        {
            List<Playlist> playlists = _dbSet.Include("User").Include("PlaylistTrackMaps").Include("PlaylistKeywordMaps").Include("PlaylistHashtagMaps").Include("FollowedPlaylists").Where(i => i.UserId == id).Where(i => i.Public == true).ToList();
            return playlists;
        }


        public async Task<List<Playlist>> GetAllFollowedPlaylists(string id)
        {
            List<FollowedPlaylist> allPlaylists = _context.Set<FollowedPlaylist>().Where(i => i.PUserId == id).ToList();
            List<Playlist> playlists = new List<Playlist>();
            foreach(var i in allPlaylists)
            {
                playlists.Add(await FindByIdAsync(i.PlaylistId));
            }
            return playlists;
        }

        public async Task<List<Playlist>> GetAllLikedPlaylists(string id)
        {
            List<LikedPlaylist> allPlaylists = _context.Set<LikedPlaylist>().Where(i => i.PUserId == id).ToList();
            List<Playlist> playlists = new List<Playlist>();
            foreach (var i in allPlaylists)
            {
                playlists.Add(await FindByIdAsync(i.PlaylistId));
            }
            return playlists;
        }

        public async Task LikePlaylist(string DispName, string pId)
        {
            PUser p = _context.Set<PUser>().Where(i => i.DisplayName == DispName).FirstOrDefault();
            _context.Add<LikedPlaylist>(new LikedPlaylist()
            {
                PUserId = p.Id,
                PlaylistId = pId
            });
            await _context.SaveChangesAsync();
        }

        public async Task UnlikePlaylist(string DispName, string pId)
        {
            PUser p = _context.Set<PUser>().Where(i => i.DisplayName == DispName).FirstOrDefault();
            LikedPlaylist liked = _context.Set<LikedPlaylist>().Where(i => i.PlaylistId == pId && i.PUserId == p.Id).FirstOrDefault();
            _context.Remove<LikedPlaylist>(liked);
            await _context.SaveChangesAsync();
        }
    }
}
