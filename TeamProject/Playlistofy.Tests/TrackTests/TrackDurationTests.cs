/*
 * Benjamin Spencer
 * ID #177330542
 * As a developer I want to learn how to do unit testing in an ASP.NET Core MVC project by using it to test a simple feature of our project
 */
using NUnit.Framework;
using Playlistofy.Utils.AlgorithmicOperations;

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
            var timeinMs = 61000;

            var time = MsConversion.ConvertMsToMinSec(timeinMs);

            Assert.AreEqual("01:01", time);
        }

        [Test]
        public void TimeConversion_TimeShould_RoundDown()
        {
            var timeinMs = 60999;

            var time = MsConversion.ConvertMsToMinSec(timeinMs);

            Assert.AreEqual("01:00", time);
        }

        [Test]
        public void TimeConversion_TimeShouldNot_RoundUp()
        {
            var timeinMs = 61999;

            var time = MsConversion.ConvertMsToMinSec(timeinMs);

            Assert.AreNotEqual("01:02", time);
        }

        [Test]
        public void TimeConversion_OverlyLongTimeShouldReturn_TrackLengthTooLong()
        {
            var timeinMs = 9999999999999999;

            var time = MsConversion.ConvertMsToMinSec(timeinMs);

            Assert.AreEqual("Track Length Too Long", time);
        }

        [Test]
        public void TimeConversion_NegativeTimesShouldReturn_0()
        {
            var timeinMs = -600000;

            var time = MsConversion.ConvertMsToMinSec(timeinMs);

            Assert.AreEqual("00:00", time);
        }
    }
}