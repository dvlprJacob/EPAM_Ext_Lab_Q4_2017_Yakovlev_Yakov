namespace Task05
{
    using System;

    /// <summary>
    /// Представляет класс круг с центром в точке Centre и радиусом Radius
    /// </summary>
    internal class Round
    {
        private Point centre;

        private int radius;

        public Round()
        {
            this.centre = new Point();
            this.radius = 1;
        }

        public Round(Point centre, int radius)
        {
            if (radius <= 0 || object.ReferenceEquals(centre, null))
            {
                throw new ArgumentException();
            }

            this.Centre = new Point(centre);
            this.Radius = radius;
        }

        public Round(int radius)
        {
            if (radius <= 0)
            {
                throw new ArgumentException();
            }

            this.centre = new Point();
            this.Radius = radius;
        }

        public Round(Round sample)
        {
            if (sample.Equals(null))
            {
                throw new ArgumentNullException();
            }

            this.Centre = sample.Centre;
            this.Radius = sample.Radius;
        }

        /// <summary>
        /// Точка центра круга
        /// </summary>
        public Point Centre
        {
            get
            {
                return this.centre;
            }

            set
            {
                if (object.ReferenceEquals(value, null))
                {
                    throw new NullReferenceException();
                }

                if (value is Point)
                {
                    this.centre = new Point(value);
                }
            }
        }

        /// <summary>
        /// Радиус круга
        /// </summary>
        public int Radius
        {
            get
            {
                return this.radius;
            }

            set
            {
                if (value > 0)
                {
                    this.radius = value;
                }
            }
        }

        /// <summary>
        /// Длина окружности круга
        /// </summary>
        public double Circumference
        {
            get
            {
                return 2 * (Math.PI * this.Radius);
            }
        }

        /// <summary>
        /// Площадь круга
        /// </summary>
        public double Area
        {
            get
            {
                return Math.PI * Math.Pow(this.Radius, 2);
            }
        }

        /// <summary>
        /// Проверяет, входит ли точка в круг
        /// </summary>
        /// <param name="a"> Точка</param>
        /// <returns></returns>
        public bool Crosses(Point a)
        {
            if (object.ReferenceEquals(a, null))
            {
                throw new NullReferenceException();
            }

            return (Point.Distance(this.Centre, a) <= this.Radius) ? true : false;
        }

        public override string ToString()
        {
            return string.Format("Round with center on {0} with radius {1}, сircumference equals to {2:0.##}, area equals to {3:0.##}", this.Centre, this.Radius, this.Circumference, this.Area);
        }
    }
}