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
        private IAtm atm;
        
        [SetUp]
        public void Setup()
        {
            atm = Substitute.For<IAtm>();
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
            IAtm newAtm = Substitute.For<IAtm>();
            _uut.ATM = newAtm;

            Assert.That(_uut.ATM, Is.EqualTo(newAtm));
        }

        [Test]
        public void ATMset_SetToNewATM_ATMgetNotEqOldATM()
        {
            IAtm newAtm = Substitute.For<IAtm>();
            _uut.ATM = newAtm;

            Assert.That(_uut.ATM, Is.Not.EqualTo(atm));
        }
    }
}

