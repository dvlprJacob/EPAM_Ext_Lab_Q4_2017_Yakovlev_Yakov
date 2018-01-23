namespace Task07_2
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// Представляет текст, хранящий частоту слов без учета регистра, по умолчанию в качестве разделителей слов
    /// выступают знаки пробел и точка
    /// </summary>
    public class MyString
    {
        private Dictionary<string, int> text;

        /// <summary>
        /// Инициализирует экземпляр на основе заданной строки с разделением на слова по указанным символам
        /// </summary>
        /// <param name="initText"> Исходный текст</param>
        /// <param name="separator"> Знаки разделителей слов</param>
        public MyString(string initText, char[] separator = null)
        {
            if (string.IsNullOrEmpty(initText))
            {
                return;
            }

            // Не использую char.IsSeparator(), так как в ТЗ в качестве разделителей определены лишь  символ пробел и точка
            this.text = new Dictionary<string, int>();
            this.Text = initText;
            string[] temp;
            if (object.ReferenceEquals(separator, null))
            {
                this.Separators = new char[] { '.', ' ' };//todo pn хардкод. 
                temp = initText.Split(new char[] { '.', ' ' }, StringSplitOptions.RemoveEmptyEntries);
            }
            else
            {
                this.Separators = separator;
                temp = initText.Split(separator, StringSplitOptions.RemoveEmptyEntries);
            }

            foreach (var word in temp)
            {
                if (this.text.Where(kvp => kvp.Key.ToUpper() == word.ToUpper()).Count() == 0)
                {
                    var count = temp.Count(w => w.ToUpper().CompareTo(word.ToUpper()) == 0);
                    this.text.Add(word, count);
                }
            }
        }

        public string Text
        {
            get;
            private set;
        }

        /// <summary>
        /// Разделители слов
        /// </summary>
        public char[] Separators
        {
            get;
            private set;
        }

        /// <summary>
        /// Число слов в тексте
        /// </summary>
        public int Count
        {
            get
            {
                return this.text.Count;
            }
        }

        public KeyValuePair<string, int> GetPairByWord(string word)
        {
            return this.text.Where(kvp => kvp.Key.ToUpper() == word.ToUpper()).First();
        }

        public KeyValuePair<string, int>[] GetPairByKey(int count)
        {
            return this.text.Where(kvp => kvp.Value == count).ToArray();
        }

        public override string ToString()
        {
            StringBuilder temp = new StringBuilder();
            StringBuilder text = new StringBuilder();
            foreach (var kvp in this.text)
            {
                temp.Append(string.Format("{0} - repeat {1} teams", kvp.Key, kvp.Value));
                temp.AppendLine();
            }

            string sepTemp = string.Empty;
            foreach (var separator in this.Separators)
            {
                sepTemp += string.Format(@"'{0}',", separator);
            }

            return string.Format("{0}\n---------------------------\n{1}\nSeparators : {2}", this.Text, temp, sepTemp.Remove(sepTemp.Length - 1));
        }
    }
}