namespace Task05_4
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    internal class MyString
    {
        private char[] symbols;

        public MyString()
        {
        }

        /// <summary>
        /// Инициализирует новый экземпляр заданным классом, реализирующим интерфейс IEnumerable<char>
        /// </summary>
        /// <param name="parent"> Исходное перечисление</param>
        public MyString(IEnumerable<char> parent)
        {
            if (object.ReferenceEquals(parent, null))
            {
                throw new NullReferenceException();
            }

            this.symbols = parent.ToArray<char>();
        }

        /// <summary>
        /// Число элементов массива
        /// </summary>
        public int Length
        {
            get
            {
                return this.symbols.Length;
            }
        }

        /// <summary>
        /// Первый символ строки
        /// </summary>
        public char First
        {
            get
            {
                return this.Get_value(0);
            }
        }

        /// <summary>
        /// Последний символ строки
        /// </summary>
        public char Last
        {
            get
            {
                if (this.Length == 0)
                {
                    return this.symbols[0];
                }

                return this.symbols[this.Length - 1];
            }
        }

        /// <summary>
        /// Перегрузка индексатора
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public char this[int index]
        {
            get
            {
                if (!(index > this.Length) || !(index < 0))
                {
                    throw new IndexOutOfRangeException();
                }

                return this.symbols[index];
            }

            set
            {
                if (!(index > this.Length) || !(index < 0))
                {
                    throw new IndexOutOfRangeException();
                }

                this.symbols[index] = value;
            }
        }

        /// <summary>
        /// Перегрузка оператора приведения строки к MyString
        /// </summary>
        /// <param name="parent"></param>
        public static explicit operator MyString(string parent)
        {
            return new MyString(parent.ToCharArray());
        }

        /// <summary>
        /// Перегрузка оператора приведения массива сиволов к MyString
        /// </summary>
        /// <param name="parent"></param>
        public static explicit operator MyString(char[] parent)
        {
            return new MyString(parent);
        }

        /// <summary>
        /// Перегрузка оператора приведения экземпляра MyString к массиву символов
        /// </summary>
        /// <param name="parent"></param>
        public static implicit operator char[] (MyString parent)
        {
            return parent.symbols;
        }

        /// <summary>
        /// Перегрузка опреатора приведения экземпляра MyString к строке
        /// </summary>
        /// <param name="parent"></param>
        public static implicit operator string(MyString parent)
        {
            StringBuilder temp = new StringBuilder();
            foreach (var symbol in parent.symbols)
            {
                temp.Append(symbol);
            }

            return temp.ToString();
        }

        /// <summary>
        /// Перегрузка оператора сравнения
        /// </summary>
        /// <param name="first_operand"> Первый операнд</param>
        /// <param name="second_operand"> Второй операнд</param>
        /// <returns></returns>
        public static bool operator ==(MyString first_operand, MyString second_operand)
        {
            if (object.ReferenceEquals(first_operand, null) || object.ReferenceEquals(second_operand, null))
            {
                throw new NullReferenceException();
            }

            if (first_operand.Length != second_operand.Length)
            {
                return false;
            }

            var length = first_operand.Length;
            for (int i = 0; i < length; i++)
            {
                if (first_operand.Get_value(i).CompareTo(second_operand.Get_value(i)) != 0)
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Перегрузка оператора сравнения
        /// </summary>
        /// <param name="first_operand"> Первый операнд</param>
        /// <param name="second_operand"> Второй операнд</param>
        /// <returns></returns>
        public static bool operator !=(MyString first_operand, MyString second_operand)
        {
            if (object.ReferenceEquals(first_operand, null) || object.ReferenceEquals(second_operand, null))
            {
                throw new NullReferenceException();
            }

            if (first_operand.Length != second_operand.Length)
            {
                return true;
            }

            var length = first_operand.Length;
            for (int i = 0; i < length; i++)
            {
                if (first_operand.Get_value(i).CompareTo(second_operand.Get_value(i)) != 0)
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Перегрузка оператора сложения
        /// </summary>
        /// <param name="first_operand"> Первый операнд</param>
        /// <param name="second_operand"> Второй операнд</param>
        /// <returns> Новый экземпляр полученный сложением к первому операнду второго</returns>
        public static MyString operator +(MyString first_operand, MyString second_operand)
        {
            if (object.ReferenceEquals(first_operand, null) || object.ReferenceEquals(second_operand, null))
            {
                throw new NullReferenceException();
            }

            char[] temp = new char[first_operand.Length + second_operand.Length];

            for (int i = 0; i < first_operand.Length; i++)
            {
                temp[i] = first_operand.Get_value(i);
            }

            int j = first_operand.Length;
            for (int i = 0; i < second_operand.Length; i++, j++)
            {
                temp[j] = second_operand.Get_value(i);
            }

            return new MyString(temp);
        }

        /// <summary>
        /// Accessor
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public char Get_value(int index)
        {
            if (index < 0 || index > this.Length - 1)
            {
                throw new ArgumentOutOfRangeException();
            }

            return this.symbols[index];
        }

        /// <summary>
        /// Mutator
        /// </summary>
        /// <param name="index"></param>
        /// <param name="value"></param>
        public void Set_value(int index, char value)
        {
            if (index < 0 || index > this.Length - 1)
            {
                throw new ArgumentOutOfRangeException();
            }

            this.symbols[index] = value;
        }

        /// <summary>
        /// Возвращает новый экземпляр класса, полученный добавлением указанного символа в конец
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public MyString Insert_in_the_end(char value)
        {
            var temp = value.ToString().ToCharArray();
            return new MyString(this.symbols.Concat(temp));
        }

        /// <summary>
        /// Возвращает новый экземпляр класса, полученный добавлением указанного символа в начало
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public MyString Insert_to_the_begining(char value)
        {
            var temp = value.ToString().ToCharArray();
            return new MyString(temp.Concat(this.symbols));
        }

        /// <summary>
        /// Метод вставляет указанный символ в указанную позицию
        /// </summary>
        /// <param name="start_index"></param>
        /// <param name="value"></param>
        public void Insert(int start_index, char value)
        {
            if (start_index < 0 || start_index > this.Length)
            {
                throw new ArgumentOutOfRangeException();
            }

            int length = this.Length + 1;
            var temp = this.symbols;
            this.symbols = new char[length];
            for (int i = 0; i < start_index; i++)
            {
                this.symbols[i] = temp[i];
            }

            this.symbols[start_index] = value;
            for (int i = start_index + 1; i < length; i++)
            {
                this.symbols[i] = temp[i - 1];
            }
        }

        /// <summary>
        /// Возвращает различающиеся элементы последовательности, используя для сравнения значений компаратор проверки на равенство по умолчанию.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<char> Distinct()
        {
            return this.symbols.Distinct<char>();
        }

        /// <summary>
        /// Возвращает новый экземпляр полученный заменой исходного символа на новый
        /// </summary>
        /// <param name="child_symbol"> Исходный символ</param>
        /// <param name="parent_symbol"> Новый символ</param>
        /// <returns></returns>
        public MyString Replace(char child_symbol, char parent_symbol)
        {
            int length = this.Length;
            char[] temp = new char[length];
            for (int i = 0; i < length; i++)
            {
                temp[i] = (this.symbols[i].CompareTo(child_symbol) == 0) ? parent_symbol : this.Get_value(i);
            }

            return new MyString(temp);
        }

        /// <summary>
        /// Метод возвращает новый класс инициализированный массивом,
        /// полученным удалением указанного числа элементов начиная с указанной позиции
        /// </summary>
        /// <param name="start_index"> Начальная позиция для удаления</param>
        /// <param name="symbols_count"> Число удаляемых элементов</param>
        /// <returns></returns>
        public MyString Remove(int start_index, int symbols_count)
        {
            if (start_index < 0 || start_index > this.Length - 1
                || (symbols_count > this.Length - start_index))
            {
                throw new ArgumentOutOfRangeException();
            }

            char[] temp = new char[this.Length - symbols_count];
            for (int i = 0; i < start_index; i++)
            {
                temp[i] = this.symbols[i];
            }

            var length = temp.Length;
            for (int i = start_index + symbols_count; i < length; i++)
            {
                temp[i] = this.symbols[i];
            }

            return new MyString(temp);
        }

        /// <summary>
        /// Изменяет порядок символов строки на противоположный
        /// </summary>
        public void Reverse()
        {
            var length = this.Length;
            for (int start = 0, end = length - 1; start < length / 2; start++, end--)
            {
                var temp = this.symbols[start];
                this.symbols[start] = this.symbols[end];
                this.symbols[end] = temp;
            }
        }

        /// <summary>
        /// Копирует элементы начиная с указанной позиции
        /// </summary>
        /// <param name="obj"> Изменяемый экземпляр</param>
        /// <param name="start_index"> Начальная позиция</param>
        public void Copy_to(MyString obj, int start_index)
        {
            if (object.ReferenceEquals(obj, null))
            {
                throw new NullReferenceException();
            }

            if (start_index > this.Length)
            {
                throw new ArgumentOutOfRangeException();
            }

            if (start_index == this.Length - 1)
            {
                obj.symbols = new char[] { this.Last };
            }

            int length = this.Length - start_index;
            char[] temp = new char[length];

            for (int i = 0; i < length; i++, start_index++)
            {
                temp[i] = this.symbols[start_index];
            }

            obj.symbols = temp;
        }

        /// <summary>
        /// Возвращает true, если длина строки нуль, иначе false
        /// </summary>
        /// <returns></returns>
        public bool Is_empty()
        {
            return (this.symbols.Length == 0) ? true : false;
        }

        public override string ToString()
        {
            var temp = string.Empty;
            foreach (var character in this.symbols)
            {
                temp += string.Format("{0,-3}", character);
            }

            return temp;
        }

        /// <summary>
        /// Определяет, совпадает ли данный экземпляр MyString с указанным объектом
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (object.ReferenceEquals(obj, null))
            {
                throw new NullReferenceException();
            }

            if (obj is MyString)
            {
                var temp = obj as MyString;
                if (this.Length == temp.Length)
                {
                    for (int i = 0; i < this.Length; i++)
                    {
                        if (this.symbols[i].CompareTo(temp.symbols[i]) != 0)
                        {
                            return false;
                        }
                    }

                    return true;
                }
            }

            return false;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}