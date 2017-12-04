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
                string command = "";
                while (command != "0")
                {
<<<<<<< HEAD
                    Console.Write("Input rectangle width and height values through a space for calculate area of rectangle, input 0 for exit\n");
                    command = Console.ReadLine();
                    int a = 0;
                    int b = 0;
                    switch (int.TryParse(command.Split(' ')[0], out a) && int.TryParse(command.Split(' ').Last(), out b) && command.Split(' ').Length == 2)
=======
                    Rec = new Rectangle(a, b);
                    Console.WriteLine("Area of rectangle with sides {0} and {1} equals {2}", a, b, Rec.Area);//todo pn не понятно, что нужно пробелом разделять
                    Console.Write("Try again ? Input 'Y' for repeat or other key for exit   ");
                    if (Console.ReadLine() == "Y")//todo pn хардкод
>>>>>>> deba0da69d17d4647861bb597c490fe854665633
                    {
                        case true:
                            Rectangle Rec = new Rectangle(a, b);
                            Console.WriteLine("Area of rectangle with sides {0} and {1} equals {2}", a, b, Rec.Area);
                            break;

<<<<<<< HEAD
                        case false:
                            if (command != "0")
                            {
                                Console.WriteLine("Incorrect input, try again or exit.");
                            }
                            break;
                    }
=======
                Console.WriteLine("Incorrect input, sides must be integer values");
                Console.Write("Try again ? Y/N   ");
                if (Console.ReadLine() == "Y")//todo pn хардкод
				{
                    Main(args);
>>>>>>> deba0da69d17d4647861bb597c490fe854665633
                }
                Console.WriteLine("Press any key for exit");
                Console.ReadKey();
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