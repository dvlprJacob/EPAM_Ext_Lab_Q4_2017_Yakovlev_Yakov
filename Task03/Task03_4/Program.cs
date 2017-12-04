using System;
using System.Linq;

namespace Task03_4
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            int n = 1;
            int m = 1;
            try
            {
                string inputString = string.Empty;
                while (inputString != "0")
                {
                    Console.Write("Input matrix row and column sizes [integer values more than 0, for example: [3 4] ]\nthrough a space, 0 for exit  ");
                    inputString = Console.ReadLine();
                    var parsed = inputString.Split(' ');
                    if (parsed.Length == 2)
                    {
                        switch (int.TryParse(parsed.First(), out n)
                        && int.TryParse(parsed.Last(), out m))
                        {
                            case true:
                                if (n > 0 && m > 0)
                                {
                                    var arr = new MyArray(n, m);
                                    Console.WriteLine(arr);
                                    Console.WriteLine("Sum of the elements from even positions equals to {0}", arr.Even_positions_elements_sum());
                                    break;
                                }
                                else
                                {
                                    Console.WriteLine("Incorrect input string, try again");
                                }

                                break;

                            case false:
                                Console.WriteLine("Incorrect input string, try again");
                                break;
                        }
                    }
                    else if (inputString != "0")
                    {
                        Console.WriteLine("Incorrect input string, try again");
                    }
                }

                Console.WriteLine("Input any key for exit   ");
                Console.ReadKey();
            }
            catch (Exception e)
            {
                Console.Write(e.Message + "\nInput any key for exit   ");
                Console.ReadKey();
            }
        }
    }
}