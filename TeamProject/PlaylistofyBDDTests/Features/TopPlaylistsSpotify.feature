Feature: As a registered user, I want to be able to view and then add if I want, the top playlists on Spotify.

Background: 
	Given the following user
	| Id                                   | UserName                    | Password        |
	| 24ccdc1f-565c-42ec-80ca-8e6c0799229c | TestNoSpotify@TestCase.DOGE | )ddnbi5==DWqz!P |
	
@mytag
Scenario: Query Playlists
	Given the user is logged in
	And in the "AddSpotifyPlaylist" view page
	When the user clicks on the button named "Query Best Playlists"
	Then the page will render the top 15 most popular tracks on Spotify based on follower count

@mytag
Scenario: Add Playlists
	Given the user queried playlists
	And playlists rendered into a list
	When the user clicks on the button named "Add"
	Then the user will be able to add the playlist