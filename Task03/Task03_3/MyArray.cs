using System;
using System.Text;

namespace Task03_3
{
    internal class MyArray
    {
        public MyArray()
        {
        }

        /// <summary>
        /// Инициализирует новый целочисленный массив заданной длины случайными величинами из указанного диапазона
        /// </summary>
        /// <param name="length"></param>
        public MyArray(int length, int minValue, int maxValue)
        {
            try
            {
                if (length < 0)
                {
                    this.Values = null;
                    return;
                }

                Random rand = new Random();
                this.Values = new int[length];
                for (int i = 0; i < length; i++)
                {
                    this.Values[i] = rand.Next(minValue, maxValue);
                }
            }
            catch (ArgumentOutOfRangeException e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public int[] Values { get; set; }

        /// <summary>
        /// Возвращает сумму положительных элементов массива
        /// </summary>
        /// <returns></returns>
        public int Non_negative_elements_sum()
        {
            if (this.Values.Length > 0)
            {
                int result = (this.Values[0] > 0) ? this.Values[0] : 0;
                for (int i = 1; i < this.Values.Length; i++)
                {
                    result += (this.Values[i] > 0) ? this.Values[i] : 0;
                }

                return result;
            }

            return 0;
        }

        public override string ToString()
        {
            if (this.Values.Length == 0)
            {
                return "Values is empty";
            }

            StringBuilder builder = new StringBuilder();
            foreach (var el in this.Values)
            {
                builder.Append(string.Format("{0}  ", el));
            }

            return builder.ToString();
        }
    }
}