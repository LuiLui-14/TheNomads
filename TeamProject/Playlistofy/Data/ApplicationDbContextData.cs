using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Playlistofy.Data
{
    public class ApplicationDbContextData : IdentityDbContext
    {
        public ApplicationDbContextData(DbContextOptions<ApplicationDbContextData> options)
            : base(options)
        {
        }
    }
}
