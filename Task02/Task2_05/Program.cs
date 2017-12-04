using System;

namespace Task2_05
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Введите целое число, для которого нужно вычислить сумму чисел не превосходящих его и кратных 5 или 3");
            Console.Write("N = ");
            int n;
            if (int.TryParse(Console.ReadLine(), out n))
            {
                Console.WriteLine(new Subtask5(n));
            }
            else
            {
                Console.WriteLine("Вы ввели не целое число");
            }
            Console.Write("Press any key for exit   ");
            Console.ReadKey();
        }
    }
}