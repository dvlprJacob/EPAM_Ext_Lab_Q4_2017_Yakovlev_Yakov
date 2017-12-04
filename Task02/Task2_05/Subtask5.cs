namespace Task2_05
{
    /// <summary>
    /// Subtask2_5 Сумма чисел меньше 1000, кратных 5 или 3
    /// </summary>
    internal class Subtask5
    {
        public Subtask5(int n)
        {
            this.N = n;
        }

        public int N { get; set; }

        public override string ToString()
        {
            int i = 0;
            int sum = 0;

            while (i < this.N)
            {
                if (i % 5 == 0 || i % 3 == 0)
                {
                    sum = i + sum;
                }
                i++;
            }

            return string.Format("Сумма чисел не превосходящих {0} и кратных 5 или 3 равна {1}", this.N, sum.ToString());
        }
    }
}