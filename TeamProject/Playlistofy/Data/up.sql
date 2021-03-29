<<<<<<< HEAD
﻿CREATE TABLE [__EFMigrationsHistory] (
    [MigrationId] nvarchar(150) NOT NULL,
    [ProductVersion] nvarchar(32) NOT NULL,
    CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );

CREATE TABLE [AspNetRoles] (
    [Id] nvarchar(450) NOT NULL,
    [Name] nvarchar(256) NULL,
    [NormalizedName] nvarchar(256) NULL,
    [ConcurrencyStamp] nvarchar(max) NULL,
    CONSTRAINT [PK_AspNetRoles] PRIMARY KEY ([Id])
);

CREATE TABLE [AspNetUsers] (
=======
﻿CREATE TABLE [PUser]
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
>>>>>>> 8957ec8a5391f5ff66626eeb479bae5f4b033815
    [Id] nvarchar(450) NOT NULL,
    [Followers] int NOT NULL,
    [DisplayName] nvarchar(max) NULL,
    [ImageUrl] nvarchar(max) NULL,
    [SpotifyUserId] nvarchar(max) NULL,
    [UserName] nvarchar(256) NULL,
    [NormalizedUserName] nvarchar(256) NULL,
    [Email] nvarchar(256) NULL,
    [NormalizedEmail] nvarchar(256) NULL,
    [EmailConfirmed] bit NOT NULL,
    [PasswordHash] nvarchar(max) NULL,
    [SecurityStamp] nvarchar(max) NULL,
    [ConcurrencyStamp] nvarchar(max) NULL,
    [PhoneNumber] nvarchar(max) NULL,
    [PhoneNumberConfirmed] bit NOT NULL,
    [TwoFactorEnabled] bit NOT NULL,
    [LockoutEnd] datetimeoffset NULL,
    [LockoutEnabled] bit NOT NULL,
    [AccessFailedCount] int NOT NULL,
    CONSTRAINT [PK_AspNetUsers] PRIMARY KEY ([Id])
);

CREATE TABLE [AspNetRoleClaims] (
    [Id] int NOT NULL IDENTITY,
    [RoleId] nvarchar(450) NOT NULL,
    [ClaimType] nvarchar(max) NULL,
    [ClaimValue] nvarchar(max) NULL,
    CONSTRAINT [PK_AspNetRoleClaims] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles] ([Id]) ON DELETE CASCADE
);

CREATE TABLE [AspNetUserClaims] (
    [Id] int NOT NULL IDENTITY,
    [UserId] nvarchar(450) NOT NULL,
    [ClaimType] nvarchar(max) NULL,
    [ClaimValue] nvarchar(max) NULL,
    CONSTRAINT [PK_AspNetUserClaims] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);

CREATE TABLE [AspNetUserLogins] (
    [LoginProvider] nvarchar(128) NOT NULL,
    [ProviderKey] nvarchar(128) NOT NULL,
    [ProviderDisplayName] nvarchar(max) NULL,
    [UserId] nvarchar(450) NOT NULL,
    CONSTRAINT [PK_AspNetUserLogins] PRIMARY KEY ([LoginProvider], [ProviderKey]),
    CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);

CREATE TABLE [AspNetUserRoles] (
    [UserId] nvarchar(450) NOT NULL,
    [RoleId] nvarchar(450) NOT NULL,
    CONSTRAINT [PK_AspNetUserRoles] PRIMARY KEY ([UserId], [RoleId]),
    CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);

CREATE TABLE [AspNetUserTokens] (
    [UserId] nvarchar(450) NOT NULL,
    [LoginProvider] nvarchar(128) NOT NULL,
    [Name] nvarchar(128) NOT NULL,
    [Value] nvarchar(max) NULL,
    CONSTRAINT [PK_AspNetUserTokens] PRIMARY KEY ([UserId], [LoginProvider], [Name]),
    CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);

CREATE TABLE [Playlist] (
    [Id] nvarchar(450) NOT NULL,
    [UserId] nvarchar(450) NULL,
    [Description] nvarchar(450) NULL,
    [Href] nvarchar(max) NOT NULL,
<<<<<<< HEAD
    [Name] nvarchar(450) NULL,
    [Public] bit NULL,
    [Collaborative] bit NULL,
    [URI] nvarchar(max) NULL,
    [trackCount] int NOT NULL,
    CONSTRAINT [PK_Playlist] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Playlist_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);

CREATE TABLE [Track] (
    [Id] nvarchar(450) NOT NULL,
    [DiscNumber] int NOT NULL,
    [DurationMs] int NOT NULL,
    [Explicit] bit NOT NULL,
    [Href] nvarchar(max) NULL,
    [IsPlayable] bit NOT NULL,
    [Name] nvarchar(max) NULL,
    [Popularity] int NOT NULL,
    [PreviewUrl] nvarchar(max) NULL,
    [TrackNumber] int NOT NULL,
    [Uri] nvarchar(max) NULL,
    [IsLocal] bit NOT NULL,
    [PlaylistId] nvarchar(450) NULL,
    CONSTRAINT [PK_Track] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Track_Playlist_PlaylistId] FOREIGN KEY ([PlaylistId]) REFERENCES [Playlist] ([Id]) ON DELETE NO ACTION
);

CREATE INDEX [IX_AspNetRoleClaims_RoleId] ON [AspNetRoleClaims] ([RoleId]);

CREATE UNIQUE INDEX [RoleNameIndex] ON [AspNetRoles] ([NormalizedName]) WHERE [NormalizedName] IS NOT NULL;

CREATE INDEX [IX_AspNetUserClaims_UserId] ON [AspNetUserClaims] ([UserId]);

CREATE INDEX [IX_AspNetUserLogins_UserId] ON [AspNetUserLogins] ([UserId]);

CREATE INDEX [IX_AspNetUserRoles_RoleId] ON [AspNetUserRoles] ([RoleId]);

CREATE INDEX [EmailIndex] ON [AspNetUsers] ([NormalizedEmail]);

CREATE UNIQUE INDEX [UserNameIndex] ON [AspNetUsers] ([NormalizedUserName]) WHERE [NormalizedUserName] IS NOT NULL;

CREATE INDEX [IX_Playlist_UserId] ON [Playlist] ([UserId]);

CREATE INDEX [IX_Track_PlaylistId] ON [Track] ([PlaylistId]);

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20210308190216_IdentityDBUser', N'5.0.3');

=======
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
>>>>>>> 8957ec8a5391f5ff66626eeb479bae5f4b033815
