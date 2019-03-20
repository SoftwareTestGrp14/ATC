using System;
using System.Collections.Generic;
using ATC;
using NUnit.Framework;
using NSubstitute;

namespace UnitTests3 { 
    public class TestSeparationCondition
    {
        private ISeparationCondition _uut;
        private ITrack t1;
        private ITrack t2;
        private ITrack t3;


        [SetUp]
        public void Setup()
        {
            //_fakeTrack1 = Substitute.For<ITrack>();
            //_fakeTrack2 = Substitute.For<ITrack>();

            //equalsSeparation = new SeparationCondition(_fakeTrack1, _fakeTrack2);

            DateTime date = new DateTime(2019, 3, 19, 13, 0, 0);

            t1 = new Track("Tag1", 10000, 10000, 2000, 3, 90, date);
            t2 = new Track("Tag2", 10000, 10000, 2000, 3, 90, date);
            t3 = new Track("Tag3", 10000, 10000, 2000, 3, 90, date);

            _uut = new SeparationCondition(t1, t2);
        }

        [Test]
        public void TestEqualsMethod_SameOrder_SameTracks_ExpectedTrue()
        {
            var sc1 = new SeparationCondition(t1, t2);

            Assert.That(_uut.Equals(sc1), Is.True);
        }

        [Test]
        public void TestEqualsMethod_ReverseOrder_SameTracks_ExpectedTrue()
        {
            var sc1 = new SeparationCondition(t2, t1);

            Assert.That(_uut.Equals(sc1), Is.True);
        }

        [Test]
        public void TestEqualsMethod_DifferentTracks_ExpectedFalse()
        {
            var sc1 = new SeparationCondition(t1, t3);

            Assert.That(_uut.Equals(sc1), Is.False);
        }
    }
}