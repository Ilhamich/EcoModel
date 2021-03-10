using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcoModel
{
    class Obstacle : Cell
    {
        public Obstacle(Ocean owner, Coordinate offset)
            :base(owner, offset, (char)CellsSymbols.ObstacleImage)
        {
        }
    }
}
