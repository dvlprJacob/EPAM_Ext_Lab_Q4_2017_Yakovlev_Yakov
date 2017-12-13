namespace Task06_3
{
    using System;

    /// <summary>
    /// Представляет прямоугольник заданный шириной и высотой
    /// </summary>
    internal class Rectangle : Figure
    {
        protected int width;
        protected int heigth;

        public Rectangle()
        {
            this.Name = "Rectangle";
            this.width = 2;
            this.heigth = 1;
        }

        public Rectangle(int width, int heigth)
        {
            if (width <= 0 || heigth <= 0)
            {
                throw new ArgumentException();
            }

            this.Name = "Rectangle";
            this.width = width;
            this.heigth = heigth;
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
                return width * heigth;
            }
        }

        /// <summary>
        /// Периметр
        /// </summary>
        public double Perimeter
        {
            get
            {
                return (2 * width) + (2 * heigth);
            }
        }

        public override string ToString()
        {
            return string.Format("{0} :\nwigth {1}\nheigth {2}\nperimeter {3}\narea {4}",
                this.Name, this.Width, this.Heigth, this.Perimeter, this.Area);
        }
    }
}