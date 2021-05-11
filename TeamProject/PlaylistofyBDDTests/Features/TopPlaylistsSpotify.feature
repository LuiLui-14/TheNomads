Feature: As a registered user, I want to be able to view and then add if I want, the top playlists on Spotify.

@mytag
Scenario: Query Playlists
	Given the user is logged in
	And in the "AddSpotifyPlaylist" view page
	When the user clicks on the button named "Query Best Playlists"
	Then the page will render the top 15 most popular tracks on Spotify

Scenario: Playlist Edit Tag
	Given a playlist that is already in the database
	And being viewed by the creator of the playlist on the usersPlaylist page
	When the edit button is pressed for that playlist
	Then there will be an area on the edit page to enter new tags and delete existing tags for that playlist

