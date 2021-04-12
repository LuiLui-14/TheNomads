using NUnit.Framework;
using Playlistofy.Models;

namespace Playlistofy.Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            Assert.Pass();
        }

        [Test]
        public void Track_Model_Default_Is_Valid()
        {
            Track newTrack = new Track();

            ModelValidator mv = new ModelValidator(newTrack);

            Assert.That(mv.Valid,Is.Not.False);
        }
    }
}