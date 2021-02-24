INSERT INTO [User]([Id],[UserName],[Email],[EmailConfirmed],[PasswordHash],[PhoneNumber],[PhoneNumberConfirmed])
        VALUES ('testuser','TestUser','testuser@test.com',1,'1234','503-541-6969',1);

INSERT INTO [Playlist]([Id],[UserId],[Description],[Href],[Name],[Public],[Collaborative],[URI])
        VALUES ('Playlist_1','testuser', 'This is my new playlist', 'This the HREF', 'My playlists Name', 1, 0, 'www.google.com');