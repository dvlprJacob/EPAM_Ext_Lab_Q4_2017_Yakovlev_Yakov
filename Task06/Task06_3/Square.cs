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
        }

        public Square(int side)
            : base(side, side)
        {
        }

        public new string Name
        {
            get
            {
                return "Square";
            }
        }

        public int Side
        {
            get
            {
                return this.Width;
            }

            set
            {
                if (value <= 0)
                {
                    throw new ArgumentException();
                }

                this.Width = value;
            }
        }

        public override string ToString()
        {
            return string.Format("{0} :\nwigth {1}\nheigth {2}\nperimeter {3}\narea {4}", this.Name, this.Side, this.Side, this.Perimeter, this.Area);
        }
    }
}