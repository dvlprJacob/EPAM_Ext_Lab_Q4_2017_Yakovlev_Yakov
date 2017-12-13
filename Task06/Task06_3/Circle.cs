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
            this.Name = "Circle";
            this.centre = new Point();
            this.radius = 1;
        }

        public Circle(Point centre, double radius)
        {
            if (radius <= 0 || object.ReferenceEquals(centre, null))
            {
                throw new ArgumentException();
            }

            this.Name = "Circle";
            this.Centre = new Point(centre);
            this.Radius = radius;
        }

        public Circle(double radius)
        {
            if (radius <= 0)
            {
                throw new ArgumentException();
            }

            this.Name = "Circle";
            this.centre = new Point();
            this.Radius = radius;
        }

        public Circle(Round sample)
        {
            if (object.ReferenceEquals(sample, null))
            {
                throw new NullReferenceException();
            }

            this.Name = "Circle";
            this.Centre = sample.Centre;
            this.Radius = sample.Radius;
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

            return (Point.Distance(this.Centre, a) <= this.Radius) ? true : false;
        }

        public virtual double Area { get; }

        public override string ToString()
        {
            return string.Format("{0} :\nCenter : {1}\nRadius  : {2}\nCircumference : {3:0.##}",
                this.Name, this.Centre, this.Radius, this.Circumference);
        }
    }
}