namespace Task07_3
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Представляет динамический массив обобщенного типа
    /// </summary>
    /// <typeparam name="T"> Тип массива</typeparam>
    public class DinamicArray<T> : IEnumerable<T>, IEnumerator<T>
    {
        /// <summary>
        /// Коэффициент, на который умножается вместимость массива, если при добавлении или вставке в массив количество
        /// элементов сравнялось с вместимостью
        /// </summary>
        private const int CapacityMultiplyCoef = 2;

        /// <summary>
        /// Вместимость для конструктора по умолчанию
        /// </summary>
        private const int DefaultCapacity = 8;

        private T[] array;
        private int position = -1;
        private bool disposedValue = false; //// Для определения избыточных вызовов

        public DinamicArray()
            : this(DefaultCapacity)
        {
        }

        public DinamicArray(int capacity)
        {
            if (capacity == 0)
            {
                throw new ArgumentException();
            }

            this.array = new T[capacity];
            this.Capacity = capacity;
            this.Length = 0;
        }

        public DinamicArray(IEnumerable<T> sample)
        {
            this.array = sample.ToArray<T>();
            this.Capacity = sample.Count() * CapacityMultiplyCoef;
            this.Length = sample.Count();
        }

        public int Length
        {
            get;
            private set;
        }

        public int Capacity
        {
            get;
            private set;
        }

        public T Current
        {
            get
            {
                return this.array[this.position];
            }
        }

        object IEnumerator.Current
        {
            get
            {
                return this.Current;
            }
        }

        public T this[int index]
        {
            get
            {
                if (index >= 0 && index < this.Length)
                {
                    return this.array[index];
                }

                throw new IndexOutOfRangeException();
            }

            set
            {
                if (index >= 0 && index < this.Length)
                {
                    this.array[index] = value;
                }

                throw new IndexOutOfRangeException();
            }
        }

        public void Add(T value)
        {
            if (this.Length < this.Capacity)
            {
                this.array[this.Length] = value;
            }
            else
            {
                this.IncreaseCapacity(this.Length * CapacityMultiplyCoef);
                this.array[this.Length] = value;
            }

            this.Length++;
        }

        public void AddRange(IEnumerable<T> sample)
        {
            if ((this.Capacity - this.Length) >= sample.Count())
            {
                for (int i = 0; i < sample.Count(); i++)
                {
                    this.array[this.Length] = sample.ElementAt(i);
                    this.Length++;
                }
            }
            else
            {
                this.IncreaseCapacity((this.Length + sample.Count()) * CapacityMultiplyCoef);

                for (int i = 0; i < sample.Count(); i++)
                {
                    this.array[this.Length] = sample.ElementAt(i);
                    this.Length++;
                }
            }
        }

        public bool Remove(int index)
        {
            try
            {
                if (index < 0 || index >= this.Length)
                {
                    throw new IndexOutOfRangeException();
                }

                var temp = this.array;

                for (int i = 0; i < index; i++)
                {
                    this.array[i] = temp[i];
                }

                for (int i = index + 1; i < this.Length; i++)
                {
                    this.array[i - 1] = temp[i];
                }

                this.Length--;
                return true;
            }
            catch (IndexOutOfRangeException)
            {
                return false;
            }
        }

        public bool Insert(T value, int index)
        {
            try
            {
                if (index < 0 || index >= this.Capacity)
                {
                    throw new IndexOutOfRangeException();
                }

                //// Если количество элементов равно вместимости, увеличиваем последнее
                if (this.Length == this.Capacity)
                {
                    this.IncreaseCapacity(this.Capacity * CapacityMultiplyCoef);
                }

                if (index == 0)
                {
                    this.InsertToFirstPosition(value);
                    return true;
                }
                else if (index == this.Length)
                {
                    this.Add(value);
                    return true;
                }

                var temp = this.array;
                if (index > this.Length)
                {
                    this.Length = index;
                    this.array = new T[this.Length];
                    for (int i = 0; i < this.Length; i++)
                    {
                        this.array[i] = temp[i];
                    }

                    this.array[index] = value;

                    return true;
                }
                else if (index < this.Length)
                {
                    this.Length++;
                    this.array = new T[this.Length];
                    for (int i = 0; i < index; i++)
                    {
                        this.array[i] = temp[i];
                    }

                    this.array[index] = value;

                    for (int i = index + 1; i < this.Length; i++)
                    {
                        this.array[i] = temp[i - 1];
                    }

                    return true;
                }

                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public override string ToString()
        {
            string temp = string.Empty;
            for (int i = 0; i < this.Length; i++)
            {
                temp += string.Format("{0} ", this.array[i]);
            }

            return temp;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return ((IEnumerable<T>)this.array).GetEnumerator();
        }

        public bool MoveNext()
        {
            if (this.position == this.Length - 1)
            {
                this.Reset();
                return false;
            }

            this.position++;
            return true;
        }

        public void Reset()
        {
            this.position = -1;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable<T>)this.array).GetEnumerator();
        }

        #region IDisposable Support

        //// TODO: переопределить метод завершения, только если Dispose(bool disposing) выше включает код для освобождения неуправляемых ресурсов.
        //// ~DinamicArray() {
        ////   // Не изменяйте этот код. Разместите код очистки выше, в методе Dispose(bool disposing).
        ////   Dispose(false);
        //// }

        // Этот код добавлен для правильной реализации шаблона высвобождаемого класса.
        public void Dispose()
        {
            //// Не изменяйте этот код. Разместите код очистки выше, в методе Dispose(bool disposing).
            this.Dispose(true);
            //// TODO: раскомментировать следующую строку, если метод завершения переопределен ниже.
            //// GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposedValue)
            {
                if (disposing)
                {
                    // TODO: освободить управляемое состояние (управляемые объекты).
                }

                //// TODO: освободить неуправляемые ресурсы (неуправляемые объекты) и переопределить ниже метод завершения.
                //// TODO: задать большим полям значение NULL.

                this.disposedValue = true;
            }
        }

        #endregion IDisposable Support

        /// <summary>
        /// Увеличивает вместимость до заданной величины
        /// </summary>
        /// <param name="newCapacity"> Новая вместимость</param>
        private void IncreaseCapacity(int newCapacity)
        {
            if (newCapacity > this.Capacity)
            {
                var temp = this.array;
                this.Capacity = newCapacity;
                this.array = new T[this.Capacity];
                temp.CopyTo(this.array, 0);
            }
        }

        private void InsertToFirstPosition(T value)
        {
            this.Length++;
            var temp1 = this.array;
            var temp2 = new T[this.Length];
            temp2[0] = value;

            for (int i = 1; i < this.Length; i++)
            {
                temp2[i] = temp1[i - 1];
            }

            this.array = temp2;
        }
    }
}