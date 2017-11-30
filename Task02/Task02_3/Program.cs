using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task02_3
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            int command = 1;
            try
            {
                Console.Write("Input row count for see image 2, 0 or less for exit\n N = ");
                switch (int.TryParse(Console.ReadLine(), out command))
                {
                    case true:
                        if (command > 0)
                        {
                            Subtask3 Tmp = new Subtask3(Convert.ToInt32(command));
                            Console.WriteLine(Tmp);
                            Main(args);
                            break;
                        }
                        else
                        {
                            Console.Write("Press any key for exit    ");
                            Console.ReadKey();
                            return;
                        }
                    case false:
                        Console.WriteLine("Wrong input, try again");
                        Main(args);
                        break;
                }
            }
            catch (Exception e)
            {
                Console.Write(e.Message + "Press any key for exit    ");
                Console.ReadKey();
                return;
            }
        }
    }
}