using System;
using System.Text;

namespace Task03_4
{
    internal class MyArray
    {
        public double[,] array { get; set; }

        public MyArray()
        {
        }

        /// <summary>
        /// Инициализирует новую матрицу действительных чисел случайными значениями
        /// </summary>
        /// <param name="rowLength"></param>
        /// <param name="colLength"></param>
        public MyArray(int rowLength, int colLength)
        {
            Random rand = new Random();
            array = new double[rowLength, colLength];
            for (int i = 0; i < rowLength; i++)
            {
                for (int j = 0; j < colLength; j++)
                {
                    array[i, j] = Math.Round(rand.NextDouble(), 2) + rand.Next(-100, 100);
                }
            }
        }

        /// <summary>
        /// Возвращает сумму элементов матрицы, сумма индексов которого четна
        /// </summary>
        /// <returns></returns>
        public double Even_positions_elements_sum()
        {
            if (array.Length == 0)
            {
                return 0;
            }

            double result = 0;
            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    result += ((i + j) % 2 == 0) ? array[i, j] : 0;
                }
            }

            return result;
        }

        /// <summary>
        /// Возвращает строковое представление экземпляра класса
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            if (array.Length == 0)
            {
                return "Array is empty";
            }

            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    builder.Append(String.Format("{0,8}  ", array[i, j]));
                }
                builder.AppendLine();
            }

            builder.Remove(builder.Length - 1, 1);

            return builder.ToString();
        }
    }
}