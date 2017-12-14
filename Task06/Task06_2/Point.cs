namespace Task06_2
{
    using System;

    /// <summary>
    /// Представляет точку на плоскости с координатами абциссы ( X ) и ординаты ( Y )
    /// </summary>
    internal class Point
    {
        private int x;

        private int y;

        public Point()
        {
            this.x = 0;
            this.y = 0;
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

            this.X = value.x;
            this.Y = value.y;
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
            this.X = x;
        }

        public int Get_x()
        {
            return this.X;
        }

        public void Set_y(int y)
        {
            this.Y = y;
        }

        public int Get_y()
        {
            return this.Y;
        }

        public override string ToString()
        {
            return string.Format("point ({0};{1})", this.X, this.Y);
        }
    }
}