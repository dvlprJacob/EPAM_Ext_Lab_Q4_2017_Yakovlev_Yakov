namespace Task06_3
{
    using System;

    internal class Program
    {
        private static string[] figures = new string[] { "Point", "Line", "Circle", "Round", "Ring", "Rectangle", "Square" };

        private static void PrintMenu()
        {
            Console.WriteLine("Input figure number for create random instance of a class, -1 for exit");
            for (int i = 0; i < figures.Length; i++)
            {
                Console.WriteLine("{0} - {1}", i + 1, figures[i]);
            }
        }

        private static void Parse(int n)
        {
            Random r = new Random();
            Console.WriteLine("\nResult :\n");
            switch (n)
            {
                case 1:
                    {
                        var x = r.Next(-100, 100);
                        var y = r.Next(-100, 100);
                        Point p = new Point(x, y);
                        Console.WriteLine("{0}", p);
                        break;
                    }

                case 2:
                    {
                        var x1 = r.Next(-100, 100);
                        var y1 = r.Next(-100, 100);
                        var x2 = r.Next(-100, 100);
                        var y2 = r.Next(-100, 100);
                        Line l = new Line(x1, y2, x2, y2);
                        Console.WriteLine("{0}", l);
                        break;
                    }

                case 3:
                    {
                        var x = r.Next(-100, 100);
                        var y = r.Next(-100, 100);
                        var rad = r.Next(1, 100);
                        Circle c = new Circle(new Point(x, y), rad);
                        Console.WriteLine("{0}", c);
                        break;
                    }

                case 4:
                    {
                        var x = r.Next(-100, 100);
                        var y = r.Next(-100, 100);
                        var rad = r.Next(1, 100);
                        Round round = new Round(new Point(x, y), rad);
                        Console.WriteLine("{0}", round);
                        break;
                    }

                case 5:
                    {
                        var x = r.Next(-100, 100);
                        var y = r.Next(-100, 100);
                        var inner = r.Next(1, 100);
                        var outer = r.Next(inner, inner + 100);
                        Ring ring = new Ring(outer, inner, new Point(x, y));
                        Console.WriteLine("{0}", ring);
                        break;
                    }

                case 6:
                    {
                        var width = r.Next(1, 100);
                        var heigth = r.Next(1, 100);
                        Rectangle rect = new Rectangle(width, heigth);
                        Console.WriteLine("{0}", rect);
                        break;
                    }

                case 7:
                    {
                        var side = r.Next(1, 100);
                        Square sq = new Square(side);
                        Console.WriteLine("{0}", sq);
                        break;
                    }

                default:
                    {
                        Console.WriteLine("Wrong input, try again");
                        break;
                    }
            }

            Console.Write("Press any key for continue   ");
            Console.ReadKey();
            Console.Clear();
        }

        private static void Main(string[] args)
        {
            int n = 0;
            while (n != -1)
            {
                PrintMenu();
                switch (int.TryParse(Console.ReadLine(), out n))
                {
                    case true:
                        if (n == -1)
                        {
                            break;
                        }

                        Parse(n);
                        break;

                    case false:
                        Console.Clear();
                        Console.WriteLine("Wrong input, try again");
                        break;
                }
            }

            Console.Write("Press any key for exit   ");
            Console.ReadKey();
        }
    }
}