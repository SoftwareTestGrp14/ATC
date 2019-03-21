using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATC;
using NSubstitute;
using NUnit.Framework;
using TransponderReceiver;

namespace UnitTest
{
    [TestFixture]
    class TestATC
    {
        private ATC.ATC _uut;
        private ATM atm;
        private ITransponderReceiver _fakeTransponderReceiver;
        private IPlaneTracker _fakePlaneTracker;

        [SetUp]
        public void Setup()
        {
            _fakeTransponderReceiver = Substitute.For<ITransponderReceiver>();
            _fakePlaneTracker = Substitute.For<IPlaneTracker>();
            atm = new ATM(_fakePlaneTracker, _fakeTransponderReceiver);
            _uut = new ATC.ATC(atm);
        }

        [Test]
        public void Constructor_test()
        {
            Assert.That(_uut.ATM, Is.EqualTo(atm));
        }

        [Test]
        public void ATMset_SetToNewATM_ATMgetEqNewATM()
        {
            ITransponderReceiver _newFakeTransponderReceiver = Substitute.For<ITransponderReceiver>();
            IPlaneTracker _newFakePlaneTracker = Substitute.For<IPlaneTracker>();
            ATM newAtm = new ATM(_newFakePlaneTracker, _newFakeTransponderReceiver);
            _uut.ATM = newAtm;

            Assert.That(_uut.ATM, Is.EqualTo(newAtm));
        }

        [Test]
        public void ATMset_SetToNewATM_ATMgetNotEqOldATM()
        {
            ITransponderReceiver _newFakeTransponderReceiver = Substitute.For<ITransponderReceiver>();
            IPlaneTracker _newFakePlaneTracker = Substitute.For<IPlaneTracker>();
            ATM newAtm = new ATM(_newFakePlaneTracker, _newFakeTransponderReceiver);
            _uut.ATM = newAtm;

            Assert.That(_uut.ATM, Is.Not.EqualTo(atm));
        }
    }
}

