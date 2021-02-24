CREATE TABLE [User]
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
    [AccessFailedCount] INT NULL    
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

ALTER TABLE [User] ADD CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED ([Id] ASC)
GO
ALTER TABLE [Playlist] ADD CONSTRAINT [PK_Playlist] PRIMARY KEY CLUSTERED ([Id] ASC)
GO
ALTER TABLE [Playlist] ADD CONSTRAINT [Playlist_FK_USER] FOREIGN KEY ([UserId]) REFERENCES [User] ([ID])
GO