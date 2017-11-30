using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task02_1
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                Console.Write("Input rectangle sides values for calculate area through a space\n");
                int a, b;
                Rectangle Rec;
                var sides = Console.ReadLine().Split(' ');

                if (int.TryParse(sides[0], out a) && int.TryParse(sides.Last(), out b))
                {
                    Rec = new Rectangle(a, b);
                    Console.WriteLine("Area of rectangle with sides {0} and {1} equals {2}", a, b, Rec.Area);
                    Console.Write("Try again ? Input 'Y' for repeat or other key for exit   ");
                    if (Console.ReadLine() == "Y")
                    {
                        Console.Clear();
                        Main(args);
                    }

                    return;
                }

                Console.WriteLine("Incorrect input, sides must be integer values");
                Console.Write("Try again ? Y/N   ");
                if (Console.ReadLine() == "Y")
                {
                    Main(args);
                }
                return;
            }
            catch (Exception e)

            {
                Console.WriteLine(e.Message + "\nPress any key for exit");

                Console.ReadKey();
            }
        }
    }
}