using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace Playlistofy.Models
{
    public partial class SpotifyDBContext : DbContext
    {
        public SpotifyDBContext()
        {
        }

        public SpotifyDBContext(DbContextOptions<SpotifyDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Playlist> Playlists { get; set; }
        public virtual DbSet<PUser> Pusers { get; set; }
        public virtual DbSet<Track> Tracks { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Name=LuisAzureDB");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Playlist>(entity =>
            {
                entity.Property(e => e.Collaborative).HasDefaultValueSql("((0))");

                entity.Property(e => e.Public).HasDefaultValueSql("((0))");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Playlists)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Playlist_FK_PUser");
            });

            modelBuilder.Entity<Track>(entity =>
            {
                entity.HasOne(d => d.Playlist)
                    .WithMany(p => p.Tracks)
                    .HasForeignKey(d => d.PlaylistId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("TRACK_FK_Playlist");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
