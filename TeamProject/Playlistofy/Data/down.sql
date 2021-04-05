ALTER TABLE [Playlist]			DROP CONSTRAINT [Playlist_FK_PUser];
ALTER TABLE [Track]				DROP CONSTRAINT [TRACK_FK_Playlist];
ALTER TABLE [PlaylistTrackMap]	DROP CONSTRAINT [PlaylistTrackMap_FK_Playlist];
ALTER TABLE [PlaylistTrackMap]	DROP CONSTRAINT [PlaylistTrackMap_FK_Track];
ALTER TABLE [PUser]				DROP CONSTRAINT [PK_PUser];
ALTER TABLE [Playlist]			DROP CONSTRAINT [PK_Playlist];
ALTER TABLE [Track]				DROP CONSTRAINT [PK_Track];
ALTER TABLE [PlaylistTrackMap]	DROP CONSTRAINT [PK_PlaylistTrackMap];


DROP TABLE	[PUser];
DROP TABLE	[Playlist];
DROP TABLE	[Track];
DROP TABLE	[PlayilstTrackMap];
