using System;
using System.Linq;

namespace LawnMowerLib
{
    public class ProblemSet
    {
        private LawnMower mower;

        public LawnMower Mower
        {
            get
            {
                return mower;
            }
        }

        private Instructions instructions;

        public Instructions Instructions
        {
            get
            {
                return instructions;
            }
        }
         
        public ProblemSet(LawnMower mower, Instructions instructions)
        {
            this.mower = mower;
            this.instructions = instructions;
        }
    }
}

