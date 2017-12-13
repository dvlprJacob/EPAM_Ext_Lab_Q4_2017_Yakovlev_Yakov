namespace Task06_3
{
    using System;

    /// <summary>
    /// Представляет квадрат с заданной стороной
    /// </summary>
    internal class Square : Rectangle
    {
        public Square()
            : base(1, 1)
        {
            this.Name = "Square";
        }

        public Square(int side)
            : base(side, side)
        {
            this.Name = "Square";
        }

        public int Side
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

        public override string ToString()
        {
            return string.Format("{0} :\nwigth {1}\nheigth {2}\nperimeter {3}\narea {4}",
                this.Name, this.Width, this.Heigth, this.Perimeter, this.Area);
        }
    }
}