Feature: As a registered user, I want to be able to upload any playlist I want from my Playlistofy account to my Spotify Account.

@mytag
Scenario: Tag Association
	Given a playlist that is being created by a registered and logged in user
	And the Tags form is filled in
	When the playlist creation form is submitted
	Then that playlist should be associated with the tags entered

Scenario: Playlist Edit Tag
	Given a playlist that is already in the database
	And being viewed by the creator of the playlist on the usersPlaylist page
	When the edit button is pressed for that playlist
	Then there will be an area on the edit page to enter new tags and delete existing tags for that playlist

