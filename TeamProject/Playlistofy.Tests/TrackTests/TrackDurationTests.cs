/*
 * ID #177330542
 * As a developer I want to learn how to do unit testing in an ASP.NET Core MVC project by using it to test a simple feature of our project
 */
using NUnit.Framework;
using Playlistofy.Controllers;

namespace Playlistofy.Tests
{
    public class TrackDurationTests
    {
        
        [SetUp]
        public void Setup()
        {
            
        }

        [Test]
        public void TimeConversion_ConvertedTimeShould_BeMinutesAndSeconds()
        {
            var time = PlaylistsController.ConvertMsToMinSec(61000);
            Assert.AreEqual("01:01", time);
        }

        [Test]
        public void TimeConversion_TimeShould_RoundDown()
        {
            var time = PlaylistsController.ConvertMsToMinSec(60999);
            Assert.AreEqual("01:00", time);
        }

        [Test]
        public void TimeConversion_TimeShouldNot_RoundUp()
        {
            var time = PlaylistsController.ConvertMsToMinSec(61999);
            Assert.AreNotEqual("01:02", time);
        }
    }
}