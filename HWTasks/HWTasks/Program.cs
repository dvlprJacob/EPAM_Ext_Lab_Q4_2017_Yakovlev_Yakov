using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HTasks
{
    internal class Program
    {
        // Subtask1
        // Написать консольное приложение, которое проверяет принадлежность точки заштрихованной области.
        // Пользователь вводит координаты точки (x; y) и выбирает букву графика (a-к)
        // В консоли должно высветиться сообщение: «Точка [(x; y)] принадлежит фигуре [г]».

        // Методы определяют принадлежность точки фигуре под буквой
        public static bool GraphA(double x, double y)
        {
            if (x * x + y * y <= 1)
                return true;
            return false;
        }

        public static bool GraphB(double x, double y)
        {
            if (((x * x + y * y) <= 1) & ((x * x + y * y) >= 0.5))
                return true;
            return false;
        }

        public static bool GraphV(double x, double y)
        {
            if ((Math.Abs(x) - 1 <= 0) & (Math.Abs(y) - 1 <= 0))
                return true;
            return false;
        }

        public static bool GraphG(double x, double y)
        {
            if (Math.Abs(x) + Math.Abs(y) <= 1)
                return true;
            return false;
        }

        public static bool GraphD(double x, double y)
        {
            double x11 = 1;
            double y11 = -1;
            double x21 = x;
            double y21 = y - 1;
            double Determ1 = x11 * y21 - x21 * y11;

            double x12 = -0.5;
            double y12 = -1;
            double x22 = x - 0.5;
            double y22 = y;
            double Determ2 = x12 * y22 - x22 * y12;

            double x13 = -0.5;
            double y13 = 1;
            double x23 = x;
            double y23 = y + 1;
            double Determ3 = x13 * y23 - x23 * y13;

            double x1 = 0.5;
            double y1 = 1;
            double x2 = x + 0.5;
            double y2 = y;
            double Determ4 = x1 * y2 - x2 * y1;

            if (Math.Sign(Determ1) == Math.Sign(Determ2) && Math.Sign(Determ2) == Math.Sign(Determ3) && Math.Sign(Determ3) == Math.Sign(Determ4))
                return true;
            List<int> signs = new List<int>() { Math.Sign(Determ1), Math.Sign(Determ2), Math.Sign(Determ3), Math.Sign(Determ4) };
            if (signs.Where(s => s == 0).Count() == 1 || signs.Where(s => s == 0).Count() == 2)
                return true;
            return false;
        }

        public static bool GraphK(double x, double y)
        {
            if (x <= 1 && x >= -1)
            {
            }
            return false;
        }

        public static void PrintResult(double x, double y, bool belongs, string graph)
        {
            if (belongs)
                Console.WriteLine("Point[{0};{1}] belongs to graph {2}", x, y, graph);
            else
                Console.WriteLine("Point[{0};{1}] is not belongs to graph {2}", x, y, graph);
        }

        private static void Main(string[] args)
        {
            Console.Write("Enter the coordinates of the point and select a shape\nX = ");

            double x, y;
            while (!Double.TryParse(Console.ReadLine(), out x))
            {
                Console.Write("This is not a number, try again\nX = ");
            }
            Console.Write("Y = ");
            while (!Double.TryParse(Console.ReadLine(), out y))
            {
                Console.Write("This is not a number, try again\nY = ");
            }

            Console.WriteLine("Switch graph letter and input [A,B,V,G,D,K]\n ");
            string graph = Console.ReadLine();
            switch (graph)
            {
                case "A":
                    PrintResult(x, y, GraphA(x, y), graph);
                    break;

                case "B":
                    PrintResult(x, y, GraphB(x, y), graph);
                    break;

                case "V":
                    PrintResult(x, y, GraphV(x, y), graph);
                    break;

                case "G":
                    PrintResult(x, y, GraphG(x, y), graph);
                    break;

                case "D":
                    PrintResult(x, y, GraphD(x, y), graph);
                    break;

                case "K":
                    PrintResult(x, y, GraphK(x, y), graph);
                    break;
            }

            // Subtask2
            // Решение квадратного уравнения при наличии действительных корней с выводом промежуточных значений

            double h;
            Console.Write("Enter the parameter h to solve the quadratic equation\nh = ");
            while (!double.TryParse(Console.ReadLine(), out h))
            {
                Console.WriteLine("This is not a number, try again\nh = ");
            }

            double a = Math.Sqrt((Math.Abs(Math.Sin(8 * h)) + 17) / (Math.Pow((1 - Math.Sin(4 * h) * Math.Cos(h * h + 18)), 2)));
            double b = 1 - Math.Sqrt(3 / (3 + Math.Abs(Math.Tan(a * h * h) - Math.Sin(a * h))));
            double c = a * h * h * Math.Sin(b * h) + b * h * h * h * Math.Cos(a * h);
            double D = b * b - 4 * a * c;
            if (D < 0)
                Console.WriteLine("There are no real roots.\na = {0}\nb = {1}\nc = {2}\nDiscr = {3}\n", a, b, c, D);
            else
                Console.WriteLine("There are real roots.\na = {0}\nb = {1}\nc = {2}\nDiscr = {3}\nx1 = {4}   x2={5}", a, b, c, D, (-b + Math.Sqrt(D) / 2 * a), (-b - Math.Sqrt(D) / 2 * a) + "\n");
            Console.Write("Want to go out ?\nInput Y/N . . . ");
            graph = Console.ReadLine();
            if (graph == "N")
                Main(args);
        }
    }
}