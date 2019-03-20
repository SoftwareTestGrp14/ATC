using System.Collections.Generic;
using ATC;
using NUnit.Framework;
using NSubstitute;

namespace UnitTests3 { 
    public class TestSeparationCondition
    {
        private SeparationCondition _uut;
        private SeparationCondition equalsSeparation;
        private ITrack _fakeTrack1;
        private ITrack _fakeTrack2;


        [SetUp]
        public void Setup()
        {
            _fakeTrack1 = Substitute.For<ITrack>();
            _fakeTrack2 = Substitute.For<ITrack>();



            equalsSeparation = new SeparationCondition(_fakeTrack1, _fakeTrack2);
            _uut = new SeparationCondition(_fakeTrack1, _fakeTrack2);
        }

        [Test]
        public void TestEqualsMethod()
        {
           
            
            //Assert.That();
        }
    }
}