using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATC;
using NUnit.Framework;
using NSubstitute;

namespace TestApp
{
    [TestFixture]
    class AirSpaceTest
    {
        private AirSpace _uut;

        [SetUp]
        public void SetUp()
        {
            _uut = new AirSpace();
        }

        [Test]
        public void GetXEndPoint_XEndPointEq80000()
        {
            Assert.That(_uut.GetXEndPoint(), Is.EqualTo(80000));
        }

        [Test]
        public void GetYEndPoint_YEndPointEq80000()
        {
            Assert.That(_uut.GetYEndPoint(), Is.EqualTo(80000));
        }


    }
}
