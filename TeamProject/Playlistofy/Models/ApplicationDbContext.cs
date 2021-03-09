using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Playlistofy.Models;

#nullable disable

namespace Playlistofy.Models
{
    public partial class ApplicationDbContext : IdentityDbContext<User>
    {
        private DbSet<User> users;

        public ApplicationDbContext()
        {
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<User> GetUsers()
        {
            return users;
        }

        public virtual void SetUsers(DbSet<User> value)
        {
            users = value;
        }

        public virtual DbSet<Playlist> Playlist { get; set; }
        public virtual DbSet<Track> Tracks { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Name=DefaultConnection");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");
            modelBuilder.Entity<Playlist>(entity =>
            {
                entity.ToTable("Playlist");

                entity.HasIndex(e => e.UserId, "IX_Playlist_UserId");

                entity.Property(e => e.Description).HasMaxLength(450);

                entity.Property(e => e.Href).IsRequired();

                entity.Property(e => e.Name).HasMaxLength(450);

                entity.Property(e => e.trackCount).HasColumnName("trackCount");

                entity.Property(e => e.Uri).HasColumnName("URI");

                entity.Property(e => e.UserId).IsRequired();

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Playlists)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<Track>(entity =>
            {
                entity.ToTable("Track");

                entity.HasIndex(e => e.PlaylistId, "IX_Track_PlaylistId");

                entity.HasOne(d => d.Playlist)
                    .WithMany(p => p.Tracks)
                    .HasForeignKey(d => d.PlaylistId);
            });

            base.OnModelCreating(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);


    }
}
