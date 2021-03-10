using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcoModel
{
    class Cell
    {
        protected readonly Ocean _owner;    // агрегация
        protected Coordinate _offset;

        public char Image { get;private set; }

        public Cell(Ocean aOcean, Coordinate offset,
                char image = (char)CellsSymbols.DefaultImage)
        {
            _offset = offset;
            Image = image;           
            _owner  = aOcean;           
        }

        //Используя сравнение изображений ячеек можно найти
        //соседние ячейки, удовлетворяющие требованиям соседа, и вернуть случайную из них
        protected virtual Cell GetNeighborWithImage(char anImage)          
        {
            Cell[] neighbor = new Cell[Constants.NEIGHBORS_COUNT];

            int countNeighbor = 0;

            if (_owner.North(_offset).Image == anImage)
            {
                neighbor[countNeighbor] = _owner.North(_offset);
                countNeighbor++;
            }

            if (_owner.South(_offset).Image == anImage)
            {
                neighbor[countNeighbor] = _owner.South(_offset);
                countNeighbor++;
            }

            if (_owner.East(_offset).Image == anImage)
            {
                neighbor[countNeighbor] = _owner.East(_offset);
                countNeighbor++;
            }

            if (_owner.West(_offset).Image == anImage)
            {
                neighbor[countNeighbor] = _owner.West(_offset);
                countNeighbor++;
            }

            return neighbor[Ocean._rnd.Next(0, countNeighbor)];
        }

        public int X 
        {
            get
            {
                return _offset.X;
            }
        }

        public int Y
        {
            get
            {
                return _offset.Y;
            }
        }

        protected virtual Coordinate GetNeighborCoord(char symbolCell)
        {
            Cell sentNeighbor = GetNeighborWithImage(symbolCell);

            if (sentNeighbor == null)
            {
                sentNeighbor = this;  //Если нет подходящего соседа отправляю себя самого
            }

            return sentNeighbor._offset;
        }

        public Coordinate GetOffset()
        {
            return _offset;
        }

        public void SetOffset(Coordinate anOffset)
        {
            _offset = anOffset;
        }

        public virtual void Process()
        {
        }       
    }
}
