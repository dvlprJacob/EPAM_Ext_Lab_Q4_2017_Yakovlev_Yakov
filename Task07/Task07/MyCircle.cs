namespace Task07_1
{
    /// <summary>
    /// Представляет круг из пар элементов с порядковым номером 1 или 2, позволяющий запустить цикл по удалению
    /// каждого второго, пока не останется один элемент
    /// </summary>
    public class MyCircle
    {
        private MyDictionary circle;

        public MyCircle(int n)
        {
            if (n <= 0)
            {
                return;
            }

            this.circle = new MyDictionary(n);
        }

        public int Count
        {
            get
            {
                return this.circle.Count;
            }
        }

        /// <summary>
        /// Запускает удаление каждой второй пары
        /// </summary>
        public void RemoveEverySecond()
        {
            while (this.Count != 1)
            {
                this.circle.RemoveAny(2);
            }
        }

        public override string ToString()
        {
            return this.circle.ToString();
        }
    }
}