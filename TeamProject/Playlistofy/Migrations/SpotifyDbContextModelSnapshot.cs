﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Playlistofy.Models;

namespace Playlistofy.Migrations
{
    [DbContext(typeof(SpotifyDbContext))]
    partial class SpotifyDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.5")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Playlistofy.Models.Album", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("AlbumType")
                        .HasMaxLength(450)
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Label")
                        .HasMaxLength(450)
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasMaxLength(450)
                        .HasColumnType("nvarchar(450)");

                    b.Property<int?>("Popularity")
                        .HasColumnType("int");

                    b.Property<string>("ReleaseDate")
                        .HasMaxLength(450)
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ReleaseDatePrecision")
                        .HasMaxLength(450)
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.ToTable("Album");
                });

            modelBuilder.Entity("Playlistofy.Models.AlbumArtistMap", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AlbumId")
                        .HasMaxLength(450)
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ArtistId")
                        .HasMaxLength(450)
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("AlbumId");

                    b.HasIndex("ArtistId");

                    b.ToTable("AlbumArtistMap");
                });

            modelBuilder.Entity("Playlistofy.Models.Artist", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(450)
                        .HasColumnType("nvarchar(450)");

                    b.Property<int?>("Popularity")
                        .HasColumnType("int");

                    b.Property<string>("Uri")
                        .HasMaxLength(450)
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.ToTable("Artist");
                });

            modelBuilder.Entity("Playlistofy.Models.ArtistTrackMap", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ArtistId")
                        .IsRequired()
                        .HasMaxLength(450)
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("TrackId")
                        .IsRequired()
                        .HasMaxLength(450)
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("ArtistId");

                    b.HasIndex("TrackId");

                    b.ToTable("ArtistTrackMap");
                });

            modelBuilder.Entity("Playlistofy.Models.Hashtag", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("HashTag1")
                        .IsRequired()
                        .HasMaxLength(450)
                        .HasColumnType("nvarchar(450)")
                        .HasColumnName("Hashtag");

                    b.HasKey("Id");

                    b.ToTable("Hashtag");
                });

            modelBuilder.Entity("Playlistofy.Models.Keyword", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Keyword1")
                        .IsRequired()
                        .HasMaxLength(450)
                        .HasColumnType("nvarchar(450)")
                        .HasColumnName("Keyword");

                    b.HasKey("Id");

                    b.ToTable("Keyword");
                });

            modelBuilder.Entity("Playlistofy.Models.PUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int?>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DisplayName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<int>("Followers")
                        .HasColumnType("int");

                    b.Property<string>("Href")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("ImageUrl")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SpotifyUserId")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.ToTable("PUser");
                });

            modelBuilder.Entity("Playlistofy.Models.Playlist", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<bool?>("Collaborative")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValueSql("((0))");

                    b.Property<string>("Description")
                        .HasMaxLength(450)
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Href")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(450)
                        .HasColumnType("nvarchar(450)");

                    b.Property<bool?>("Public")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValueSql("((0))");

                    b.Property<string>("Uri")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("URI");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasMaxLength(450)
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Playlist");
                });

            modelBuilder.Entity("Playlistofy.Models.PlaylistHashtagMap", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("HashtagId")
                        .HasColumnType("int");

                    b.Property<string>("PlaylistId")
                        .IsRequired()
                        .HasMaxLength(450)
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("HashtagId");

                    b.HasIndex("PlaylistId");

                    b.ToTable("PlaylistHashtagMap");
                });

            modelBuilder.Entity("Playlistofy.Models.PlaylistKeywordMap", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("KeywordId")
                        .HasColumnType("int");

                    b.Property<string>("PlaylistId")
                        .IsRequired()
                        .HasMaxLength(450)
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("KeywordId");

                    b.HasIndex("PlaylistId");

                    b.ToTable("PlaylistKeywordMap");
                });

            modelBuilder.Entity("Playlistofy.Models.PlaylistTrackMap", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("PlaylistId")
                        .IsRequired()
                        .HasMaxLength(450)
                        .HasColumnType("nvarchar(450)")
                        .HasColumnName("PlaylistID");

                    b.Property<string>("TrackId")
                        .IsRequired()
                        .HasMaxLength(450)
                        .HasColumnType("nvarchar(450)")
                        .HasColumnName("TrackID");

                    b.HasKey("Id");

                    b.HasIndex("PlaylistId");

                    b.HasIndex("TrackId");

                    b.ToTable("PlaylistTrackMap");
                });

            modelBuilder.Entity("Playlistofy.Models.Track", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int?>("DiscNumber")
                        .HasColumnType("int");

                    b.Property<string>("Duration")
                        .HasMaxLength(450)
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("DurationMs")
                        .HasColumnType("int");

                    b.Property<bool>("Explicit")
                        .HasColumnType("bit");

                    b.Property<string>("Href")
                        .HasMaxLength(450)
                        .HasColumnType("nvarchar(450)");

                    b.Property<bool>("IsLocal")
                        .HasColumnType("bit");

                    b.Property<bool>("IsPlayable")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .HasMaxLength(450)
                        .HasColumnType("nvarchar(450)");

                    b.Property<int?>("Popularity")
                        .HasColumnType("int");

                    b.Property<string>("PreviewUrl")
                        .HasMaxLength(450)
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("TrackNumber")
                        .HasColumnType("int");

                    b.Property<string>("Uri")
                        .HasMaxLength(450)
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.ToTable("Track");
                });

            modelBuilder.Entity("Playlistofy.Models.TrackAlbumMap", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AlbumId")
                        .HasMaxLength(450)
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("TrackId")
                        .HasMaxLength(450)
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("AlbumId");

                    b.HasIndex("TrackId");

                    b.ToTable("TrackAlbumMap");
                });

            modelBuilder.Entity("Playlistofy.Models.AlbumArtistMap", b =>
                {
                    b.HasOne("Playlistofy.Models.Album", "Album")
                        .WithMany("AlbumArtistMaps")
                        .HasForeignKey("AlbumId")
                        .HasConstraintName("AlbumArtistMap_FK_Album");

                    b.HasOne("Playlistofy.Models.Artist", "Artist")
                        .WithMany("AlbumArtistMaps")
                        .HasForeignKey("ArtistId")
                        .HasConstraintName("AlbumArtistMap_FK_Artist");

                    b.Navigation("Album");

                    b.Navigation("Artist");
                });

            modelBuilder.Entity("Playlistofy.Models.ArtistTrackMap", b =>
                {
                    b.HasOne("Playlistofy.Models.Artist", "Artist")
                        .WithMany("ArtistTrackMaps")
                        .HasForeignKey("ArtistId")
                        .HasConstraintName("ArtistTrackMap_FK_Artist")
                        .IsRequired();

                    b.HasOne("Playlistofy.Models.Track", "Track")
                        .WithMany("ArtistTrackMaps")
                        .HasForeignKey("TrackId")
                        .HasConstraintName("ArtistTrackMap_FK_Track")
                        .IsRequired();

                    b.Navigation("Artist");

                    b.Navigation("Track");
                });

            modelBuilder.Entity("Playlistofy.Models.Playlist", b =>
                {
                    b.HasOne("Playlistofy.Models.PUser", "User")
                        .WithMany("Playlists")
                        .HasForeignKey("UserId")
                        .HasConstraintName("Playlist_FK_PUser")
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Playlistofy.Models.PlaylistHashtagMap", b =>
                {
                    b.HasOne("Playlistofy.Models.Hashtag", "Hashtag")
                        .WithMany("PlaylistHashtagMaps")
                        .HasForeignKey("HashtagId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Playlistofy.Models.Playlist", "Playlist")
                        .WithMany("PlaylistHashtagMaps")
                        .HasForeignKey("PlaylistId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Hashtag");

                    b.Navigation("Playlist");
                });

            modelBuilder.Entity("Playlistofy.Models.PlaylistKeywordMap", b =>
                {
                    b.HasOne("Playlistofy.Models.Keyword", "Keyword")
                        .WithMany("PlaylistKeywordMaps")
                        .HasForeignKey("KeywordId")
                        .HasConstraintName("PlaylistKeyMap_FK_Keyword")
                        .IsRequired();

                    b.HasOne("Playlistofy.Models.Playlist", "Playlist")
                        .WithMany("PlaylistKeywordMaps")
                        .HasForeignKey("PlaylistId")
                        .HasConstraintName("PlaylistKeyMap_FK_Playlist")
                        .IsRequired();

                    b.Navigation("Keyword");

                    b.Navigation("Playlist");
                });

            modelBuilder.Entity("Playlistofy.Models.PlaylistTrackMap", b =>
                {
                    b.HasOne("Playlistofy.Models.Playlist", "Playlist")
                        .WithMany("PlaylistTrackMaps")
                        .HasForeignKey("PlaylistId")
                        .HasConstraintName("PlaylistTrackMap_FK_Playlist")
                        .IsRequired();

                    b.HasOne("Playlistofy.Models.Track", "Track")
                        .WithMany("PlaylistTrackMaps")
                        .HasForeignKey("TrackId")
                        .HasConstraintName("PlaylistTrackMap_FK_Track")
                        .IsRequired();

                    b.Navigation("Playlist");

                    b.Navigation("Track");
                });

            modelBuilder.Entity("Playlistofy.Models.TrackAlbumMap", b =>
                {
                    b.HasOne("Playlistofy.Models.Album", "Album")
                        .WithMany("TrackAlbumMaps")
                        .HasForeignKey("AlbumId")
                        .HasConstraintName("TrackAlbumMap_FK_Album");

                    b.HasOne("Playlistofy.Models.Track", "Track")
                        .WithMany("TrackAlbumMaps")
                        .HasForeignKey("TrackId")
                        .HasConstraintName("TrackAlbumMap_FK_Track");

                    b.Navigation("Album");

                    b.Navigation("Track");
                });

            modelBuilder.Entity("Playlistofy.Models.Album", b =>
                {
                    b.Navigation("AlbumArtistMaps");

                    b.Navigation("TrackAlbumMaps");
                });

            modelBuilder.Entity("Playlistofy.Models.Artist", b =>
                {
                    b.Navigation("AlbumArtistMaps");

                    b.Navigation("ArtistTrackMaps");
                });

            modelBuilder.Entity("Playlistofy.Models.Hashtag", b =>
                {
                    b.Navigation("PlaylistHashtagMaps");
                });

            modelBuilder.Entity("Playlistofy.Models.Keyword", b =>
                {
                    b.Navigation("PlaylistKeywordMaps");
                });

            modelBuilder.Entity("Playlistofy.Models.PUser", b =>
                {
                    b.Navigation("Playlists");
                });

            modelBuilder.Entity("Playlistofy.Models.Playlist", b =>
                {
                    b.Navigation("PlaylistHashtagMaps");

                    b.Navigation("PlaylistKeywordMaps");

                    b.Navigation("PlaylistTrackMaps");
                });

            modelBuilder.Entity("Playlistofy.Models.Track", b =>
                {
                    b.Navigation("ArtistTrackMaps");

                    b.Navigation("PlaylistTrackMaps");

                    b.Navigation("TrackAlbumMaps");
                });
#pragma warning restore 612, 618
        }
    }
}