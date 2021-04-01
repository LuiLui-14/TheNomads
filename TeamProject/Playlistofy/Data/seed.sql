INSERT INTO [PUser]([Id],[UserName],[Email],[EmailConfirmed],[PasswordHash],[PhoneNumber],[PhoneNumberConfirmed])
        VALUES ('testuser','TestUser','testuser@test.com',1,'1234','503-541-6969',1);

INSERT INTO [Playlist]([Id],[UserId],[Description],[Href],[Name],[Public],[Collaborative],[URI])
        VALUES ('Playlist_1','testuser', 'This is my new playlist', 'This the HREF', 'My playlists Name', 1, 0, 'www.google.com');

INSERT INTO [Track]([ID],[PlaylistId],[DiscNumber],[Href],[IsPlayable],[Name],[Popularity],[TrackNumber],[Uri],[IsLocal])
        VALUES ('Track_1','Playlist_1',3000,'Track Href',1,'Bettle Track',9,35,'www.spotify.uri.track',0);