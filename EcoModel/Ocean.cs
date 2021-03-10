using System;
using System.Collections;
using System.Threading;

namespace EcoModel
{
    class Ocean : IOcean, IEnumerable
    {
        public static Random _rnd = new Random();
        private Cell[,] _cells;    // композиция
        private Coordinate _startPos;

        public int NumPrey { get; set; }

        public int NumPredators { get; set; }

        public int NumObstacles { get; set; }

        public Ocean(Coordinate startPos = new Coordinate(),
                int numRows = Constants.MAX_ROWS,
                int numCols = Constants.MAX_COLS,
                int numPrey = Constants.DEFAULT_NUM_PREY,
                int numPredators = Constants.DEFAULT_NUM_PREDATORS,
                int numObstracles = Constants.DEFAULT_NUM_OBSTACLES)
        {
            _startPos = startPos;

            InitCells(numRows, numCols);
            AddObstacles(numObstracles);
            AddPray(numPrey);
            AddPredators(numPredators);
            AddEmptyCells();

            NumPrey = numPrey;
            NumPredators = numPredators;
            NumObstacles = numObstracles;
        }

        public int NumRows
        {
            get
            {
                int numRows = 0;

                if (_cells != null)
                {
                    numRows = _cells.GetLength(0);
                }

                return numRows;
            }
        }

        public int NumCols
        {
            get
            {
                int numCols = 0;

                if (_cells != null)
                {
                    numCols = _cells.GetLength(1);
                }

                return numCols;
            }
        }

        public int Size
        {
            get
            {
                return (int)_cells.LongLength;
            }
        }

        public void InitCells(int numRows, int numCols)
        {
            _cells = new Cell[numRows, numCols];
        }

        public void AddEmptyCells()
        {
            for (int y = _startPos.Y; y < _startPos.Y +  _cells.GetLength(0); y++)
            {
                for (int x = _startPos.X; x < _startPos.X + _cells.GetLength(1); x++)
                {
                    if (_cells[y - _startPos.Y, x - _startPos.X] == null)
                    {
                        _cells[y - _startPos.Y, x - _startPos.X] = new Cell(this, new Coordinate(y, x));
                    }
                }
            }
        }

        public void AddObstacles(int numObstracles)
        {
            int y = 0;
            int x = 0;
            int countObstracles = 0;

            do
            {
                y = _rnd.Next(_startPos.Y, _startPos.Y + NumRows);
                x = _rnd.Next(_startPos.X, _startPos.X + NumCols);

                if (_cells[y - _startPos.Y, x - _startPos.X] == null)
                {
                    _cells[y - _startPos.Y, x - _startPos.X] = new Obstacle(this, new Coordinate(y, x));
                    countObstracles++;
                }
                else
                {
                    if (_cells[y - _startPos.Y, x - _startPos.X].Image == (char)CellsSymbols.DefaultImage)
                    {
                        _cells[y - _startPos.Y, x - _startPos.X] = new Obstacle(this, new Coordinate(y, x));
                        countObstracles++;
                    }
                }
            } while (countObstracles < numObstracles);
        }

        public void AddPredators(int numPredators)
        {
            int x = 0;
            int y = 0;
            int countPredators = 0;

            do
            {
                y = _rnd.Next(_startPos.Y, _startPos.Y + NumRows);
                x = _rnd.Next(_startPos.X, _startPos.X + NumCols);

                if (_cells[y - _startPos.Y, x - _startPos.X] == null)
                {
                    _cells[y - _startPos.Y, x - _startPos.X] = new Predator(this, new Coordinate(y, x),false);
                    countPredators++;
                }
                else
                {
                    if (_cells[y - _startPos.Y, x - _startPos.X].Image == (char)CellsSymbols.DefaultImage)
                    {
                        _cells[y - _startPos.Y, x - _startPos.X] = new Predator(this, new Coordinate(y, x), false);
                        countPredators++;
                    }
                }
            } while (countPredators < numPredators);
        }

        public void AddPray(int numPrey)
        {
            int x = 0;
            int y = 0;
            int countPrey = 0;

            do
            {
                y = _rnd.Next(_startPos.Y, _startPos.Y + NumRows);
                x = _rnd.Next(_startPos.X, _startPos.X + NumCols);

                if (_cells[y - _startPos.Y, x - _startPos.X] == null)
                {
                    _cells[y - _startPos.Y, x - _startPos.X] = new Prey(this, new Coordinate(y, x), false);
                    countPrey++;
                }
                else
                {
                    if (_cells[y - _startPos.Y, x - _startPos.X].Image == (char)CellsSymbols.DefaultImage)
                    {
                        _cells[y - _startPos.Y, x - _startPos.X] = new Prey(this, new Coordinate(y, x), false);
                        countPrey++;
                    }
                }
            } while (countPrey < numPrey);
        }

        public Coordinate GetEmptyCellCoord()
        {
            return new Coordinate();
        }

