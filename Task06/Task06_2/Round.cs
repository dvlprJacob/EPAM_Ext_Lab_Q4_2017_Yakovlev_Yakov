namespace Task06_2
{
    using System;

    /// <summary>
    /// Представляет класс круг с центром в точке Centre и радиусом Radius
    /// </summary>
    internal class Round
    {
        private Point centre;

        private double radius;

        public Round()
        {
            this.centre = new Point();
            this.radius = 1;
        }

        public Round(Point centre, double radius)
        {
            if (radius <= 0 || object.ReferenceEquals(centre, null))
            {
                throw new ArgumentException();
            }

            this.Centre = new Point(centre);
            this.Radius = radius;
        }

        public Round(double radius)
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
            if (object.ReferenceEquals(sample, null))
            {
                throw new NullReferenceException();
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
        public double Radius
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
                var pi = Math.PI;
                return 2 * (pi * this.Radius);
            }
        }

        /// <summary>
        /// Площадь круга
        /// </summary>
        public double Area
        {
            get
            {
                var pi = Math.PI;
                var pow = Math.Pow(this.Radius, 2);
                return pi * pow;
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
            return string.Format("Round :\nCenter : {0}\nRadius  : {1}\nCircumference : {2:0.##}\nArea : {3:0.##}", this.Centre, this.Radius, this.Circumference, this.Area);
        }
    }
}