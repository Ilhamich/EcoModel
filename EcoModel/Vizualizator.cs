using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcoModel
{
    class Vizualizator
    {
        public static void Print(Cell aCell)
        {
            Console.OutputEncoding = Encoding.Unicode;

            if (aCell.Image == (char)CellsSymbols.DefaultImage)
            {
                Console.ForegroundColor = ConsoleColor.Blue;
            }
            else
            {
                if (aCell.Image == (char)CellsSymbols.ObstacleImage)
                {
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                }
                else
                {
                    if (aCell.Image == (char)CellsSymbols.PreyImage)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                    }
                }
            }
            
            Console.SetCursorPosition(aCell.X, aCell.Y);
            Console.Write(aCell.Image);
        }
    }
}
