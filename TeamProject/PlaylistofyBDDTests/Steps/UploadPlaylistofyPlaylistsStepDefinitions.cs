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
    }
}