        public void Run(int distance)
        {
            for (int i = 0; i < distance; i++)
            {
                foreach (Cell item in this)
                {
                    Vizualizator.Print(item);
                }

                Thread.Sleep(40);

                foreach (Cell item in this)
                {
                    item.Process();

                    if (NumRows + _startPos.Y == item.Y + 1 && NumCols + _startPos.X == item.X + 1)
                    {
                        SetEndOfIteration();
                    }
                }              
            }
        }

        private void SetEndOfIteration()
        {
            foreach (Cell item in _cells)
            {
                if (item.Image == (char)CellsSymbols.PreyImage)
                {
                    ((Prey)item).FixEndOfIteration();
                }
                else
                {
                    if (item.Image == (char)CellsSymbols.PredatorImage)
                    {
                        ((Predator)item).FixEndOfIteration();
                    }
                }
            }
        }

        public Cell North(Coordinate aCoord)
        {
            int tmpCoord = 0;

            if (aCoord.Y - 1 >= _startPos.Y)
            {
                tmpCoord = aCoord.Y - 1;
            }
            else
            {
                tmpCoord = aCoord.Y;
            }

            return _cells[tmpCoord - _startPos.Y, aCoord.X - _startPos.X];
        }

        public Cell South(Coordinate aCoord)
        {
            int tmpCoord = 0;

            if (aCoord.Y + 1 <= _cells.GetLength(0) - 1 + _startPos.Y)
            {
                tmpCoord = aCoord.Y + 1;
            }
            else
            {
                tmpCoord = aCoord.Y;
            }

            return _cells[tmpCoord - _startPos.Y, aCoord.X - _startPos.X];
        }

        public Cell East(Coordinate aCoord)
        {
            int tmpCoord = 0;

            if (aCoord.X + 1 <= _cells.GetLength(1) - 1 + _startPos.X)
            {
                tmpCoord = aCoord.X + 1;
            }
            else
            {
                tmpCoord = aCoord.X;
            }

            return _cells[aCoord.Y - _startPos.Y, tmpCoord - _startPos.X];
        }

        public Cell West(Coordinate aCoord)
        {
            int tmpCoord = 0;

            if (aCoord.X - 1 >= _startPos.X)
            {
                tmpCoord = aCoord.X - 1;
            }
            else
            {
                tmpCoord = aCoord.X;
            }

            return _cells[aCoord.Y - _startPos.Y, tmpCoord - _startPos.X];
        }

        public void MovePrey(Coordinate from, Coordinate to, int reproduce)
        {
          if (reproduce > 0)
            {
                _cells[to.Y - _startPos.Y, to.X - _startPos.X] = new Prey(this, to, true, reproduce);
                _cells[from.Y - _startPos.Y, from.X - _startPos.X] = new Cell(this, from);
            }
            else
            {
                _cells[to.Y - _startPos.Y, to.X - _startPos.X] = new Prey(this, to, true);
                _cells[from.Y - _startPos.Y, from.X - _startPos.X] = new Prey(this, from, false);

                NumPrey++;
            }
        }

        public void MovePredator(Coordinate from, Coordinate to,
                int reproduce, int timeToFeed, bool eaten)
        {
            if (timeToFeed > 0)
            {
                if (eaten)
                {
                    timeToFeed = Constants.DEFAULT_TIME_TO_FEED;
                }
                
                if (reproduce > 0)
                {
                    _cells[to.Y - _startPos.Y, to.X - _startPos.X] = new Predator(this, to, true, reproduce, timeToFeed);
                    _cells[from.Y - _startPos.Y, from.X - _startPos.X] = new Cell(this, from);
                }
                else
                {
                    _cells[to.Y - _startPos.Y, to.X - _startPos.X] = new Predator(this, to, true,
                           Constants.DEFAULT_TIME_TO_REPRODUCE, timeToFeed);
                    _cells[from.Y - _startPos.Y, from.X - _startPos.X] = new Predator(this, from, false);

                    NumPredators++;
                }
            }
            else
            {
                _cells[from.Y - _startPos.Y, from.X - _startPos.X] = new Cell(this, from);

                NumPredators--;
            }
        }

        public IEnumerator GetEnumerator()
        {
            return new CellsNumerator(this);
        }

        struct CellsNumerator : IEnumerator
        {
            private readonly Ocean _owner;
            int _cellsX;
            int _cellsY;

            public CellsNumerator(Ocean owner)
            {
                _owner = owner;
                _cellsX = -1;
                _cellsY = -1;
            }

            public object Current
            {
                get
                {
                    return _owner._cells[_cellsY, _cellsX];
                }
            }

            public bool MoveNext()
            {
                if (_cellsY == -1)
                {
                    _cellsY++;
                    _cellsX++;
                }
                else
                {
                    if (_cellsX + 1 >= _owner._cells.GetLength(1)
                            && _cellsY + 1 < _owner._cells.GetLength(0))
                    {
                        _cellsY++;
                        _cellsX = 0;
                    }
                    else
                    {
                        _cellsX++;
                    }
                }

                return (_cellsX + 1) * (_cellsY + 1) <= _owner._cells.LongLength;
            }

            public void Reset()
            {
                _cellsX = -1;
                _cellsY = -1;
            }
        }
    }
}
