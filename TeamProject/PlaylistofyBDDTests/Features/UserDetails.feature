Feature: As a user to this website, I want to be able to view a different users profile page so that I can find playlists created by that user.

Scenario: User Details Link on Playlist Details Page
	Given that a user is searching for a playlist
	When the playlist details page is navigated to
	And that playlist is marked as public
	Then there will be a link to the creator of the playlist on the page

Scenario: User Display Name is Present on Details Page
	Given that user is viewing a playlist details page
	And that playlist is marked as public
	When the link to the creator of the playlist is clicked
	Then the user will be redirected to a user details page
	And there will be a display name on the page