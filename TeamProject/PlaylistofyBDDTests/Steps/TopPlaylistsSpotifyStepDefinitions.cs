using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using Playlistofy.Models;
using System.Collections.Generic;
using System.Linq;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace PlaylistofyBDDTests.Steps
{
    [Binding]
    public class TopPlaylistsSpotifyStepDefinitions
    {
        // For additional details on SpecFlow step definitions see https://go.specflow.org/doc-stepdef

        private readonly ScenarioContext _ctx;
        private string _hostBaseName = @"https://playlistofy.azurewebsites.net/";
        private readonly IWebDriver _driver;

        public TopPlaylistsSpotifyStepDefinitions(ScenarioContext scenarioContext, IWebDriver driver)
        {
            _ctx = scenarioContext;
            _driver = driver;

        }


        [Given(@"in the AddSpotifyPlaylist view page")]
        public void GivenThatUserIsViewingTheAddPlaylistsViewPage()
        {
            _driver.Navigate().GoToUrl(_hostBaseName + @"Playlists/AddSpotifyPlaylists?topPlaylists=QueryTopPlaylists");
        }

        [When(@"the user clicks on the button named Browse Playlists")]
        public void WhenTheBrowsePlaylistsButtonIsClicked()
        {
            _driver.FindElement(By.Id("Browse Playlists")).Click();
        }

        [Then(@"the page will render 15 featured playlists from that user's spotify playlists recommendations")]
        public void ThenFifteenOrLessFeaturedPlaylistsWillBeShown()
        {
            IEnumerable<string> count = _driver.FindElement(By.Id("CountPlaylists"))
                                               .FindElements(By.TagName("a"))
                                               .Select(ab => ab.Text);
            Assert.That(count.Count, Is.LessThanOrEqualTo(15));
        }

        [Given(@"the user queried playlists")]
        public void GivenTheUserQuriedPlaylistsOnViewPage()
        {
            IEnumerable<string> count = _driver.FindElement(By.Id("CountPlaylists"))
                                               .FindElements(By.TagName("a"))
                                               .Select(ab => ab.Text);
            Assert.That(count, !Is.Null);
        }

        [Given(@"the user queried playlists")]
        public void GivenTheUserQuriedPlaylistsOnViewPageAreAList()
        {
            IEnumerable<string> ElementType = _driver.FindElement(By.Id("CountPlaylists"))
                                               .FindElements(By.TagName("a"))
                                               .Select(ab => ab.Text);
            Assert.That(ElementType.GetType, Is.TypeOf<IEnumerable<string>>());
        }

        [When(@"the user clicks on the button named Add")]
        public void WhenTheAddPlaylistButtonIsClicked()
        {
            _driver.FindElement(By.Id("AddPlaylist")).Click();
        }

        [Then(@"the user's playlist will be added and be shown the original Playlist Adding page again")]
        public void ThenViewPageWillBeShownAgain()
        {
            IEnumerable<string> count = _driver.FindElement(By.Id("CountPlaylists"))
                                               .FindElements(By.TagName("a"))
                                               .Select(ab => ab.Text);
            Assert.That(count.Count, Is.EqualTo(0) | Is.EqualTo(null));
        }
    }
}
