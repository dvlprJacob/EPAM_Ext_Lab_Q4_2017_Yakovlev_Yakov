﻿namespace Task04_3
{
    using System;
    using System.Text;
    using System.Diagnostics;

    internal class Program
    {
        /// <summary>
        /// Складываемый символ
        /// </summary>
        private static char term = '*';

        /// <summary>
        /// Число итераций операции сложения, необходим для более точного вычисления среднего времени
        /// прибавления n числа символов к объекту
        /// </summary>
        private static int iterations_count = 3;

        /// <summary>
        /// Вычисляет среднее врямя операции прибавления n числа символов к объекту типа string
        /// </summary>
        /// <param name="n"> Число символов</param>
        /// <returns> Среднее время в миллисекундах</returns>
        public static long String_average_addition_time(int n)
        {
            var temp = string.Empty;
            Stopwatch sw = new Stopwatch();
            for (int i = 0; i < iterations_count; i++)
            {
                sw.Start();
                for (int j = 0; j < n; j++)
                {
                    temp += term;
                }

                sw.Stop();
                temp = string.Empty;
            }

            return sw.ElapsedMilliseconds / iterations_count;
        }

        /// <summary>
        /// Вычисляет среднее врямя операции прибавления n числа символов к объекту типа StringBuilder
        /// </summary>
        /// <param name="n"> Число символов</param>
        /// <returns> Среднее время в миллисекундах</returns>
        public static long StringBuilder_average_addition_time(int n)
        {
            StringBuilder temp = new StringBuilder();
            Stopwatch sw = new Stopwatch();
            for (int i = 0; i < iterations_count; i++)
            {
                sw.Start();
                for (int j = 0; j < n; j++)
                {
                    temp.Append(term);
                }

                sw.Stop();
                temp.Clear();
            }

            return sw.Elapsed.Milliseconds / iterations_count;
        }

        private static void Main(string[] args)
        {
            int n = -1;
            while (n != 0)
            {
                Console.Write("Введите число больше нуля ( лучше не меньше 100000 ), чтобы произвести сравнительный анализ, 0 чтобы выйти\nN = ");
                switch (int.TryParse(Console.ReadLine(), out n))
                {
                    case true:
                        switch (n <= 0)
                        {
                            case true:
                                if (n == 0)
                                {
                                    break;
                                }

                                Console.WriteLine("N должен быть больше нуля !");

                                break;

                            case false:
                                var string_time = String_average_addition_time(n);
                                var sb_time = StringBuilder_average_addition_time(n);
                                Console.WriteLine("Среднее время выполнения операции сложения переменной типа string : temp+='*' {0} раз составляет {1} миллисекунд", n, string_time);
                                Console.WriteLine("Среднее время выполнения операции сложения объектом типа StringBuilder : temp.Append('*') {0} раз составляет {1} миллисекунд", n, sb_time);

                                break;
                        }

                        break;

                    case false:
                        Console.WriteLine("Неверный ввод, попробуйте заново");
                        n = 1;
                        break;
                }
            }

            Console.WriteLine("Press any key for exit");
            Console.ReadKey();
        }
    }
}