using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task02_1
{
    /// <summary>
    /// Subtask2_1
    /// Написать программу, которая определяет площадь прямоугольника со сторонами a и b.
    /// Если пользователь вводит некорректные значения(отрицательные, или 0),должно выдаваться сообщение об ошибке.
    /// Возможность ввода пользователем строки вида «абвгд», или нецелых чисел игнорировать.
    /// </summary>
    internal class Rectangle
    {
        private int a;
        private int b;

        public int Width
        {
            get { return a; }
            set
            {
                if (value > 0)
                {
                    a = value;
                    return;
                }
            }
        }

        public int Height
        {
            get { return b; }
            set
            {
                if (value > 0)
                {
                    b = value;
                    return;
                }
            }
        }

        /// <summary>
        /// Площадь прямоугольника
        /// </summary>
        public int Area { get { return Width * Height; } }

        public Rectangle(int width, int height)
        {
            if (width <= 0 || height <= 0)
                throw new ArgumentException("Arguments values must be more than zero");

            Width = width;
            Height = height;
        }

        public Rectangle()
        {
        }
    }
}