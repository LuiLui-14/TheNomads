CREATE TABLE [PUser]
(
	[Id] NVARCHAR(450) NOT NULL, 
    [UserName] NVARCHAR(256) NULL, 
    [NormalizedUserName] NVARCHAR(256) NULL, 
    [Email] NVARCHAR(256) NULL, 
    [NormalizedEmail] NVARCHAR(256) NULL, 
    [EmailConfirmed] BIT NOT NULL DEFAULT 0, 
    [PasswordHash] NVARCHAR(MAX) NULL, 
    [SecurityStamp] NVARCHAR(MAX) NULL, 
    [ConcurrencyStamp] NVARCHAR(MAX) NULL, 
    [PhoneNumber] NVARCHAR(MAX) NULL, 
    [PhoneNumberConfirmed] BIT NOT NULL DEFAULT 0, 
    [TwoFactorEnabled] BIT NOT NULL DEFAULT 0, 
    [LockoutEnd] DATETIMEOFFSET NULL, 
    [LockoutEnabled] BIT NOT NULL DEFAULT 0, 
    [AccessFailedCount] INT NULL,
    --Extra Added--
    [Followers] INT NOT NULL DEFAULT 0,
    [DisplayName] NVARCHAR(256),
    [ImageUrl] NVARCHAR(256),
    [SpotifyUserId] NVARCHAR(256),
    [Href] NVARCHAR(256)
)
GO

CREATE TABLE [Playlist]
(
    [Id] nvarchar(450) NOT NULL,
    [UserId] nvarchar(450) NOT NULL,
    [Description] nvarchar(450),
    [Href] nvarchar(max) NOT NULL,
    [Name] nvarchar(450),
    [Public] bit DEFAULT 0,
    [Collaborative] bit DEFAULT 0,
    [URI] nvarchar(max)
)
GO

CREATE TABLE [Track]
(
    [Id] NVARCHAR(450) NOT NULL,
    [PlaylistId] NVARCHAR(450) NOT NULL,
    [DiscNumber] INT,
    [DurationMs] INT NOT NULL DEFAULT 0,
    [Explicit] BIT NOT NULL DEFAULT 0,
    [Href] NVARCHAR(450),
    [IsPlayable] BIT NOT NULL DEFAULT 0,
    [Name] NVARCHAR(450),
    [Popularity] INT,
    [PreviewUrl] NVARCHAR(450),
    [TrackNumber] INT NOT NULL DEFAULT 0,
    [Uri] NVARCHAR(450),
    [IsLocal] BIT NOT NULL DEFAULT 0
)
GO

ALTER TABLE [PUser] ADD CONSTRAINT [PK_PUser] PRIMARY KEY CLUSTERED ([Id] ASC)
GO
ALTER TABLE [Playlist] ADD CONSTRAINT [PK_Playlist] PRIMARY KEY CLUSTERED ([Id] ASC)
GO
ALTER TABLE [Playlist] ADD CONSTRAINT [Playlist_FK_PUser] FOREIGN KEY ([UserId]) REFERENCES [PUser] ([ID])
GO
ALTER TABLE [Track] ADD CONSTRAINT [PK_Track] PRIMARY KEY CLUSTERED ([Id] ASC)
GO
ALTER TABLE [Track] ADD CONSTRAINT [TRACK_FK_Playlist] FOREIGN KEY ([PlaylistId]) REFERENCES [Playlist] ([ID])
GO