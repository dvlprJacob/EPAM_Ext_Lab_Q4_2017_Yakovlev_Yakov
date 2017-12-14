namespace Task06_3
{
    using System;

    /// <summary>
    /// Представляет класс окружность точкой центра и радиусом
    /// </summary>
    internal class Circle : Figure
    {
        private Point centre;

        private double radius;

        public Circle()
        {
            this.centre = new Point();
        }

        public Circle(Point centre, double radius)
        {
            if (radius <= 0 || object.ReferenceEquals(centre, null))
            {
                throw new ArgumentException();
            }

            this.centre = new Point(centre);
            this.radius = radius;
        }

        public Circle(double radius)
        {
            if (radius <= 0)
            {
                throw new ArgumentException();
            }

            this.centre = new Point();
            this.radius = radius;
        }

        public Circle(Round sample)
        {
            if (object.ReferenceEquals(sample, null))
            {
                throw new NullReferenceException();
            }

            this.centre = sample.centre;
            this.radius = sample.radius;
        }

        public new string Name
        {
            get
            {
                return "Circle";
            }
        }

        /// <summary>
        /// Точка центра
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
                    this.centre = value;
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

        public double Pi
        {
            get
            {
                return Math.PI;
            }
        }

        /// <summary>
        /// Длина окружности
        /// </summary>
        public double Circumference
        {
            get
            {
                return 2 * (this.Pi * this.Radius);
            }
        }

        public virtual double Area { get; }

        /// <summary>
        /// Проверяет, входит ли точка в окружность
        /// </summary>
        /// <param name="a"> Точка</param>
        /// <returns></returns>
        public bool Crosses(Point a)
        {
            if (object.ReferenceEquals(a, null))
            {
                throw new NullReferenceException();
            }

            return (Point.Distance(this.centre, a) <= this.radius) ? true : false;
        }

        public override string ToString()
        {
            return string.Format("{0} :\nCenter : {1}\nRadius  : {2}\nCircumference : {3:0.##}", this.Name, this.centre, this.radius, this.Circumference);
        }
    }
}