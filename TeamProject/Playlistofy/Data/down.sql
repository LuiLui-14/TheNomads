<<<<<<< HEAD
﻿ALTER TABLE dbo.[AspNetRoleClaims] DROP CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId]
ALTER TABLE dbo.[AspNetUserClaims] DROP CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId]
ALTER TABLE dbo.[AspNetUserLogins] DROP CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId]
ALTER TABLE dbo.[AspNetUserRoles] DROP CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId]
ALTER TABLE dbo.[AspNetUserRoles] DROP CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId]
ALTER TABLE dbo.[AspNetUserTokens] DROP CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId]
ALTER TABLE dbo.[Playlist] DROP CONSTRAINT [FK_Playlist_AspNetUsers_UserId]
ALTER TABLE dbo.[Track] DROP CONSTRAINT [FK_Track_Playlist_PlaylistId]
DROP TABLE dbo.[__EFMigrationsHistory]
DROP TABLE dbo.[AspNetRoles]
DROP TABLE dbo.[AspNetUsers]
DROP TABLE dbo.[AspNetRoleClaims]
DROP TABLE dbo.[AspNetUserClaims]
DROP TABLE dbo.[AspNetUserLogins]
DROP TABLE dbo.[AspNetUserRoles]
DROP TABLE dbo.[AspNetUserTokens]
DROP TABLE dbo.[Playlist]
DROP TABLE dbo.[Track]
=======
﻿ALTER TABLE [Playlist] DROP CONSTRAINT [Playlist_FK_PUser];
ALTER TABLE [Track] DROP CONSTRAINT [TRACK_FK_Playlist];
ALTER TABLE [PUser] DROP CONSTRAINT [PK_PUser];
ALTER TABLE [Playlist] DROP CONSTRAINT [PK_Playlist];
ALTER TABLE [Track] DROP CONSTRAINT [PK_Track];

DROP Table [PUser];
DROP Table [Playlist];
DROP TABLE [Track];
>>>>>>> 8957ec8a5391f5ff66626eeb479bae5f4b033815
