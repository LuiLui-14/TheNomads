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
    public class TestUser
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
    [Binding]
    public sealed class LikedPlaylistStepDefinitions
    {
        // For additional details on SpecFlow step definitions see https://go.specflow.org/doc-stepdef

        private readonly ScenarioContext _ctx;
        private string _hostBaseName = @"https://localhost:5001/";//https://playlistofy.azurewebsites.net/";
        private readonly IWebDriver _driver;

        public LikedPlaylistStepDefinitions(ScenarioContext scenarioContext, IWebDriver driver)
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

        [Given(@"a logged in user")]
        public void GivenALoggedInUser()
        {
            _driver.Navigate().GoToUrl(_hostBaseName + @"Identity/Account/Login");
            IEnumerable<TestUser> users = (IEnumerable<TestUser>)_ctx["User"];
            TestUser u = users.FirstOrDefault();
            _driver.FindElement(By.Id("Input_Email")).SendKeys(u.UserName);
            _driver.FindElement(By.Id("Input_Password")).SendKeys(u.Password);  
            _driver.FindElement(By.Id("account")).FindElement(By.CssSelector("button[type=submit]")).Click();
        }


        [Given(@"that user is viewing a playlist details page")]
        public void GivenThatUserIsViewingAPlaylistDetailsPage()
        {
            _driver.Navigate().GoToUrl(_hostBaseName + @"Playlists/DetailsFromSearch/3a6jm48gwa2ydtokc5b2nisz8");
        }

        [When(@"the like playlist button is clicked")]
        public void WhenTheLikePlaylistButtonIsClicked()
        {
            _driver.FindElement(By.Id("LikePlaylist")).Click();
        }

        [Then(@"that user will be associated to that playlist in the LikedPlaylist table")]
        public void ThenThatUserWillBeAssociatedToThatPlaylistInTheLikedPlaylistTable()
        {
            _ctx.Pending();
        }

        [Then(@"the like playlist button will change to an unlike button")]
        public void ThenTheLikePlaylistButtonWillChangeToAUnlikeButton()
        {
            IWebElement unlike = _driver.FindElement(By.Id("UnlikePlaylist"));
            Assert.That(unlike, Is.Not.Null);
        }

        [When(@"the Account page is navigated to")]
        public void WhenTheAccountPageIsNavigatedTo()
        {
            _ctx.Pending();
        }

        [Then(@"there will be a liked playlist table on the page")]
        public void ThenThereWillBeALikedPlaylistTableOnThePage()
        {
            _ctx.Pending();
        }

    }
}
