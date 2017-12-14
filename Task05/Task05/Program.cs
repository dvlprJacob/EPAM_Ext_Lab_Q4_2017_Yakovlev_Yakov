namespace Task05
{
    using System;

    internal class Program
    {
        private static void Main(string[] args)
        {
            int n;
            const string Exit = "0";
            try
            {
                string command = string.Empty;
                while (command != Exit)
                {
                    Console.Write("---------------------------------------------------------------------"
                        + "\nInput number of circles for generate random rounds, 0 for exit\nN = ");

                    command = Console.ReadLine();
                    switch (command)
                    {
                        case Exit:

                            Console.WriteLine("Press any key for exit");
                            Console.ReadKey();
                            return;

                        default:
                            if (!int.TryParse(command, out n))
                            {
                                Console.WriteLine("Incorrect input");
                                break;
                            }

                            if (n > 0)
                            {
                                Random rand = new Random();
                                Round[] rounds = new Round[n];
                                Console.WriteLine("Generated circles :");
                                for (int i = 0; i < n; i++)
                                {
                                    int x = rand.Next(-30, 30);
                                    int y = rand.Next(-30, 30);
                                    int radius = rand.Next(1, 50);
                                    rounds[i] = new Round(new Point(x, y), radius);
                                    Console.WriteLine(rounds[i]);
                                }

                                int xs, ys;
                                Console.Write("Enter the coordinates of the point to check for intersection with circles\nX = ");
                                switch (int.TryParse(Console.ReadLine(), out xs))
                                {
                                    case true:
                                        Console.Write("Y = ");
                                        switch (int.TryParse(Console.ReadLine(), out ys))
                                        {
                                            case true:
                                                Point a = new Point(xs, ys);
                                                Console.WriteLine(a);
                                                foreach (var round in rounds)
                                                {
                                                    if (round.Crosses(a))
                                                    {
                                                        Console.WriteLine("{0,-13} intersect round with center on {1} and radius equals to {2}", string.Empty, round.Centre, round.Radius);
                                                    }
                                                    else
                                                    {
                                                        Console.WriteLine("{0,-13} does not intersect round with center on {1} and radius equals to {2}", string.Empty, round.Centre, round.Radius);
                                                    }
                                                }

                                                break;

                                            case false:
                                                Console.WriteLine("Incorrect input");
                                                break;
                                        }

                                        break;

                                    case false:
                                        Console.WriteLine("Incorrect input");
                                        break;
                                }

                                break;
                            }

                            Console.WriteLine("Incorrect number, count must be more than zero");
                            break;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message + "Press any key for exit");
                Console.ReadKey();
                return;
            }
        }
    }
}