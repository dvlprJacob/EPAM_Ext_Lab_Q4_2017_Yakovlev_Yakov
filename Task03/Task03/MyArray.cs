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
                if (this.IsNull())
                {
                    throw new NullReferenceException();
                }

                return this.array.Length;
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
            this.array = new T[length];
        }

        /// <summary>
        /// Генерирует рандомное строковое значение длины от 1 до 10 символами латиницы и цифр
        /// </summary>
        /// <returns></returns>
        private static string Get_random_string()
        {
            Random rand = new Random();
            const string Pool = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var builder = new StringBuilder();
            var stringLength = rand.Next(1, 10);

            for (var i = 0; i < stringLength; i++)
            {
                var c = Pool[rand.Next(0, Pool.Length)];
                builder.Append(c);
                System.Threading.Thread.Sleep(10);
            }

            return builder.ToString();
        }

        /// <summary>
        /// Генерирует рандомное символьное значение из множества символов латиницы и цифр
        /// </summary>
        /// <returns></returns>
        private static char Get_random_character()
        {
            Random rand = new Random();
            const string Pool = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            return Pool[rand.Next(0, Pool.Length)];
        }

        /// <summary>
        /// Генерирует рандомное целочисленное значением из интервала от -100 до 100
        /// </summary>
        /// <returns></returns>
        private static int Get_random_number()
        {
            Random rand = new Random();
            return rand.Next(-100, 101);
        }

        /// <summary>
        /// Инициализирует новый экземпляр класса MyArray, массив строковых элементов длиной arrayLength заполняется случайными величинами
        /// </summary>
        /// <param name="arrayLength"> Длина массива</param>
        /// <returns></returns>
        public static MyArray<string> String_array(int arrayLength)
        {
            List<string> values = new List<string>(arrayLength);
            for (int i = 0; i < arrayLength; i++)
            {
                values.Add(MyArray<string>.Get_random_string());
            }

            return new MyArray<string>(values);
        }

        /// <summary>
        /// Инициализирует новый экземпляр класса MyArray, массив целочисленных элементов длиной arrayLength заполняется случайными величинами
        /// </summary>
        /// <param name="arrayLength"> Длина массива</param>
        /// <returns></returns>
        public static MyArray<int> Integer_array(int arrayLength = 0)
        {
            if (arrayLength == 0)
            {
                return new MyArray<int>();
            }

            List<int> values = new List<int>();

            for (int i = 0; i < arrayLength; i++)
            {
                values.Add(MyArray<int>.Get_random_number());
                System.Threading.Thread.Sleep(10);
            }

            return new MyArray<int>(values);
        }

        /// <summary>
        /// Инициализирует новый экземпляр класса MyArray, массив символьных элементов длиной arrayLength заполняется случайными величинами
        /// </summary>
        /// <param name="arrayLength"> Длина массива</param>
        /// <returns></returns>
        public static MyArray<char> Character_array(int arrayLength = 0)
        {
            if (arrayLength == 0)
            {
                return new MyArray<char>();
            }

            List<char> values = new List<char>();

            for (int i = 0; i < arrayLength; i++)
            {
                values.Add(MyArray<char>.Get_random_character());
                System.Threading.Thread.Sleep(10);
            }

            return new MyArray<char>(values);
        }

        /// <summary>
        /// Возвращает минимальное значение в массиве
        /// </summary>
        /// <returns></returns>
        public T Get_min_element()
        {
            if (this.IsNull())
            {
                throw new NullReferenceException();
            }

            T tmp = this.array[0];
            foreach (var el in this.array)
            {
                if (tmp.CompareTo(el) < 0)
                {
                    tmp = el;
                }
            }

            return tmp;
        }

        /// <summary>
        /// Возвращает максимальное значение в массиве
        /// </summary>
        /// <returns></returns>
        public T Get_max_element()
        {
            if (this.IsNull())
            {
                throw new NullReferenceException();
            }

            T tmp = this.array[0];
            foreach (var el in this.array)
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
            if (this.IsNull())
            {
                throw new NullReferenceException();
            }

            for (int i = 0; i < this.Length; i++)
            {
                for (int j = 0; j < this.Length - i - 1; j++)
                {
                    if (this.array[j].CompareTo(this.array[j + 1]) > 0)
                    {
                        T tmp = this.array[j];
                        this.array[j] = this.array[j + 1];
                        this.array[j + 1] = tmp;
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
            if (this.array.Equals(null))
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Возвращает элемент массива по индексу
        /// </summary>
        /// <param name="index"> Индекс элемента в массиве</param>
        /// <returns></returns>
        public T Get_value(int index)
        {
            if (this.IsNull())
            {
                throw new NullReferenceException();
            }

            if (index <= this.Length)
            {
                return this.array[index];
            }
            else
            {
                throw new IndexOutOfRangeException();
            }
        }

        /// <summary>
        /// Заменяет элемент с указанным индексом на указанный элемент
        /// </summary>
        /// <param name="value"> Присваиваемое значение</param>
        /// <param name="index"> Индекс элемента массива</param>
        public void Replace_by(T value, int index)
        {
            if (this.IsNull())
            {
                throw new NullReferenceException();
            }

            if (index >= this.Length)
            {
                throw new IndexOutOfRangeException();
            }

            this.array[index] = value;
        }

        /// <summary>
        /// Возвращает строковое представление экземпляра класса
        /// </summary>
        /// <returns>Строковое представление элементов массива через пробел</returns>
        public override string ToString()
        {
            if (this.IsNull())
            {
                return "Array is empty";
            }

            StringBuilder array = new StringBuilder();
            for (int i = 0; i < this.Length - 1; i++)
            {
                array.Append(this.array[i] + "   ");
            }

            return array.Append(this.array.Last()).ToString();
        }
    }
}