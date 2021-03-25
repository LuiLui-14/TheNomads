ALTER TABLE [Playlist] DROP CONSTRAINT [Playlist_FK_User];
ALTER TABLE [Track] DROP CONSTRAINT [TRACK_FK_Playlist];
ALTER TABLE [User] DROP CONSTRAINT [PK_User];
ALTER TABLE [Playlist] DROP CONSTRAINT [PK_Playlist];
ALTER TABLE [Track] DROP CONSTRAINT [PK_Track];

DROP Table [User];
DROP Table [Playlist];
DROP TABLE [Track];
