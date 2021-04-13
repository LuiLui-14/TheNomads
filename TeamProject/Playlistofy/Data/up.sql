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


CREATE TABLE [Track]
(
    [Id] NVARCHAR(450) NOT NULL,
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
    [IsLocal] BIT NOT NULL DEFAULT 0,
    [Duration] NVARCHAR(450)
)


CREATE TABLE [PlaylistTrackMap]
(
    [ID] NVARCHAR(450) NOT NULL,
    [PlaylistID] NVARCHAR(450) NOT NULL,
    [TrackID] NVARCHAR(45) NOT NULL
)


ALTER TABLE [PUser] ADD CONSTRAINT [PK_PUser] PRIMARY KEY CLUSTERED ([Id] ASC)

ALTER TABLE [Playlist] ADD CONSTRAINT [PK_Playlist] PRIMARY KEY CLUSTERED ([Id] ASC)

ALTER TABLE [Track] ADD CONSTRAINT [PK_Track] PRIMARY KEY CLUSTERED ([Id] ASC)

ALTER TABLE [PlaylistTrackMap] ADD CONSTRAINT [PK_PlaylistTrackMap] PRIMARY KEY CLUSTERED ([ID] ASC)


ALTER TABLE [Playlist] ADD CONSTRAINT [Playlist_FK_PUser] FOREIGN KEY ([UserId]) REFERENCES [PUser] ([ID])

ALTER TABLE [PlaylistTrackMap] ADD CONSTRAINT [PlaylistTrackMap_FK_Playlist] FOREIGN KEY ([PlaylistId]) REFERENCES [Playlist] ([ID])

ALTER TABLE [PlaylistTrackMap] ADD CONSTRAINT [PlaylistTrackMap_FK_Track] FOREIGN KEY ([TrackId]) REFERENCES [Track] ([ID])
