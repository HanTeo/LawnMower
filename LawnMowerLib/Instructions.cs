using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;

namespace LawnMowerLib
{
    public class Instructions : IEnumerator<char>
    {
        private char[] inst;
        private int pos;
        private char curChar;

        public int Length { get { return inst.Length; } }

        public Instructions(char[] instructions)
        {
            inst = instructions;
            pos = -1;
            curChar = default(char);
        }

        public bool MoveNext()
        {
            if (++pos >= inst.Length)
            {
                return false;
            }
            else
            {
                curChar = inst[pos];
            }

            return true;
        }

        public void Reset()
        {
            pos = -1;
            curChar = default(char);
        }

        public char Current
        {
            get
            {
                return curChar;
            }
        }

        object IEnumerator.Current
        {
            get { return Current; }
        }

        void IDisposable.Dispose()
        {
            
        }

        public override string ToString()
        {
            return string.Join<char>("", inst);
        }

        public static bool TryParseInstructions(string rawInstructions, out Instructions instructions)
        {
            var tokens = rawInstructions.ToCharArray();

            // All instructions must be L,R,M
            foreach (var i in tokens)
            {
                if (!"LRM".Contains(i))
                {
                    instructions = null;
                    return false;
                }
            }

            instructions = new Instructions(tokens);

            return true;
        }
    }
}

