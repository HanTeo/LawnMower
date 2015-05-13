using System;
using NUnit.Framework;
using LawnMowerLib;
using System.Linq;

namespace LawnMower
{
    [TestFixture]
    public class SolverTests
    {
        private string Inputs;
        private string Outputs;
        private Solver solver;

        [SetUp]
        public void Setup()
        {
            Inputs = @"5 5
1 2 N
LMLMLMLMM
3 3 E
MMRMMRMRRM";

            Outputs = @"1 3 N
5 1 E";
            solver = new Solver();
        }

        [Test()]
        public void SolverShouldCorrectlyGenerateObjects()
        {
            var parsed = Solver.ParseRawInputs(Inputs);

            var grid = parsed.Item1;
            var problemSets = parsed.Item2;

            Assert.AreEqual(5, grid.Top);
            Assert.AreEqual(5, grid.Right);
            Assert.AreEqual(2, problemSets.Count());
        }

        [Test()]
        public void SolverShouldReturnNullIfGridFormatIncorrect()
        {
            var badInputs = @"5 M
1 2 N
LMLMLMLMM";
            var parsed = Solver.ParseRawInputs(badInputs);

            Assert.IsNull(parsed);
        }

        [Test()]
        public void SolverShouldReturnNullIfMowerFacingFormatIncorrect()
        {
            var badInputs = @"5 5
1 2 R
LMLMLMLMM";
            var parsed = Solver.ParseRawInputs(badInputs);

            Assert.IsNull(parsed);
        }

        [Test()]
        public void SolverShouldReturnNullIfMowerXYFormatIncorrect()
        {
            var badInputs = @"5 5
1 N N
LMLMLMLMM";
            var parsed = Solver.ParseRawInputs(badInputs);

            Assert.IsNull(parsed);
        }

        [Test()]
        public void SolverShouldReturnNullIfInstructionsFormatIncorrect()
        {
            var badInputs = @"5 5
1 1 N
LMLMLXXXXL";
            var parsed = Solver.ParseRawInputs(badInputs);

            Assert.IsNull(parsed);
        }

        [Test]
        public void SolverCorrectlyCreatesMower()
        {
            var parsed = Solver.ParseRawInputs(Inputs);

            Assert.IsNotNull(parsed);

            var grid = parsed.Item1;
            var problemSets = parsed.Item2;

            var mower = problemSets.FirstOrDefault().Mower;

            Assert.AreEqual(1, mower.X);
            Assert.AreEqual(2, mower.Y);
            Assert.AreEqual('N', mower.Bearing);
        }

        [Test]
        public void SolverCorrectlyInstructions()
        {
            var parsed = Solver.ParseRawInputs(Inputs);

            Assert.IsNotNull(parsed);

            var grid = parsed.Item1;
            var problemSets = parsed.Item2;

            var instructions = problemSets.FirstOrDefault().Instructions;

            Assert.AreEqual(9, instructions.Length);
            Assert.AreEqual("LMLMLMLMM", instructions.ToString());
        }
         

        [Test]
        public void SolvePositive()
        {
            var outputs = solver.Solve(Inputs);
            Assert.AreEqual(Outputs, outputs);
        }

        [Test]
        public void SolveNegative()
        {
            var badInputs = @"5 5
1 2 N
LMLML2LMM
3 3 E
MMRMMRMRRM";

            var expected = @"Error Parsing Input: 5 5
1 2 N
LMLML2LMM
3 3 E
MMRMMRMRRM";

            var outputs = solver.Solve(badInputs);
            Assert.AreEqual(outputs, expected);
        }
    }
}

