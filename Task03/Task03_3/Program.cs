using System;

namespace Task03_3
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            int n = 1;
            try
            {
                while (n > 0)
                {
                    Console.Write("Input array length, 0 or less for exit ___");
                    if (int.TryParse(Console.ReadLine(), out n))
                    {
                        if (n > 0)
                        {
                            Console.WriteLine("Input the minimum possible value");
                            int min = 0, max = 0;
                            switch (int.TryParse(Console.ReadLine(), out min))
                            {
                                case true:
                                    Console.WriteLine("Input the maximum possible value");
                                    switch (int.TryParse(Console.ReadLine(), out max))
                                    {
                                        case true:
                                            if (min > max)
                                            {
                                                Console.WriteLine("Maximum possible value must be more than minimum");
                                                break;
                                            }

                                            var arr = new MyArray(n, min, max);
                                            Console.WriteLine(arr);
                                            Console.WriteLine("Sum of the non-negative array elements equals to {0}", arr.Non_negative_elements_sum());
                                            break;
                                    }

                                    break;
                            }
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
                        n = 1;
                    }
                }
            }
            catch (Exception e)
            {
                Console.Write(e.Message + "\nInput any key for exit   ");
                Console.ReadKey();
            }
        }
    }
}