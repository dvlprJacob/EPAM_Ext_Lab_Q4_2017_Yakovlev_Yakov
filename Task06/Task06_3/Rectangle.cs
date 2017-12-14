namespace Task06_3
{
    using System;

    /// <summary>
    /// Представляет прямоугольник заданный шириной и высотой
    /// </summary>
    internal class Rectangle : Figure
    {
        private int width;
        private int heigth;

        public Rectangle()
        {
            this.width = 2;
            this.heigth = 1;
        }

        public Rectangle(int width, int heigth)
        {
            if (width <= 0 || heigth <= 0)
            {
                throw new ArgumentException();
            }

            this.width = width;
            this.heigth = heigth;
        }

        public new string Name
        {
            get
            {
                return "Rectangle";
            }
        }

        /// <summary>
        /// Высота
        /// </summary>
        public int Width
        {
            get
            {
                return this.width;
            }

            set
            {
                if (value <= 0)
                {
                    throw new ArgumentException();
                }

                this.width = value;
            }
        }

        /// <summary>
        /// Ширина
        /// </summary>
        public int Heigth
        {
            get
            {
                return this.heigth;
            }

            set
            {
                if (value <= 0)
                {
                    throw new ArgumentException();
                }

                this.heigth = value;
            }
        }

        /// <summary>
        /// Площадь
        /// </summary>
        public double Area
        {
            get
            {
                return this.width * this.heigth;
            }
        }

        /// <summary>
        /// Периметр
        /// </summary>
        public double Perimeter
        {
            get
            {
                return (2 * this.width) + (2 * this.heigth);
            }
        }

        public override string ToString()
        {
            return string.Format("{0} :\nwigth {1}\nheigth {2}\nperimeter {3}\narea {4}", this.Name, this.width, this.heigth, this.Perimeter, this.Area);
        }
    }
}