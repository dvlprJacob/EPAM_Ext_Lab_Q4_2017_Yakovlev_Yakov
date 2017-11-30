using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task02_4
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            int N;
            try
            {
                while (true)
                {
                    Console.Write("Input row count for see image 2, 0 or less number for exit\n N = ");
                    if (int.TryParse(Console.ReadLine(), out N))
                    {
                        if (N > 0)
                        {
                            Subtask4 Tmp = new Subtask4(N);
                            Console.WriteLine(Tmp);
                        }
                        else
                        {
                            Console.Write("Press any key for exit  ");
                            Console.ReadKey();
                            return;
                        }
                    }
                    else
                    {
                        Console.WriteLine("Wrong input! Count must be integer value and more than 0, try again");
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message + "\nPress any key");

                Console.ReadKey();
                return;
            }
        }
    }
}