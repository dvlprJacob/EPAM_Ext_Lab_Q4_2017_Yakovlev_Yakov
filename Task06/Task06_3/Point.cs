namespace Task06_3
{
    using System;

    /// <summary>
    /// Представляет точку на плоскости с координатами абциссы ( X ) и ординаты ( Y )
    /// </summary>
    internal class Point : Figure
    {
        private int x;

        private int y;

        public Point()
        {
        }

        public Point(int x = 0, int y = 0)
        {
            this.x = x;
            this.y = y;
        }

        public Point(Point value)
        {
            if (object.ReferenceEquals(value, null))
            {
                throw new ArgumentNullException("Point(Point value), point value indicates to null");
            }

            this.x = value.x;
            this.y = value.y;
        }

        public new string Name
        {
            get
            {
                return "Point";
            }
        }

        private int X
        {
            get
            {
                return this.x;
            }

            set
            {
                this.x = value;
            }
        }

        private int Y
        {
            get
            {
                return this.y;
            }

            set
            {
                this.y = value;
            }
        }

        /// <summary>
        /// Вычисляет расстояние между двумя точками
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static double Distance(Point a, Point b)
        {
            if (object.ReferenceEquals(a, null) || object.ReferenceEquals(b, null))
            {
                throw new ArgumentNullException("Point(Point value), point a or point b indicates to null");
            }

            return Math.Sqrt(Math.Pow(b.X - a.X, 2) - Math.Pow(b.Y - a.Y, 2));
        }

        public void Set_x(int x)
        {
            this.x = x;
        }

        public int Get_x()
        {
            return this.x;
        }

        public void Set_y(int y)
        {
            this.y = y;
        }

        public int Get_y()
        {
            return this.y;
        }

        public override string ToString()
        {
            return string.Format("{0} ({1};{2})", this.Name, this.x, this.y);
        }
    }
}