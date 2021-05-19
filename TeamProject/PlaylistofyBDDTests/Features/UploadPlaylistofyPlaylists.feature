Feature: As a registered user, I want to be able to upload any playlist I want from my Playlistofy account to my Spotify Account.

@mytag
Scenario: View Playlists
	Given the user is logged in
	And playlists rendered into a list
	When the user views the page
	Then the playlists will show

Scenario: Upload Playlists
	Given the user is logged in
	When the user clicks on the button named "Add Playlist"
	Then the playlist will be add to the new created Spotify Playlist

