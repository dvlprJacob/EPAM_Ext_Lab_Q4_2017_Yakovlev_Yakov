using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task2
{
    /// <summary>
    /// Subtask2_3 Пирамида
    /// </summary>
    internal class Subtask3
    {
        private int n;

        public int RowCount
        {
            get { return n; }
            set { if (value > 0) n = value; }
        }

        public Subtask3(int rowCount)
        {
            RowCount = rowCount;
        }

        public Subtask3()
        {
        }

        public override string ToString()
        {
            if (RowCount == 1)
                return "*";

            StringBuilder temporary = new StringBuilder();
            int k = 1;
            for (int i = RowCount; i > 0; i--)
            {
                temporary.Append(' ', i - 1);
                temporary.Append('*', (2 * k) - 1);
                temporary.AppendLine();
                k++;
            }
            return temporary.ToString();
        }
    }
}