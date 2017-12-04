namespace Task03
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    internal class MyArray<T> where T : IComparable<T>
    {
        /// <summary>
        /// Массив эдементов типа T
        /// </summary>
        private T[] array;

        /// <summary>
        /// Количество элементов массива
        /// </summary>
        public int Length
        {
            get
            {
                if (IsNull())
                {
                    throw new NullReferenceException();
                }

                return array.Length;
            }
        }

        /// <summary>
        /// Инициализирует новый экземпляр класса MyArray
        /// </summary>
        public MyArray()
        {
        }

        /// <summary>
        /// Инициализирует новый экземпляр класа MyArray перечислением
        /// </summary>
        /// <param name="array"> Перечисление, записываемое в поле array</param>
        public MyArray(IEnumerable<T> array)
        {
            this.array = array.ToArray<T>();
        }

        /// <summary>
        /// Инициализирует новый экземпляр класа MyArray значениями по умолчанию
        /// </summary>
        /// <param name="length"> Длина массива</param>
        public MyArray(int length)
        {
            array = new T[length];
        }

        /// <summary>
        /// Возвращает максимальное значение в массиве
        /// </summary>
        /// <returns></returns>
        public T Find_max_value()
        {
            if (IsNull())
            {
                throw new NullReferenceException();
            }

            T tmp = array[0];
            foreach (var el in array)
            {
                if (tmp.CompareTo(el) < 0)
                {
                    tmp = el;
                }
            }

            return tmp;
        }

        /// <summary>
        /// Возвращает минимальное значение в массиве
        /// </summary>
        /// <returns></returns>
        public T Find_min_value()
        {
            if (IsNull())
            {
                throw new NullReferenceException();
            }

            T tmp = array[0];
            foreach (var el in array)
            {
                if (tmp.CompareTo(el) > 0)
                {
                    tmp = el;
                }
            }

            return tmp;
        }

        /// <summary>
        /// Сортирует поле array пузырьковым методом
        /// </summary>
        public void Sort()
        {
            if (IsNull())
            {
                throw new NullReferenceException();
            }

            for (int i = 0; i < Length; i++)
            {
                for (int j = 0; j < Length - i - 1; j++)
                {
                    if (array[j].CompareTo(array[j + 1]) > 0)
                    {
                        T tmp = array[j];
                        array[j] = array[j + 1];
                        array[j + 1] = tmp;
                    }
                }
            }
        }

        /// <summary>
        /// Проверяет поле array на равенство с null
        /// </summary>
        /// <returns> true, если массив пуст, иначе false</returns>
        public bool IsNull()
        {
            if (array.Equals(null))
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Генерирует рандомное строковое значение длины от 1 до 10 символами латиницы и цифр
        /// </summary>
        /// <returns></returns>
        private static string Generate_random_string_value()
        {
            Random rand = new Random();
            const string pool = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var builder = new StringBuilder();
            var stringLength = rand.Next(1, 10);

            for (var i = 0; i < stringLength; i++)
            {
                var c = pool[rand.Next(0, pool.Length)];
                builder.Append(c);
                System.Threading.Thread.Sleep(10);
            }

            return builder.ToString();
        }

        /// <summary>
        /// Генерирует рандомное символьное значение из множества символов латиницы и цифр
        /// </summary>
        /// <returns></returns>
        private static char Generate_random_char_value()
        {
            Random rand = new Random();
            const string pool = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            return pool[rand.Next(0, pool.Length)];
        }

        /// <summary>
        /// Генерирует рандомное целочисленное значением из интервала от -100 до 100
        /// </summary>
        /// <returns></returns>
        private static int Generate_random_int_value()
        {
            Random rand = new Random();
            return rand.Next(-100, 101);
        }

        /// <summary>
        /// Инициализирует новый экземпляр класса MyArray, массив строковых элементов длиной arrayLength заполняется случайными величинами
        /// </summary>
        /// <param name="arrayLength"> Длина массива</param>
        /// <returns></returns>
        public static MyArray<string> MyArray_with_random_string_values(int arrayLength)
        {
            List<string> values = new List<string>(arrayLength);
            for (int i = 0; i < arrayLength; i++)
            {
                values.Add(MyArray<string>.Generate_random_string_value());
            }

            return new MyArray<string>(values);
        }

        /// <summary>
        /// Инициализирует новый экземпляр класса MyArray, массив целочисленных элементов длиной arrayLength заполняется случайными величинами
        /// </summary>
        /// <param name="arrayLength"> Длина массива</param>
        /// <returns></returns>
        public static MyArray<int> MyArray_with_random_int_values(int arrayLength = 0)
        {
            if (arrayLength == 0)
            {
                return new MyArray<int>();
            }
            List<int> values = new List<int>();

            for (int i = 0; i < arrayLength; i++)
            {
                values.Add(MyArray<int>.Generate_random_int_value());
                System.Threading.Thread.Sleep(10);
            }

            return new MyArray<int>(values);
        }

        /// <summary>
        /// Инициализирует новый экземпляр класса MyArray, массив символьных элементов длиной arrayLength заполняется случайными величинами
        /// </summary>
        /// <param name="arrayLength"> Длина массива</param>
        /// <returns></returns>
        public static MyArray<char> MyArray_with_random_char_values(int arrayLength = 0)
        {
            if (arrayLength == 0)
            {
                return new MyArray<char>();
            }

            List<char> values = new List<char>();

            for (int i = 0; i < arrayLength; i++)
            {
                values.Add(MyArray<char>.Generate_random_char_value());
                System.Threading.Thread.Sleep(10);
            }

            return new MyArray<char>(values);
        }

        /// <summary>
        /// Возвращает элемент массива по индексу
        /// </summary>
        /// <param name="index"> Индекс элемента в массиве</param>
        /// <returns></returns>
        public T Get_value(int index)
        {
            if (IsNull())
            {
                throw new NullReferenceException();
            }

            if (index <= Length)
            {
                return array[index];
            }
            else
            {
                throw new IndexOutOfRangeException();
            }
        }

        /// <summary>
        /// Присваивает элементу массива по индексу переданное значение значение
        /// </summary>
        /// <param name="value"> Присваиваемое значение</param>
        /// <param name="index"> Индекс элемента массива</param>
        public void Set_value(T value, int index)
        {
            if (IsNull())
            {
                throw new NullReferenceException();
            }

            if (index >= Length)
            {
                throw new IndexOutOfRangeException();
            }

            array[index] = value;
        }

        /// <summary>
        /// Возвращает строковое представление экземпляра класса
        /// </summary>
        /// <returns>Строковое представление элементов массива через пробел</returns>
        public override string ToString()
        {
            if (IsNull())
            {
                return "Array is empty";
            }

            StringBuilder array = new StringBuilder();
            for (int i = 0; i < Length - 1; i++)
            {
                array.Append(this.array[i] + "   ");
            }

            return array.Append(this.array.Last()).ToString();
        }
    }
}