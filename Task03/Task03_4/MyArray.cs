using System;
using System.Text;

namespace Task03_4
{
    internal class MyArray
    {
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
            this.Values = new double[rowLength, colLength];
            for (int i = 0; i < rowLength; i++)
            {
                for (int j = 0; j < colLength; j++)
                {
                    this.Values[i, j] = Math.Round(rand.NextDouble(), 2) + rand.Next(-100, 100);
                }
            }
        }

        public double[,] Values { get; set; }

        /// <summary>
        /// Возвращает сумму элементов матрицы, сумма индексов которого четна
        /// </summary>
        /// <returns></returns>
        public double Even_positions_elements_sum()
        {
            if (this.Values.Length == 0)
            {
                return 0;
            }

            double result = 0;
            for (int i = 0; i < this.Values.GetLength(0); i++)
            {
                for (int j = 0; j < this.Values.GetLength(1); j++)
                {
                    result += ((i + j) % 2 == 0) ? this.Values[i, j] : 0;
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
            if (this.Values.Length == 0)
            {
                return "Values is empty";
            }

            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < this.Values.GetLength(0); i++)
            {
                for (int j = 0; j < this.Values.GetLength(1); j++)
                {
                    builder.Append(string.Format("{0,8}  ", this.Values[i, j]));
                }

                builder.AppendLine();
            }

            builder.Remove(builder.Length - 1, 1);

            return builder.ToString();
        }
    }
}