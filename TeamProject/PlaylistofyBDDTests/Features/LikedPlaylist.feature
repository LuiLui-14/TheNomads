@mstest:DeploymentItem:TechTalk.SpecFlow.MSTest.SpecFlowPlugin.dll
Feature: As a registered user I want to be able to like playlists so that I can access those playlists easily at my convenience

Background: 
	Given the following user
	| Id                                   | UserName                    | Password        |
	| 24ccdc1f-565c-42ec-80ca-8e6c0799229c | TestNoSpotify@TestCase.DOGE | )ddnbi5==DWqz!P |


Scenario: Like Playlist Button Changes Upon Click
	Given a logged in user
	And that user is viewing a playlist details page
	When the like playlist button is clicked
	Then the like playlist button will change to an unlike button

Scenario: Liked Playlist Table
	Given a logged in user
	When the Account page is navigated to
	Then there will be a liked playlist table on the page