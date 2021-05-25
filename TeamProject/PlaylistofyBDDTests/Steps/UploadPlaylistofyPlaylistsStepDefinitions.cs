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
    public class UploadPlaylistofyPlaylistsStepDefinitions
    {
        // For additional details on SpecFlow step definitions see https://go.specflow.org/doc-stepdef

        private readonly ScenarioContext _ctx;
        private string _hostBaseName = @"https://localhost:5001/";//https://playlistofy.azurewebsites.net/";
        private readonly IWebDriver _driver;

        public UploadPlaylistofyPlaylistsStepDefinitions(ScenarioContext scenarioContext, IWebDriver driver)
        {
            _ctx = scenarioContext;
            _driver = driver;

        }

        [Given(@"the following user")]
        public void GivenTheFollowingUser(Table table)
        {
            IEnumerable<TestUser> puser = table.CreateSet<TestUser>();
            _ctx["User"] = puser;
        }

        [Given(@"the user is logged in")]
        public void GivenALoggedInUser()
        {
            _driver.Navigate().GoToUrl(_hostBaseName + @"Identity/Account/Login");
            IEnumerable<TestUser> users = (IEnumerable<TestUser>)_ctx["User"];
            TestUser u = users.FirstOrDefault();
            _driver.FindElement(By.Id("Input_Email")).SendKeys(u.UserName);
            _driver.FindElement(By.Id("Input_Password")).SendKeys(u.Password);
            _driver.FindElement(By.Id("account")).FindElement(By.CssSelector("button[type=submit]")).Click();
        }

        [When(@"the user views the page AddSpotifyPlaylist")]
        public void GivenThatUserIsViewingTheUploadPlaylistsViewPage()
        {
            _driver.Navigate().GoToUrl(_hostBaseName + @"Playlists/AddSpotifyPlaylists?topPlaylists=QueryTopPlaylists");
        }

        [Then(@"the user will see their playlists as a list")]
        public void GivenTheUsersPlaylistsOnViewPageAreAList()
        {
            IEnumerable<string> Elements = _driver.FindElement(By.ClassName("text-right"))
                                               .FindElements(By.TagName("td"))
                                               .Select(ab => ab.Text);
            Assert.That(Elements.GetType, Is.TypeOf<IEnumerable<string>>());
        }

        [Given(@"the user is logged in")]
        public void GivenUSerIsLoggedIn()
        {
            _driver.Navigate().GoToUrl(_hostBaseName + @"Identity/Account/Login");
            IEnumerable<TestUser> users = (IEnumerable<TestUser>)_ctx["User"];
            TestUser u = users.FirstOrDefault();
            _driver.FindElement(By.Id("Input_Email")).SendKeys(u.UserName);
            _driver.FindElement(By.Id("Input_Password")).SendKeys(u.Password);
            _driver.FindElement(By.Id("account")).FindElement(By.CssSelector("button[type=submit]")).Click();
        }

        [Given(@"the user can see a list of their playlists")]
        public void GivenTheUserCanSeeAListOfTheirPlaylists()
        {
            IEnumerable<string> Elements = _driver.FindElement(By.ClassName("text-right"))
                                               .FindElements(By.TagName("td"))
                                               .Select(ab => ab.Text);
            Assert.That(Elements.GetType, Is.TypeOf<IEnumerable<string>>());
        }

        [When(@"the user clicks on the button named Add Playlist")]
        public void WhenTheAddPlaylistButtonIsClicked()
        {
            _driver.FindElement(By.Id("AddPlaylist")).Click();
        }

        [Then(@"the user's playlist will be redirected to Spotify with their new Playlist")]
        public void ThenUserIsRedirectedToSpotify()
        {
            var Title = _driver.Title;
            Assert.That(Title, Is.EqualTo("Spotify"));
        }
    }
}
