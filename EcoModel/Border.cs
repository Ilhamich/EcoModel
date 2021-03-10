using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcoModel
{
    public struct Border
    {
        public int X { get; }

        public int Y { get; }

        public char Symbol { get; }

        public Border(int x, int y, char symbol)
        {
            X = x;
            Y = y;
            Symbol = symbol;
        }
    }
}
