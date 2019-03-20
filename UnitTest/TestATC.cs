using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATC;
using NSubstitute;
using NUnit.Framework;

namespace UnitTest
{
    class TestATC
    {
        private ATM _fakeATM;
        private ATC.ATC _uut;

        [SetUp]
        public void setup()
        {
            _fakeATM = Substitute.For<ATM>();
        }

        [Test]
        public void Constructor_test()
        {
            _uut = new ATC.ATC(_fakeATM);

            Assert.That(_uut, Is.Not.EqualTo(null));
        }
    }
}
