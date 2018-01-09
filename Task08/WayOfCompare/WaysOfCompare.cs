namespace WayOfCompare
{
    /// <summary>
    /// Класс реализует предикаты сравнения двух строк
    /// </summary>
    public class WaysOfCompare
    {
        public WaysOfCompare()
        {
        }

        /// <summary>
        /// Выполняет сортировку массива строк по возрастанию длины, если длина равная, сортирует по алфавиту
        /// </summary>
        /// <param name="f"></param>
        /// <param name="s"></param>
        /// <returns></returns>
        public static int ByLength(string f, string s)
        {
            return (f.Length == s.Length) ? string.Compare(f, s) : (f.Length > s.Length) ? 1 : -1;
        }
    }
}