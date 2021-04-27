﻿using Playlistofy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Playlistofy.Data.Abstract
{
    public interface ITrackRepository : IRepository<Track>
    {
        Task AddTrackPlaylistMap(string TrackId, string PlaylistId);
        public List<Track> FindTracksBySearch(string searchQuery);
    }
}
