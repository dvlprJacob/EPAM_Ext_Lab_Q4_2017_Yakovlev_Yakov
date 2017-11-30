using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task02_4
{
    /// <summary>
    /// Subtask2_4 Ель
    /// </summary>
    internal class Subtask4
    {
        private int n;

        public int RowCount
        {
            get { return n; }
            set { if (value > 0) n = value; }
        }

        public Subtask4(int rowCount)
        {
            RowCount = rowCount;
        }

        public Subtask4()
        {
        }

        /// <summary>
        /// Возвращает треугольник из rowCount строк с отступом верхней вершины на spaceCount символов пробела
        /// </summary>
        /// <param name="spaceCount"> Число пробелов</param>
        /// <param name="rowCount"> Число строк треугольника</param>
        /// <returns></returns>
        private static string Triangle(int spaceCount, int rowCount)
        {
            StringBuilder temporary = new StringBuilder();
            int k = 1;
            for (int i = rowCount; i > 0; i--)
            {
                temporary.Append(' ', spaceCount - k);
                temporary.Append('*', (2 * k) - 1);
                temporary.AppendLine();
                k++;
            }
            return temporary.ToString();
        }

        public override string ToString()
        {
            if (RowCount == 1)
                return "*";

            StringBuilder temporary = new StringBuilder();
            for (int i = 0; i < RowCount; i++)
            {
                temporary.Append(Subtask4.Triangle(RowCount, i + 1));
            }
            return temporary.ToString();
        }
    }
}