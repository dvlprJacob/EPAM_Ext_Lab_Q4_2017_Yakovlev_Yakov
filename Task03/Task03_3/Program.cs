using System;

namespace Task03_3
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            int N = 1;
            try
            {
                while (N > 0)
                {
                    Console.Write("Input array length, 0 or less for exit ___");
                    if (int.TryParse(Console.ReadLine(), out N))
                    {
                        if (N > 0)
                        {
                            var arr = new MyArray(N);
                            Console.WriteLine(arr);
                            Console.WriteLine("Sum of the non-negative array elements equals to {0}", arr.Non_negative_elements_sum());
                        }
                        else
                        {
                            Console.Write("Press any key for exit   ");
                            Console.ReadKey();
                            return;
                        }
                    }
                    else
                    {
                        Console.WriteLine("Wrong input, input positive integer value !");
                        N = 1;
                    }
                }
            }
            catch (Exception e)
            {
                Console.Write(e.Message + "\nInput any key for exit   ");
            }
        }
    }
}