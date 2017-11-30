using System;

namespace Task02_2
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            string command = "";
            int N;

            while (command != "0")
            {
                Console.Write("Input row count for see image 1 or less number for exit\n N = ");
                command = Console.ReadLine();
                if (int.TryParse(command, out N))
                {
                    if (N > 0)
                    {
                        Subtask2 Tmp = new Subtask2(N);
                        Console.WriteLine(Tmp);
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
                    Console.WriteLine("Wrong input ! Input correct command");
                }
            }
        }
    }
}