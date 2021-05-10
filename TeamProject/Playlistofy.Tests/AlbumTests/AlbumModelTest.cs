/*
 * Benjamin Spencer
 * ID #177100040
 * As a user, I want to be able to see albums that are present on this website, so that I can view more information about the album.
 */
using NUnit.Framework;
using Playlistofy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Playlistofy.Tests.AlbumTests
{
    class AlbumModelTest
    {
        [Test]
        public void Album_Model_Default_Is_NotValid()
        {
            Album a = new Album();

            ModelValidator mv = new ModelValidator(a);

            Assert.That(mv.Valid, Is.False);
        }

        [Test]
        public void AlbumModelAttributes_FulfilledRequirmentsShould_BeVaild()
        {
            Album a = new Album()
            {  
                Id = "t",
                AlbumType = "single",
                Popularity = 55
            };

            ModelValidator mv = new ModelValidator(a);

            Assert.That(mv.Valid, Is.True);
        }

        [Test]
        public void testingPlaylist()
        {
            PUser pUser = new PUser();
            Playlist p = new Playlist()
            {
                Id = "df93idfhio38040fji4",
                User = pUser,
                UserId = "wrges45ge45gwse5"
            };

            ModelValidator mv = new ModelValidator(p);

            Assert.That(mv.Valid, Is.True);
        }
    }
}
