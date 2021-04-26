﻿ALTER TABLE [Playlist]			DROP CONSTRAINT [Playlist_FK_PUser];
ALTER TABLE [Track]				DROP CONSTRAINT [Track_FK_Playlist];
ALTER TABLE [PlaylistTrackMap]	DROP CONSTRAINT [PlaylistTrackMap_FK_Playlist];
ALTER TABLE [PlaylistTrackMap]	DROP CONSTRAINT [PlaylistTrackMap_FK_Track];
ALTER TABLE [ArtistTrackMap]	DROP CONSTRAINT [ArtistTrackMap_FK_Artist];
ALTER TABLE [ArtistTrackMap]	DROP CONSTRAINT [ArtistTrackMap_FK_Track];
ALTER TABLE [PUser]				DROP CONSTRAINT [PK_PUser];
ALTER TABLE [Playlist]			DROP CONSTRAINT [PK_Playlist];
ALTER TABLE [Track]				DROP CONSTRAINT [PK_Track];
ALTER TABLE [PlaylistTrackMap]	DROP CONSTRAINT [PK_PlaylistTrackMap];
ALTER TABLE [Artist]			DROP CONSTRAINT [PK_Artist];
ALTER TABLE [ArtistTrackMap]	DROP CONSTRAINT [PK_ArtistTrackMap];

DROP TABLE	[PUser];
DROP TABLE	[Playlist];
DROP TABLE	[Track];
DROP TABLE	[PlayilstTrackMap];
DROP TABLE  [Artist];
DROP TABLE  [ArtistTrackMap];
