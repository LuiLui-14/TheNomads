Feature: As a registered user, I want to be able to view and then add if I want, the top playlists on Spotify.

Background: 
	Given the following user
	| UserName                | Password        |
	| bspencer16@mail.wou.edu | yDjLwy97hDxVUs! |

@mytag
Scenario: Query Playlists
	Given the user is logged in
	And in the AddSpotifyPlaylist view page
	When the user clicks on the button named Browse Playlists
	Then the page will render 15 featured playlists from that user's spotify playlists recommendations

@mytag
Scenario: Add Playlists
	Given the user queried playlists
	And playlists rendered into a list
	When the user clicks on the button named "Add"
	Then the user's playlist will be added and no playlists will be shown on the screen anymore