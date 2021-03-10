using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcoModel
{
    class UI
    {
        public static void PrintField(FieldBorders field)
        {
            Console.ForegroundColor = ConsoleColor.DarkBlue;

            for (int i = 0; i < field.FieldLenght; i++)
            {
                Console.SetCursorPosition(field[i].X, field[i].Y);
                Console.Write(field[i].Symbol);
            }

            Console.ResetColor();
        }
    }
}
