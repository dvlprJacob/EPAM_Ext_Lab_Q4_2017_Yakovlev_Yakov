namespace Task09_3
{
    using System.Collections.Generic;
    using System.Linq;
    using Task09_2;

    /// <summary>
    /// Поиск в массиве строк элементов, представляющих целое положительное число разными способами
    /// </summary>
    internal class MyStringArray
    {
        public MyStringArray(IEnumerable<string> parent)
        {
            this.Values = parent as string[];
        }

        public delegate string[] SearchWay();

        public string[] Values { get; set; }

        public int Length
        {
            get
            {
                return this.Values.Length;
            }
        }

        public string this[int index]
        {
            get
            {
                return (index >= 0 && index < this.Length) ? this.Values[index] : default(string);
            }

            set
            {
                if (index >= 0 && index < this.Length)
                {
                    this.Values[index] = value;
                }
            }
        }

        /// <summary>
        /// Поиск напрямую
        /// </summary>
        /// <returns> Все элементы массива строк, являющиеся целыми положительными числами</returns>
        public string[] FindAllPositiveNumberStrings()
        {
            if (this.Values == null)
            {
                return null;
            }

            List<string> temp = new List<string>();

            foreach (var elem in this.Values)
            {
                // Метод расширения для типа string из задания Task09_2
                if (elem.IsPositiveIntegerNumber())
                {
                    temp.Add(elem);
                }
            }

            return temp.ToArray();
        }

        /// <summary>
        /// Посик через делегат
        /// </summary>
        /// <returns></returns>
        public string[] FindAllPositiveNumberByDelegate()
        {
            if (this.Values == null)
            {
                return null;
            }

            List<string> temp = new List<string>();
            SearchWay result = new SearchWay(this.FindAllPositiveNumberStrings);

            return result();
        }

        /// <summary>
        /// Поиск через анонимный делегат
        /// </summary>
        /// <returns></returns>
        public string[] FindAllPositiveNumberByAnonymousDelegate()
        {
            if (this.Values == null)
            {
                return null;
            }

            List<string> temp = new List<string>();
            SearchWay result = delegate
            {
                return this.FindAllPositiveNumberStrings();
            };

            return result();
        }

        /// <summary>
        /// Поиск через лямбда - выражение
        /// </summary>
        /// <returns></returns>
        public string[] FindAllPositiveNumberByLymbdaExtensionDelegate()
        {
            if (this.Values == null)
            {
                return null;
            }

            List<string> temp = new List<string>();
            SearchWay result = () => this.FindAllPositiveNumberStrings();
            return result();
        }

        /// <summary>
        /// Поиск через LINQ
        /// </summary>
        /// <returns></returns>
        public string[] FindAllPositiveNumberStringsByLinq()
        {
            if (this.Values == null)
            {
                return null;
            }

            var result = this.Values.Where(s => s.First() != '0').Where(s => s.All(c => char.IsDigit(c)));

            //// либо так :
            //// var result = this.Values.Where(s => s.IsPositiveIntegerNumber());

            return result.ToArray();
        }
    }
}