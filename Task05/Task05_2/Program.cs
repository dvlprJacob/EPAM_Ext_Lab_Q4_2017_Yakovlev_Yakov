namespace Task05_2
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
                    Console.Write("---------------------------------------------------------------------\nInput number for generate random triangles, 0 for Exit\nN = ");

                    command = Console.ReadLine();
                    switch (command)
                    {
                        case Exit:
                            Console.WriteLine("Press any key for Exit");
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
                                Triangle[] triangles = new Triangle[n];
                                Console.WriteLine("Generated triangles :");
                                for (int i = 0; i < n; i++)
                                {
                                    int a = rand.Next(1, 100);
                                    int b = rand.Next(1, 100);
                                    int c = rand.Next(1, 100);
                                    triangles[i] = new Triangle(a, b, c);
                                    Console.WriteLine("{0}, area equals to {1:0.##}, perimeter equals to {2:0.##}", triangles[i], triangles[i].Area, triangles[i].Perimeter);
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