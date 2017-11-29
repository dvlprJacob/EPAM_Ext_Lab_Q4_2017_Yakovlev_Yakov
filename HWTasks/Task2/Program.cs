using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task2
{
    internal class Program
    {
        public static void Subtask2_1()
        {
            Console.Clear();
            Console.Write("Input rectangle sides values for calculate area through a space\n");
            int a, b;
            Rectangle Rec;
            var sides = Console.ReadLine().Split(' ');

            if (int.TryParse(sides[0], out a) && int.TryParse(sides.Last(), out b))
            {
                Rec = new Rectangle(a, b);
                Console.WriteLine("Area of rectangle with sides {0} and {1} equals {2}", a, b, Rec.Area);
                return;
            }

            Console.WriteLine("Incorrect input, sides must be integer values");
            return;
        }

        public static void Subtask2_2()
        {
            Console.Clear();
            Console.Write("Input row count for see image 1\n N = ");
            int N;

            if (int.TryParse(Console.ReadLine(), out N))
            {
                Subtask2 Tmp = new Subtask2(N);
                Console.WriteLine(Tmp);
                return;
            }
            Console.WriteLine("Incorrect input");
            return;
        }

        public static void Subtask2_3()
        {
            Console.Clear();
            Console.Write("Input row count for see image 2\n N = ");
            int N;

            if (int.TryParse(Console.ReadLine(), out N))
            {
                Subtask3 Tmp = new Subtask3(N);
                Console.WriteLine(Tmp);
                return;
            }
            Console.WriteLine("Incorrect input");
            return;
        }

        public static void Subtask2_4()
        {
            Console.Clear();
            Console.Write("Input row count for see image 2\n N = ");
            int N;

            if (int.TryParse(Console.ReadLine(), out N))
            {
                Subtask4 Tmp = new Subtask4(N);
                Console.WriteLine(Tmp);
                return;
            }
            Console.WriteLine("Incorrect input");
            return;
        }

        public static void Subtask2_5()
        {
            Console.Clear();
            Console.WriteLine("Сумма чисел меньше 1000 и кратных 5 или 3 равна {0}", new Subtask5());
        }

        public static void Subtask2_6()
        {
            Subtask6 tmp = new Subtask6();
            Console.WriteLine(tmp);
            string commmand = "";
            Console.Write("Input accentuation and on/off for activate or deactivate or 0 for exit, 'c' for clear console,\n'print' for write on console\n   ");
            commmand = Console.ReadLine();

            while (commmand != "0")
            {
                var parsed = commmand.Split(' ');

                switch (parsed.Last())
                {
                    case "on":
                        if (tmp.IsExist(parsed.First()))
                        {
                            tmp.ActivateAccent(parsed.First());
                            Console.WriteLine(tmp);
                        }
                        else
                            Console.WriteLine("{0} accent is not exist", parsed.First());
                        break;

                    case "off":
                        if (tmp.IsExist(parsed.First()))
                        {
                            tmp.DeactivateAccent(parsed.First());
                            Console.WriteLine(tmp);
                        }
                        else
                            Console.WriteLine("{0} accent is not exist", parsed.First());
                        break;

                    case "print":
                        Console.WriteLine(tmp);
                        break;

                    case "c":
                        Console.Clear();
                        break;

                    default:
                        Console.WriteLine("Uncknown command");
                        break;
                }

                Console.Write("Input accentuation and on/off for activate or deactivate or 0 for exit, 'c' for clar console\n   ");
                commmand = Console.ReadLine();
            }
        }

        private static void Main(string[] args)
        {
            Console.Title = "Homework task 02";
            Console.Write("Swith subtask [ 1, 2, 3, 4, 5, 6 ], switch number and input\n  ");
            try
            {
                string subtask = Console.ReadLine();

                while (subtask != "0")
                {
                    switch (subtask)
                    {
                        case "1":
                            {
                                Subtask2_1();
                                break;
                            }
                        case "2":
                            {
                                Subtask2_2();
                                break;
                            }
                        case "3":
                            {
                                Subtask2_3();
                                break;
                            }
                        case "4":
                            {
                                Subtask2_4();
                                break;
                            }
                        case "5":
                            {
                                Subtask2_5();
                                break;
                            }
                        case "6":
                            {
                                Subtask2_6();
                                break;
                            }
                        case "0":
                            {
                                return;
                            }
                        case "c":
                            {
                                Console.Clear();
                                break;
                            }
                        default:
                            {
                                Console.WriteLine("Incorrect input, try again");
                                break;
                            }
                    }
                    Console.Write("Swith subtask [ 1, 2, 3, 4, 5, 6 ] and input number, 0 for exit, 'c' for clear console\n  ");
                    subtask = Console.ReadLine();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message + "\nPress any key for exit");
                Console.ReadKey();
            }
        }
    }
}