using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcoModel
{
    class Program
    {
        static void Main(string[] args)
        {

            FieldBorders field = new FieldBorders(Constants.MAX_ROWS + 2,
                    Constants.MAX_COLS + 2, 0, 0);

            UI.PrintField(field);

            Ocean aOcean = new Ocean(new Coordinate(field.LeftTopAngle.X + 1,
                    field.LeftTopAngle.Y + 1)/*,5, 15, 3, 3, 3*/);

            aOcean.Run(1000);

            Console.ReadKey();
        }     
    }
}
