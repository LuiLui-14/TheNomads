DROP Table [User];
DROP Table [Playlist];

ALTER TABLE [User] DROP CONSTRAINT [PK_User];
ALTER TABLE [Playlist] DROP CONSTRAINT [PK_Playlist];
ALTER TABLE [Playlist] DROP CONSTRAINT [Playlist_FK_USER];