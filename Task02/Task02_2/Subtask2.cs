using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task02_2
{
    /// <summary>
    /// Subtask2_2 Лесенка
    /// </summary>
    internal class Subtask2
    {
        private int n;

        public int RowCount
        {
            get { return n; }
            set { if (value > 0) n = value; }
        }

        public Subtask2(int rowCount)
        {
            RowCount = rowCount;
        }

        public Subtask2()
        {
        }

        public override string ToString()
        {
            if (RowCount == 1)
            {
                return "*";
            }

            String temporary = "*\n";
            for (int i = 2; i < RowCount; i++)
            {
                temporary += new String('*', i) + "\n";
            }
            temporary += new string('*', RowCount);
            return temporary.ToString();
        }
    }
}