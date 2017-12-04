using System;
using System.Text;

namespace Task03_3
{
    internal class MyArray
    {
        public int[] array { get; set; }

        public MyArray()
        {
        }

        /// <summary>
        /// Инициализирует новый целочисленный массив заданной длины случайными величинами
        /// </summary>
        /// <param name="length"></param>
        public MyArray(int length)
        {
            if (length < 0)
            {
                this.array = null;
                return;
            }

            Random rand = new Random();
            array = new int[length];
            for (int i = 0; i < length; i++)
            {
                array[i] = rand.Next(-100, 100);
            }
        }

        /// <summary>
        /// Возвращает сумму положительных элементов массива
        /// </summary>
        /// <returns></returns>
        public int Non_negative_elements_sum()
        {
            if (array.Length > 0)
            {
                int result = (array[0] > 0) ? array[0] : 0;
                for (int i = 1; i < array.Length; i++)
                {
                    result += (array[i] > 0) ? array[i] : 0;
                }

                return result;
            }
            return 0;
        }

        public override string ToString()
        {
            if (array.Length == 0)
            {
                return "Array is empty";
            }

            StringBuilder builder = new StringBuilder();
            foreach (var el in array)
            {
                builder.Append(String.Format("{0}  ", el));
            }

            return builder.ToString();
        }
    }
}