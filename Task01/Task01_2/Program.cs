using System;

namespace Task01_2
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            //// Subtask2
            //// Решение квадратного уравнения при наличии действительных корней с выводом промежуточных значений

            double h;
            Console.Write("Enter the parameter h to solve the quadratic equation\nh = ");
            while (!double.TryParse(Console.ReadLine(), out h))
            {
                Console.WriteLine("This is not a number, try again\nh = ");
            }

            double a = Math.Sqrt(Math.Abs(Math.Sin(8 * h)) + (17 / Math.Pow(1 - (Math.Sin(4 * h) * Math.Cos((h * h) + 18)), 2)));
            double b = 1 - Math.Sqrt(3 / (3 + Math.Abs(Math.Tan(a * h * h)) - Math.Sin(a * h)));
            double c = (a * h * h * Math.Sin(b * h)) + (b * h * h * h * Math.Cos(a * h));
            double discr = (b * b) - (4 * a * c);
            if (discr < 0)
            {
                Console.WriteLine("There are no real roots.\na = {0}\nb = {1}\nc = {2}\nDiscr = {3}\n", a, b, c, discr);
            }
            else
            {
                Console.WriteLine("There are real roots.\na = {0}\nb = {1}\nc = {2}\nDiscr = {3}\nx1 = {4}   x2={5}", a, b, c, discr, ((-b) + Math.Sqrt(discr)) / (2 * a), ((-b) - Math.Sqrt(discr)) / (2 * a) + "\n");
            }

            Console.Write("Want to go out ?\nInput Y/N . . . ");
            string graph = Console.ReadLine();
            if (graph == "N")
            {
                Main(args);
            }
        }
    }
}