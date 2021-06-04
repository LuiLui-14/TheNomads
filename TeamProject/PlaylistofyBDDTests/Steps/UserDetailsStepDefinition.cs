using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace PlaylistofyBDDTests.Steps
{
    [Binding]
    public sealed class UserDetailsStepDefinition
    {
        private readonly ScenarioContext _ctx;
        private string _hostBaseName = @"https://playlistofy.azurewebsites.net/";
        private readonly IWebDriver _driver;

        public UserDetailsStepDefinition(ScenarioContext scenarioContext, IWebDriver driver)
        {
            _ctx = scenarioContext;
            _driver = driver;

        }
        [Given(@"that a user is searching for a playlist")]
        public void GivenThatAUserIsSearchingForAPlaylist()
        {
            _driver.Navigate().GoToUrl(_hostBaseName + @"Search/PlaylistSearch?searchTerm=PubTest");
        }


        [When(@"the playlist details page is navigated to")]
        public void WhenThePlaylistDetailsPageIsNavigatedTo()
        {
            _driver.Navigate().GoToUrl(_hostBaseName + @"Playlists/DetailsFromSearch/3a6jm48gwa2ydtokc5b2nisz8");
        }

        [When(@"that playlist is marked as public")]
        [Given(@"that playlist is marked as public")]
        public void WhenThatPlaylistIsMarkedAsPublic()
        {
            WebDriverWait _wait = new WebDriverWait(_driver, new TimeSpan(0, 1, 0));

            _wait.Until(d => d.FindElement(By.Id("Public")));
            IWebElement pub = _driver.FindElement(By.Id("Public"));
        }

        [Then(@"there will be a link to the creator of the playlist on the page")]
        public void ThenThereWillBeALinkToTheCreatorOfThePlaylistOnThePage()
        {
            WebDriverWait _wait = new WebDriverWait(_driver, new TimeSpan(0, 1, 0));

            _wait.Until(d => d.FindElement(By.Id("creator")));
            IWebElement creator = _driver.FindElement(By.Id("creator"));
            Assert.That(creator, Is.Not.Null);
        }

        [When(@"the link to the creator of the playlist is clicked")]
        public void WhenTheLinkToTheCreatorOfThePlaylistIsClicked()
        {
            WebDriverWait _wait = new WebDriverWait(_driver, new TimeSpan(0, 1, 0));

            _wait.Until(d => d.FindElement(By.Id("creatorLink")));
            _driver.FindElement(By.Id("creatorLink")).Click();
        }

        [Then(@"the user will be redirected to a user details page")]
        public void ThenTheUserWillBeRedirectedToAUserDetailsPage()
        {
            Assert.That(_driver.Url, Is.EqualTo(_hostBaseName + @"Account/PublicUserDetails/Bspencer16"));
        }

        [Then(@"there will be a display name on the page")]
        public void ThenThereWillBeADisplayNameOnThePage()
        {
            WebDriverWait _wait = new WebDriverWait(_driver, new TimeSpan(0, 1, 0));

            _wait.Until(d => d.FindElement(By.Id("DispName")));
            IWebElement dispName = _driver.FindElement(By.Id("DispName"));
            Assert.That(dispName.Text, Is.EqualTo("Bspencer16"));
        }


    }
}
