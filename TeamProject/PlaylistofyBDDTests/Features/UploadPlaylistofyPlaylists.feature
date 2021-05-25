Feature: As a registered user, I want to be able to upload any playlist I want from my Playlistofy account to my Spotify Account.

Background: 
	Given the following user
	| Id                                   | UserName                    | Password        |
	| 24ccdc1f-565c-42ec-80ca-8e6c0799229c | TestNoSpotify@TestCase.DOGE | )ddnbi5==DWqz!P |

@mytag
Scenario: View Playlists
	Given the user is logged in
	When the user views the page "AddSpotifyPlaylist"
	Then the user will see their playlists as a list

Scenario: Upload Playlists
	Given the user is logged in
	And the user can see a list of their playlists
	When the user clicks on the button named "Add Playlist"
	Then the user's playlist will be redirected to Spotify with their new Playlist

