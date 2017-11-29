using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task2
{
    /// <summary>
    /// Subtask2_5 Сумма чисел меньше 1000, кратных 5 или 3
    /// </summary>
    internal class Subtask5
    {
        public override string ToString()
        {
            int i = 0;
            int sum = 0;
            while (i < 1000)
            {
                if (i % 5 == 0 || i % 3 == 0)
                    sum += i;
                i++;
            }
            return sum.ToString();
        }
    }
}