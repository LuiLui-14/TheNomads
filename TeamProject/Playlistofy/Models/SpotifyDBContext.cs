using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace Playlistofy.Models
{
    public partial class SpotifyDbContext : DbContext
    {
        public SpotifyDbContext()
        {
        }

        public SpotifyDbContext(DbContextOptions<SpotifyDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Album> Albums { get; set; }
        public virtual DbSet<Artist> Artists { get; set; }
        public virtual DbSet<ArtistAlbumMap> ArtistAlbumMaps { get; set; }
        public virtual DbSet<ArtistTrackMap> ArtistTrackMaps { get; set; }
        public virtual DbSet<Playlist> Playlists { get; set; }
        public virtual DbSet<PlaylistTrackMap> PlaylistTrackMaps { get; set; }
        public virtual DbSet<PUser> Pusers { get; set; }
        public virtual DbSet<Track> Tracks { get; set; }
        public virtual DbSet<TrackAlbumMap> TrackAlbumMaps { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Name=AzureSpotifyDB");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Album>(entity =>
            {
                entity.ToTable("Album");

                entity.Property(e => e.AlbumType).HasMaxLength(50);

                entity.Property(e => e.Label).HasMaxLength(450);

                entity.Property(e => e.Name).HasMaxLength(450);

                entity.Property(e => e.ReleaseDate).HasMaxLength(450);

                entity.Property(e => e.ReleaseDatePrecision).HasMaxLength(450);
            });

            modelBuilder.Entity<Artist>(entity =>
            {
                entity.ToTable("Artist");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.Property(e => e.Uri).HasMaxLength(450);
            });

            modelBuilder.Entity<ArtistAlbumMap>(entity =>
            {
                entity.ToTable("ArtistAlbumMap");

                entity.Property(e => e.AlbumId)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.Property(e => e.ArtistId)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.HasOne(d => d.Album)
                    .WithMany(p => p.ArtistAlbumMaps)
                    .HasForeignKey(d => d.AlbumId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("ArtistAlbumMap_FK_Track");

                entity.HasOne(d => d.Artist)
                    .WithMany(p => p.ArtistAlbumMaps)
                    .HasForeignKey(d => d.ArtistId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("ArtistAlbumMap_FK_Artist");
            });

            modelBuilder.Entity<ArtistTrackMap>(entity =>
            {
                entity.ToTable("ArtistTrackMap");

                entity.Property(e => e.ArtistId)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.Property(e => e.TrackId)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.HasOne(d => d.Artist)
                    .WithMany(p => p.ArtistTrackMaps)
                    .HasForeignKey(d => d.ArtistId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("ArtistTrackMap_FK_Artist");

                entity.HasOne(d => d.Track)
                    .WithMany(p => p.ArtistTrackMaps)
                    .HasForeignKey(d => d.TrackId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("ArtistTrackMap_FK_Track");
            });

            modelBuilder.Entity<Playlist>(entity =>
            {
                entity.ToTable("Playlist");

                entity.Property(e => e.Collaborative).HasDefaultValueSql("((0))");

                entity.Property(e => e.Description).HasMaxLength(450);

                entity.Property(e => e.Href).IsRequired();

                entity.Property(e => e.Name).HasMaxLength(450);

                entity.Property(e => e.Public).HasDefaultValueSql("((0))");

                entity.Property(e => e.Uri).HasColumnName("URI");

                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Playlists)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Playlist_FK_PUser");
            });

            modelBuilder.Entity<PlaylistTrackMap>(entity =>
            {
                entity.ToTable("PlaylistTrackMap");

                entity.Property(e => e.PlaylistId)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.Property(e => e.TrackId)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.HasOne(d => d.Playlist)
                    .WithMany(p => p.PlaylistTrackMaps)
                    .HasForeignKey(d => d.PlaylistId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("PlaylistTrackMap_FK_Playlist");

                entity.HasOne(d => d.Track)
                    .WithMany(p => p.PlaylistTrackMaps)
                    .HasForeignKey(d => d.TrackId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("PlaylistTrackMap_FK_Track");
            });

            modelBuilder.Entity<PUser>(entity =>
            {
                entity.ToTable("PUser");

                entity.Property(e => e.DisplayName).HasMaxLength(256);

                entity.Property(e => e.Email).HasMaxLength(256);

                entity.Property(e => e.Href).HasMaxLength(256);

                entity.Property(e => e.ImageUrl).HasMaxLength(256);

                entity.Property(e => e.NormalizedEmail).HasMaxLength(256);

                entity.Property(e => e.NormalizedUserName).HasMaxLength(256);

                entity.Property(e => e.SpotifyUserId).HasMaxLength(256);

                entity.Property(e => e.UserName).HasMaxLength(256);
            });

            modelBuilder.Entity<Track>(entity =>
            {
                entity.ToTable("Track");

                entity.Property(e => e.Duration).HasMaxLength(450);

                entity.Property(e => e.Href).HasMaxLength(450);

                entity.Property(e => e.Name).HasMaxLength(450);

                entity.Property(e => e.PreviewUrl).HasMaxLength(450);

                entity.Property(e => e.Uri).HasMaxLength(450);
            });

            modelBuilder.Entity<TrackAlbumMap>(entity =>
            {
                entity.ToTable("TrackAlbumMap");

                entity.Property(e => e.AlbumId)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.Property(e => e.TrackId)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.HasOne(d => d.Album)
                    .WithMany(p => p.TrackAlbumMaps)
                    .HasForeignKey(d => d.AlbumId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("TrackAlbumMap_FK_Album");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
