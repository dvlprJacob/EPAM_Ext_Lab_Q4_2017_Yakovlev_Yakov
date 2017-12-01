using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task02_6
{
    internal class Program
    {
        public static void Main(string[] args)//todo pn реализовать таким образом, чтобы нужно было вводить только 1-3. Ну, и ключ для выхода.
        {
            Subtask6 tmp = new Subtask6();
            Console.WriteLine(tmp);
            string commmand = "";
            Console.Write("Input accentuation and on/off for activate or deactivate or 0 for exit, 'c' for clear console,\n'print' for write on console\n   ");
            commmand = Console.ReadLine();

            while (commmand != "0")
            {
                var parsed = commmand.Split(' ');

                switch (parsed.Last())//todo pn не работает логика, даже если вводишь "on"
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
            Console.Write("Press any key for exit   ");
            Console.ReadKey();
        }
    }
}