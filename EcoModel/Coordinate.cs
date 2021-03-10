using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcoModel
{
    struct Coordinate
    {
        int _x;
        int _y;

        public Coordinate GetCopy()
        {
            return (Coordinate)MemberwiseClone();
        }

        public Coordinate(int y, int x)
        {
            _x = x;
            _y = y;
        }

        public int X
        {
            get { return _x; }
            set { _x = value; }
        }

        public int Y
        {
            get { return _y; }
            set { _y = value; }
        }

        public static bool operator ==(Coordinate first, Coordinate second)
        {
            return first.X == second.X && first.Y == second.Y;
        }

        public static bool operator !=(Coordinate first, Coordinate second)
        {
            return !(first == second);
        }
    }
}
