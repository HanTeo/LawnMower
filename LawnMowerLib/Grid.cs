using System;
using System.Collections.Generic;

namespace LawnMowerLib
{
    public class Grid
    {
        public int Top{ get; set;}
        public int Right { get; set; }
        public int Left { get; private set; }
        public int Bottom { get; private set; }

        private HashSet<string> occupiedSquares;

        public Grid()
        {
            occupiedSquares = new HashSet<string>();
        }

        private string makeKey(int x, int y)
        {
            return string.Format("{0}{1}", x, y);
        }

        public void Park(int x, int y)
        {
            occupiedSquares.Add(makeKey(x, y));
        }

        public bool IsOccupiable(int x, int y)
        {
            // Boundaries
            if (x > Right)
                return false;

            if (x < Left)
                return false;

            if (y > Top)
                return false;

            if (y < Bottom)
                return false;

            // is unoccupied
            var key = makeKey(x, y);
            var isFree = !occupiedSquares.Contains(key);

            return isFree;
        }

        public static bool TryParse(string input, out Grid grid)
        {
            // Grid input are two numbers separated by a space
            var tokens = input.Split(' ');

            // Two numbers only
            if (tokens.Length != 2)
            {
                grid = null;
                return false;
            }

            // Tokens Must Be Numbers
            foreach (var token in tokens)
            {
                foreach (var c in token)
                {
                    if (!char.IsDigit(c))
                    {
                        grid = null;
                        return false;
                    }
                }
            }

            // Top and Right Values Assigned
            grid = new Grid
            {
                    Top = int.Parse(tokens[0]),
                    Right = int.Parse(tokens[1])
            };

            return true;
        }
    }
}

