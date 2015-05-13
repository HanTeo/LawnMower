using System;
using NUnit.Framework;
using LawnMowerLib;

namespace LawnMower
{
    [TestFixture]
    public class GridTests
    {
        private Grid grid;

        [SetUp]
        public void Setup()
        {
            grid = new Grid();
        }

        [Test]
        public void GridInitialisedWith00LeftBottom()
        {
            Assert.AreEqual(0, grid.Bottom);
            Assert.AreEqual(0, grid.Left);
        }

        [Test]
        public void GridTryParsePositive()
        {
            var success = Grid.TryParse("5 5", out grid);

            Assert.IsTrue(success);
            Assert.AreEqual(5, grid.Top);
            Assert.AreEqual(5, grid.Right);
        }

        [Test]
        public void GridTryParseNegative()
        {
            var success = Grid.TryParse("5 M", out grid);
            Assert.IsFalse(success);
            Assert.IsNull(grid);

            success = Grid.TryParse("M 5", out grid);
            Assert.IsFalse(success);
            Assert.IsNull(grid);

            success = Grid.TryParse("5", out grid);
            Assert.IsFalse(success);
            Assert.IsNull(grid);

            success = Grid.TryParse("5 5 5", out grid);
            Assert.IsFalse(success);
            Assert.IsNull(grid);
        }

        [Test]
        public void GridShouldHaveNoOccupiedCellsByDefault()
        {
            var success = Grid.TryParse("1 1", out grid);
            Assert.IsTrue(success);
            Assert.IsNotNull(grid);

            Assert.IsTrue(grid.IsOccupiable(0,0));
            Assert.IsTrue(grid.IsOccupiable(0,1));
            Assert.IsTrue(grid.IsOccupiable(1,0));
            Assert.IsTrue(grid.IsOccupiable(1,1));
        }

        [Test]
        public void GridShouldRememberOccupiedCells()
        {
            var success = Grid.TryParse("1 1", out grid);
            Assert.IsTrue(success);

            grid.Park(1, 1);
            Assert.IsFalse(grid.IsOccupiable(1,1));

            grid.Park(1, 0);
            Assert.IsFalse(grid.IsOccupiable(1,0));

            Assert.IsTrue(grid.IsOccupiable(0,0));
            Assert.IsTrue(grid.IsOccupiable(0,1));
        }
            

        [Test]
        public void GridShouldKnowItsBoundaries()
        {
            var success = Grid.TryParse("4 4", out grid);

            Assert.IsTrue(success);
            Assert.IsNotNull(grid);

            // Inside
            Assert.IsTrue(grid.IsOccupiable(0,0));
            Assert.IsTrue(grid.IsOccupiable(0,1));
            Assert.IsTrue(grid.IsOccupiable(1,0));
            Assert.IsTrue(grid.IsOccupiable(1,1));

            // Outside
            Assert.IsFalse(grid.IsOccupiable(5,5));
            Assert.IsFalse(grid.IsOccupiable(0,5));
            Assert.IsFalse(grid.IsOccupiable(5,0));
            Assert.IsFalse(grid.IsOccupiable(0,-1));
            Assert.IsFalse(grid.IsOccupiable(-1,0));
            Assert.IsFalse(grid.IsOccupiable(-1,-1));
        }
    }
}

