Feature: As a registered user I want to be able to like playlists so that I can access those playlists easily at my convenience

Scenario: Liking a Playlist
	Given a registered user, user 1
	And that user is viewing a playlist details page
	When the like playlist button is clicked
	Then that user will be associated to that playlist in the LikedPlaylist table

Scenario: Like Playlist Button
	Given a registered user, user 1
	And that user is viewing a playlist details page
	When the like playlist button is clicked
	Then the like playlist button will change to a liked button

Scenario: Liked Playlist Table
	Given a registered user, user 1 
	When the Account page is navigated to
	Then there will be a liked playlist table on the page