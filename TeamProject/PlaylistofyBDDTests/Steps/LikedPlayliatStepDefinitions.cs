using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TechTalk.SpecFlow;

namespace PlaylistofyBDDTests.Steps
{
    [Binding]
    public sealed class LikedPlayliatStepDefinitions
    {
        // For additional details on SpecFlow step definitions see https://go.specflow.org/doc-stepdef

        private readonly ScenarioContext _scenarioContext;

        public LikedPlayliatStepDefinitions(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        [Given(@"a registered user, user (*)")]
        public void GivenARegisteredUser(int p0)
        {
            _scenarioContext.Pending();
        }

        [Given(@"that user is viewing a playlist details page")]
        public void GivenThatUserIsViewingAPlaylistDetailsPage()
        {
            _scenarioContext.Pending();
        }

        [When(@"the like playlist button is clicked")]
        public void WhenTheLikePlaylistButtonIsClicked()
        {
            _scenarioContext.Pending();
        }

        [Then(@"that user will be associated to that playlist in the LikedPlaylist table")]
        public void ThenThatUserWillBeAssociatedToThatPlaylistInTheLikedPlaylistTable()
        {
            _scenarioContext.Pending();
        }

        [Then(@"the like playlist button will change to a liked button")]
        public void ThenTheLikePlaylistButtonWillChangeToALikedButton()
        {
            _scenarioContext.Pending();
        }

        [When(@"the Account page is navigated to")]
        public void WhenTheAccountPageIsNavigatedTo()
        {
            _scenarioContext.Pending();
        }

        [Then(@"there will be a liked playlist table on the page")]
        public void ThenThereWillBeALikedPlaylistTableOnThePage()
        {
            _scenarioContext.Pending();
        }

    }
}
