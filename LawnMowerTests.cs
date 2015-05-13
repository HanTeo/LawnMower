using System;
using LawnMowerLib;
using NUnit.Framework;

namespace LawnMower
{
    [TestFixture]
    public class LawnMowerTests
    {
        private LawnMowerLib.LawnMower mower;
        private Grid grid;

        [SetUp]
        public void SetUp()
        {
            mower = new LawnMowerLib.LawnMower();
            mower.Bearing = 'N';
            Grid.TryParse("5 5", out grid);
        }

        [Test]
        public void MowerTurnRight()
        {
            mower.TurnRight();
            Assert.AreEqual('E', mower.Bearing);
        }

        [Test]
        public void MowerTurnLeft()
        {
            mower.TurnLeft();
            Assert.AreEqual('W', mower.Bearing);
        }

        [Test]
        public void MowerTurnRightFullCircle()
        {
            mower.Bearing = 'W';
            mower.TurnRight();
            Assert.AreEqual('N', mower.Bearing);
        }

        [Test]
        public void MowerMoveWestLimited()
        {
            mower.Bearing = 'W';
            Assert.AreEqual(0, mower.X);
            Assert.AreEqual(0, mower.Y);
            mower.Move(grid);
            Assert.AreEqual(0, mower.X);
            Assert.AreEqual(0, mower.Y);
            Assert.AreEqual('W', mower.Bearing);
        }

        [Test]
        public void MowerMoveEast()
        {
            mower.Bearing = 'E';
            Assert.AreEqual(0, mower.X);
            Assert.AreEqual(0, mower.Y);
            mower.Move(grid);
            Assert.AreEqual(1, mower.X);
            Assert.AreEqual(0, mower.Y);
            Assert.AreEqual('E', mower.Bearing);
        }

        [Test]
        public void MowerMoveNorth()
        {
            mower.Bearing = 'N';
            Assert.AreEqual(0, mower.X);
            Assert.AreEqual(0, mower.Y);
            mower.Move(grid);
            Assert.AreEqual(0, mower.X);
            Assert.AreEqual(1, mower.Y);
            Assert.AreEqual('N', mower.Bearing);
        }

        [Test]
        public void MowerMoveSouthLimited()
        {
            mower.Bearing = 'S';
            Assert.AreEqual(0, mower.X);
            Assert.AreEqual(0, mower.Y);
            mower.Move(grid);
            Assert.AreEqual(0, mower.X);
            Assert.AreEqual(0, mower.Y);
            Assert.AreEqual('S', mower.Bearing);
        }

        [Test]
        public void MowerMoveSouth()
        {
            mower.Bearing = 'S';
            mower.X = 1;
            mower.Y = 1;
            mower.Move(grid);
            Assert.AreEqual(1, mower.X);
            Assert.AreEqual(0, mower.Y);
            Assert.AreEqual('S', mower.Bearing);
        }

        [Test]
        public void MowerMoveWest()
        {
            mower.Bearing = 'W';
            mower.X = 1;
            mower.Y = 1;
            mower.Move(grid);
            Assert.AreEqual(0, mower.X);
            Assert.AreEqual(1, mower.Y);
            Assert.AreEqual('W', mower.Bearing);
        }

        [Test]
        public void MowerMoveEastLimited()
        {
            mower.Bearing = 'E';
            mower.X = 5;
            mower.Y = 5;
            mower.Move(grid);
            Assert.AreEqual(5, mower.X);
            Assert.AreEqual(5, mower.Y);
            Assert.AreEqual('E', mower.Bearing);
        }

        [Test]
        public void MowerMoveNorthLimited()
        {
            mower.Bearing = 'N';
            mower.X = 5;
            mower.Y = 5;
            mower.Move(grid);
            Assert.AreEqual(5, mower.X);
            Assert.AreEqual(5, mower.Y);
            Assert.AreEqual('N', mower.Bearing);
        }

        [Test]
        public void MowerAcceptsInstructions()
        {
            Instructions instructions;
            LawnMowerLib.LawnMower mower;
            var success = LawnMowerLib.Instructions.TryParseInstructions("LRMLMRM", out instructions);
            Assert.IsTrue(success);
            Assert.AreEqual(7, instructions.Length);
            success = LawnMowerLib.LawnMower.TryParseLawnMower("1 0 N", out mower);
            Assert.IsTrue(success);
            Grid grid;
            success = Grid.TryParse("5 5", out grid);
            Assert.IsTrue(success);
            Assert.IsFalse(mower.IsFinished);
            mower.Go(grid, instructions);
            Assert.IsTrue(mower.IsFinished);
        }
    }
    
}
