namespace Task08
{
    using System;
    using System.Linq;

    /// <summary>
    /// Представляет массив строк
    /// </summary>
    public class MyStringArray
    {
        private string[] array;

        public MyStringArray(string[] sample)
        {
            this.array = new string[sample.Length];
            for (int i = 0; i < sample.Length; i++)
            {
                this.array[i] = sample[i];
            }
        }

        public int Length
        {
            get
            {
                return this.array.Length;
            }
        }

        public string this[int index]
        {
            get
            {
                if (index >= 0 && index < this.array.Length)
                {
                    return this.array[index];
                }

                return default(string);
            }

            set
            {
                if (index >= 0 && index < this.array.Length)
                {
                    this.array[index] = value;
                }
            }
        }

        /// <summary>
        /// Выполняет сотрировку массива строк по указанному предикату
        /// </summary>
        /// <param name="compareWay"> Предикат</param>
        /// <returns></returns>
        public bool Sort(Func<string, string, int> compareWay)
        {
            if (compareWay.Equals(null))
            {
                return false;
            }

            int j;
            for (int i = 0; i < this.array.Length; i++)
            {
                j = i + 1;
                while (j < this.array.Length)
                {
                    if (compareWay.Invoke(this.array[i], this.array[j]) > 0)
                    {
                        this.Swap(i, j);
                    }

                    j++;
                }
            }

            return true;
        }

        public string First()
        {
            return this.array.First();
        }

        public string Last()
        {
            return this.array.Last();
        }

        private void Swap(int fIndex, int sIndex)
        {
            string buf = this.array[fIndex];
            this.array[fIndex] = this.array[sIndex];
            this.array[sIndex] = buf;
        }
    }
}