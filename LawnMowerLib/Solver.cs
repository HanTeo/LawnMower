using System;
using System.Collections.Generic;
using System.Linq;

namespace LawnMowerLib
{
    public class Solver
    {
        public string Solve(string input)
        {
            var parsed = ParseRawInputs(input);

            if (parsed == null)
            {
                return "Error Parsing Input: " + input;
            }

            var grid = parsed.Item1;
            var problemSets = parsed.Item2;

            var res = new List<string>();

            // Model collision on grid??

            foreach(var problem in problemSets)
            {
                var mower = problem.Mower;
                var instructions = problem.Instructions;
                mower.Go(grid, instructions);
                res.Add(string.Format("{0} {1} {2}", mower.X, mower.Y, mower.Bearing));
            }

            return string.Join("\n", res);
        }

        public static Tuple<Grid, IEnumerable<ProblemSet>> ParseRawInputs(string inputs)
        {
            var lines = inputs.Split(new[]{ '\n' });

            return ParseRawInputs(lines);
        }

        public static Tuple<Grid, IEnumerable<ProblemSet>> ParseRawInputs(IEnumerable<string> inputs)
        {
            var problemSets = new List<ProblemSet>();
            var e = inputs.AsEnumerable().GetEnumerator();

            // Grid
            e.MoveNext();
            var rawGrid = e.Current;
            Grid grid;
            if (!Grid.TryParse(rawGrid, out grid)){ return null; }

            // Problem Sets
            while(e.MoveNext())
            {
                var rawState = e.Current;
                LawnMower mower;
                if(!LawnMower.TryParseLawnMower(rawState, out mower))
                { 
                    return null; 
                }

                e.MoveNext();

                var rawInstructions = e.Current;
                Instructions instructions;
                if (!Instructions.TryParseInstructions(rawInstructions, out instructions))
                {
                    return null;
                }

                var problemSet = new ProblemSet(mower, instructions);
                problemSets.Add(problemSet);
            }

            return Tuple.Create<Grid,IEnumerable<ProblemSet>>(grid, problemSets);
        }
    }
}

