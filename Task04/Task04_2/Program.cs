using System;
using System.Linq;

namespace Task04_2
{
    internal class Program
    {
        /// <summary>
        /// Возвращает значение первой строки first дублируя символы, содержащиеся во второй строке через редактируемый параметр
        /// </summary>
        /// <param name="first"> Первая строка</param>
        /// <param name="second"> Вторая строка</param>
        /// <param name="tmp"> Редактируемый параметр</param>
        public static void Dublicate(string first, string second, out string tmp)
        {
            tmp = string.Empty;
            foreach (var symbol in first)
            {
                switch (second.Contains(symbol))
                {
                    case true:
                        tmp += symbol.ToString() + symbol.ToString();
                        break;

                    case false:
                        tmp += symbol.ToString();
                        break;
                }
            }
        }

        private static void Main(string[] args)
        {
            string f = string.Empty;
            string s = string.Empty;
            bool status = true;

            while (status)
            {
                Console.WriteLine("Введите строку :");
                f = Console.ReadLine();
                Console.WriteLine("Введите строку, символы из которой нужно продублировать в первой строке :");
                s = Console.ReadLine();
                string res = string.Empty;
                Dublicate(f, s, out res);
                Console.WriteLine(res);
                Console.WriteLine("Если хотите повторить, введите Y");

                if (Console.ReadLine() != "Y")
                {
                    status = false;
                }
            }

            Console.WriteLine("Press any key for exit");
            Console.ReadKey();
        }
    }
}