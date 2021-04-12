using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Playlistofy.Models;

namespace Playlistofy.Tests
{
    [TestFixture]
    public class TimeConverter
    {
        [Test]
        public void Milliseconds_To_Minutes()
        {
            // Arrange
            ConvertingTIme convertingTime = new ConvertingTIme();


            // Act
            // Random const number being converted to minutes to the hundreth decimal place
            int tempMilliseconds = 675025;
            double minutes = convertingTime.Milliseconds_ToMinutes(tempMilliseconds);


            // Assert
            Assert.That(minutes, Is.EqualTo(11.25));
        }
    }
}
