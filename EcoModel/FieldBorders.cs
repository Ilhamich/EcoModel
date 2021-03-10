using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcoModel
{
    class FieldBorders
    {
        public const byte QUANTITY_OF_SIDE = 4;
        private const byte PARALLEL_SIDE= 2;
        private const byte ANGLES_ON_ONE_SIDE = 2;
        private const byte ANGLES_ON_FIELD = 4;

        private int _step;
        private int _vertiSize;
        private int _horizSize;

        private Border[] _pointOfField;

        private Coordinate _lTopAngleCoord;
        private Coordinate _rDownAngleCoord;

        private char[] _angles =
        {
            (char)BordersSymbols.LeftTop,
            (char)BordersSymbols.RightTop,
            (char)BordersSymbols.LeftDown,
            (char)BordersSymbols.RightDown
        };

        public int FieldLenght
        {
            get
            {
                int length = 0;

                if (_pointOfField.Length != 0)
                {
                    length = _pointOfField.Length;
                }

                return length;
            }
        }

        public Coordinate LeftTopAngle
        {
            get
            {
                return _lTopAngleCoord;
            }
        }

        public Coordinate RightDownAngle
        {
            get
            {
                return _rDownAngleCoord;
            }
        }

        public FieldBorders(int verSize, int horSize, int xOfLeftTopAngle, int yOfLeftTopAngle,
                int step = 1)
        {
            _step = step;
            _vertiSize = verSize;
            _horizSize = horSize;
            _lTopAngleCoord.X = xOfLeftTopAngle;
            _lTopAngleCoord.Y = yOfLeftTopAngle;
            _rDownAngleCoord.X = _lTopAngleCoord.X + (_horizSize - 1) * _step;
            _rDownAngleCoord.Y = _lTopAngleCoord.Y + (_vertiSize - 1) * _step;

            BuildField(verSize, horSize);
        }

        private void BuildField(int verSize, int horSize)
        {
            _pointOfField = new Border[verSize * PARALLEL_SIDE
                    + horSize * PARALLEL_SIDE - ANGLES_ON_FIELD];

            int index = 0;
            int symbolNum = 0;

            for (int sideNumber = 0; sideNumber < QUANTITY_OF_SIDE; sideNumber++)
            {
                if ((sideNumber + 1) % 2 != 0)
                {
                    for (int count = 0; count < horSize; count++)
                    {
                        BildHorizontalLine(index, count, sideNumber, ref symbolNum);
                        index++;
                    }
                }
                else
                {
                    for (int count = 0; count < verSize - ANGLES_ON_ONE_SIDE; count++)
                    {
                        BildVerticallLine(index, count, sideNumber);
                        index++;
                    }
                }
            }
        }

        private void BildVerticallLine(int index, int count, int sideNumber)
        {
            int xPosition = 0;

            if (sideNumber == 1)
            {
                xPosition = _lTopAngleCoord.X;
            }
            else
            {
                xPosition = _rDownAngleCoord.X;
            }

            _pointOfField[index] = new Border
                   (xPosition, _lTopAngleCoord.Y + _step + (count * _step),
                   (char)BordersSymbols.Vertical);
        }

        private void BildHorizontalLine(int index, int count, int sideNumber,
                ref int symbol)
        {
            int yPosition = 0;

            if (sideNumber == 0)
            {
                yPosition = _lTopAngleCoord.Y;
            }
            else
            {
                yPosition = _rDownAngleCoord.Y;
            }

            if (count == 0)
            {
                _pointOfField[index] =
                        new Border
                        (_lTopAngleCoord.X + count * _step, yPosition,
                        _angles[symbol]);

                symbol++;
            }
            else
            {
                if (count == _horizSize - 1)
                {
                    _pointOfField[index] =
                           new Border
                           (_lTopAngleCoord.X + count * _step, yPosition,
                           _angles[symbol]);

                    symbol++;
                }
                else
                {
                    _pointOfField[index] =
                          new Border
                          (_lTopAngleCoord.X + count * _step, yPosition,
                          (char)BordersSymbols.Horizontal);
                }
            }
        }

        /// <summary>
        /// Return one of BorderElement by it's index
        /// </summary>
        /// <param name="index">index of BorderElement in all BorderElements</param>
        /// <returns>BorderElement</returns>
        public Border this[int index]
        {
            get
            {
                return _pointOfField[index];
            }
        }
    }
}
