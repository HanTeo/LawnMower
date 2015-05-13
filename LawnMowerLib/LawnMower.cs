using System;
using System.Linq;
using System.Collections.Generic;

namespace LawnMowerLib
{
    public class LawnMower
    {
        public bool IsFinished { get; private set; }
        public char Bearing 
        { 
            get{ return bearing; } 
            set
            { 
                if (bearings.Contains(value))
                    bearing = value;
            } 
        }
        public int X { get; set; }
        public int Y { get; set; }

        private List<char> bearings = new List<char> {'N','E','S','W'};
        private char bearing;

        public void Go(Grid grid, Instructions instructions)
        {
            while (instructions.MoveNext())
            {
                switch (instructions.Current)
                {
                    case 'L':
                        TurnLeft();
                        break;
                    case 'R':
                        TurnRight();
                        break;
                    case 'M':
                        Move(grid);
                        break;
                }
            }

            IsFinished = true;

            Park(grid);
        }

        public void Park(Grid grid)
        {
            grid.Park(X, Y);
        }

        public void TurnRight()
        {
            var i = bearings.IndexOf(Bearing);

            if (i + 1 == bearings.Count)
            {
                i = 0;
            }
            else
            {
                i++;
            }

            Bearing = bearings[i];
        }

        public void TurnLeft()
        {
            var i = bearings.IndexOf(Bearing);

            if (i == 0)
            {
                i = bearings.Count-1;
            }
            else
            {
                i--;
            }

            Bearing = bearings[i];
        }

        public void Move(Grid grid)
        {
            switch (Bearing)
            {
                case 'N':
                    if (!grid.IsOccupiable(X, Y+1))
                        break;
                    Y = Y + 1; 
                    break;

                case 'E':
                    if (!grid.IsOccupiable(X+1, Y))
                        break;
                    X = X + 1;
                    break;

                case 'S':
                    if (!grid.IsOccupiable(X, Y-1))
                        break;
                    Y = Y - 1;
                    break;

                case 'W':
                    if (!grid.IsOccupiable(X-1, Y))
                        break;
                    X = X - 1;
                    break;
            }
        }

        public override string ToString()
        {
            return string.Format("{0} {1} {2}", X, Y, Bearing);
        }

        public static bool TryParseLawnMower(string rawState, out LawnMower mower)
        {
            var tokens = rawState.Split(new[]{ ' ' });

            // 3 Tokens
            if (tokens.Length != 3) 
            {
                mower = null;
                return false;
            }

            // First Two Tokens Are Numbers
            foreach(var i in new[]{0,1})
            {
                foreach (var c in tokens[i])
                {
                    if (!char.IsDigit(c))
                    {
                        mower = null;
                        return false;
                    }
                }
            };

            // Third token is single digit pole e.g N,S,E,W
            if(tokens[2].Length > 1)
            {
                mower = null;
                return false;
            }
            var pole = tokens[2][0];
            if (!"NSEW".Contains(pole))
            {
                mower = null;
                return false;
            }

            // Initialise the mower
            mower = new LawnMower
                { 
                    X = int.Parse(tokens[0]),
                    Y = int.Parse(tokens[1]),
                    Bearing = pole
                };

            return true;
        }
    }
}