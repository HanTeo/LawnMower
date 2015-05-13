using System;
using NUnit.Framework;
using LawnMowerLib;

namespace LawnMower
{
    [TestFixture]
    public class InstructionsTests
    {
        private Instructions instructions;
        private char[] chars;

        [SetUp]
        public void Setup()
        {
            chars = new[]{ 'L', 'M', 'R', 'M' };
            instructions = new Instructions(chars);
        }

        [Test]
        public void InstructionsInitialiseCurrentWithDefaultValue()
        {
            Assert.AreEqual(default(char), instructions.Current);
        }

        [Test]
        public void InstructionsMoveNext()
        {
            for(var i=0; i<chars.Length; i++)
            {
                instructions.MoveNext();
                Assert.AreEqual(chars[i], instructions.Current);
            }
        }

        [Test]
        public void InstructionsMoveNextStopsOnTheLastItem()
        {
            bool moreElements = false;
            for(var i=0; i<instructions.Length+1; i++)
            {
                moreElements = instructions.MoveNext();
            }
            Assert.IsFalse(moreElements);
            Assert.AreEqual(chars[instructions.Length-1], instructions.Current);
        }

        [Test]
        public void InstructionsResetWorks()
        {
            for(var i=0; i<instructions.Length; i++)
            {
                instructions.MoveNext();
            }
            instructions.Reset();
            Assert.AreEqual(default(char), instructions.Current);
        }
    }
}
