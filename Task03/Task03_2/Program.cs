using System;
using System.Text;

namespace Task03_2
{
    internal class Program
    {
        /// <summary>
        /// Выводит на консоль трехмернный массив
        /// </summary>
        /// <param name="array"></param>
        public static void Print_array_3d(int[,,] array)
        {
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < array.GetLength(0); i++)
            {
                builder.Append(string.Format("Matrix[{0}] :\n", i));
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    for (int k = 0; k < array.GetLength(2); k++)
                    {
                        builder.Append(string.Format("{0,-6}  ", array[i, j, k]));
                    }

                    builder.Append("\n");
                }
            }

            Console.WriteLine(builder.ToString());
        }

        /// <summary>
        /// Инициализирует новый трехмерный массив случайными целочисленними значениями из отрезка [minValue,maxValue]
        /// </summary>
        /// <param name="r"> Число элементов в первом измерении</param>
        /// <param name="с"> Число элементов во втором измерении</param>
        /// <param name="p"> Число элементов в третем измерении</param>
        /// <param name="minValue"> Минимальное возможное значение элемента</param>
        /// <param name="maxValue"> Максимально возможное значение элемента</param>
        /// <returns></returns>
        public static int[,,] Create_array_with_random_values(int r, int c, int p, int minValue = -100, int maxValue = 100)
        {
            int[,,] array3d = new int[r, c, p];
            Random rand = new Random();
            for (int i = 0; i < r; i++)
            {
                for (int j = 0; j < c; j++)
                {
                    for (int k = 0; k < p; k++)
                    {
                        array3d[i, j, k] = rand.Next(minValue, maxValue);
                    }
                }
            }

            return array3d;
        }

        /// <summary>
        /// Заменяет все положительные элементы переданного в качестве параметра трехмерного массива на нули
        /// </summary>
        /// <param name="array3d"></param>
        public static void Replace_pozitive_elements_by_zero(int[,,] array3d)
        {
            for (int i = 0; i < array3d.GetLength(0); i++)
            {
                for (int j = 0; j < array3d.GetLength(1); j++)
                {
                    for (int k = 0; k < array3d.GetLength(2); k++)
                    {
                        if (array3d[i, j, k] > 0)
                        {
                            array3d[i, j, k] = 0;
                        }
                    }
                }
            }
        }

        private static void Main(string[] args)
        {
            try
            {
                string command = " ";
                while (command != "0")
                {
                    int[] sizes = new int[] { 0, 0, 0 };
                    Console.WriteLine("Введите размерности трехмерного массива,минимально и максимально возможное значение, 0 чтобы выйти");
                    bool eventStatus = true;
                    for (int i = 0; i < 3; i++)
                    {
                        Console.Write("Размер массива в {0} измерении : ", i + 1);
                        command = Console.ReadLine();
                        switch (command)
                        {
                            case "0":
                                i = 3;
                                eventStatus = false;
                                break;

                            default:
                                int.TryParse(command, out sizes[i]);
                                if (sizes[i] <= 0)
                                {
                                    Console.WriteLine("Некорректный ввод, размерность должна быть больше нуля, попробуйте заново");
                                    i = 3;
                                    eventStatus = false;
                                }

                                break;
                        }
                    }

                    if (eventStatus != false)
                    {
                        int min = 0, max = 0;
                        Console.Write("Введите максимально и минимально возможное значение через пробел : ");
                        command = Console.ReadLine();
                        if (command == "0")
                        {
                            break;
                        }

                        var valueInterval = command.Split(' ');
                        switch (valueInterval.Length == 2
                            && int.TryParse(valueInterval[0], out min)
                            && int.TryParse(valueInterval[1], out max))
                        {
                            case true:
                                if (min > max)
                                {
                                    Console.WriteLine("Максимально возможное значение должно быть больше или равно минимально возможному, попробуйте заново");
                                    break;
                                }

                                int[,,] array_3d = Create_array_with_random_values(sizes[0], sizes[1], sizes[2], min, max);
                                Console.WriteLine("Initial 3D array [{0},{1},{2}] :", sizes[0], sizes[1], sizes[2]);
                                Print_array_3d(array_3d);
                                Replace_pozitive_elements_by_zero(array_3d);
                                Console.WriteLine("3D array after replacing all positive numbers by zero :");
                                Print_array_3d(array_3d);
                                break;

                            case false:
                                Console.WriteLine("Введенная строка имела неверный формат, попробуйте заново");
                                break;
                        }
                    }
                }

                Console.Write("Press any key for exit   ");
                Console.ReadKey();
                return;
            }
            catch (Exception e)
            {
                Console.Write(e.Message + "Input any key for exit");

                Console.ReadKey();
                return;
            }
        }
    }
}