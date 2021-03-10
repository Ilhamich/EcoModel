using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcoModel
{
    interface IOcean
    {
        void InitCells(int numRows, int numCols);

        void AddEmptyCells();

        void AddObstacles(int numObstracles);

        void AddPredators(int numPredators);

        void AddPray(int numPrey);

        Coordinate GetEmptyCellCoord();

        void Run(int distance);      
    }
}
