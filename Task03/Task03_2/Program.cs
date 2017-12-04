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
                        builder.Append(String.Format("{0,-6}  ", array[i, j, k]));
                    }
                    builder.Append("\n");
                }
            }

            Console.WriteLine(builder.ToString());
        }

        /// <summary>
        /// Инициализирует новый трехмерный массив случайными целочисленними значениями
        /// </summary>
        /// <param name="r"> Число элементов в первом измерении</param>
        /// <param name="с"> Число элементов во втором измерении</param>
        /// <param name="p"> Число элементов в третем измерении</param>
        /// <returns></returns>
        public static int[,,] Create_array_with_random_values(int r, int c, int p)
        {
            int[,,] array3d = new int[r, c, p];
            Random rand = new Random();
            for (int i = 0; i < r; i++)
            {
                for (int j = 0; j < c; j++)
                {
                    for (int k = 0; k < p; k++)
                    {
                        array3d[i, j, k] = rand.Next(-1000, 1000);//todo pn хардкод
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
                int[,,] array3d = Create_array_with_random_values(3, 4, 3);
                Console.WriteLine("Initial 3D array [2,3,4] :");
                Print_array_3d(array3d);
                Replace_pozitive_elements_by_zero(array3d);
                Console.WriteLine("3D array after replacing all positive numbers by zero :");
                Print_array_3d(array3d);
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