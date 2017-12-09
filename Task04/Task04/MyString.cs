namespace Task04
{
    using System.Collections.Generic;
    using System.Linq;

    internal class MyString
    {
        private string text;

        public MyString(string inputString)
        {
            this.text = inputString;
        }

        /// <summary>
        /// Вычисляет среднюю длину слова в строке Text
        /// </summary>
        /// <returns> Отношение числа букв к числу слов в строке</returns>
        public int Average_word_length()
        {
            if (this.text == string.Empty)
            {
                return 0;
            }

            int length = 0;
            List<int> wordLengths = new List<int>();
            foreach (var character in this.text)
            {
                switch (char.IsLetterOrDigit(character))
                {
                    case true:
                        length++;
                        break;

                    case false:
                        wordLengths.Add(length);
                        length = 0;
                        break;
                }
            }

            if (length != 0)
            {
                wordLengths.Add(length);
            }

            return (wordLengths.Count != 0) ? wordLengths.Sum() / wordLengths.Count : 0;
        }
    }
}