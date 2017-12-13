namespace Task06_3
{
    using System;

    /// <summary>
    /// Представляет класс кольцо с внешним и внутренним радиусами
    /// </summary>
    internal class Ring : Round
    {
        private double inner_radius;

        public Ring()
            : base()
        {
            this.Name = "Ring";
            this.Inner_radius = 0.9;
        }

        public Ring(Round round, double inner_rad)
        {
            if (object.ReferenceEquals(round, null))
            {
                throw new NullReferenceException();
            }

            if (inner_rad > round.Radius)
            {
                throw new ArgumentException();
            }

            this.Name = "Ring";
            this.Centre = round.Centre;
            this.Outer_radius = round.Radius;
            this.inner_radius = inner_rad;
        }

        public Ring(double outer_rad, double inner_rad, int x, int y)
        {
            if (inner_rad > outer_rad)
            {
                throw new ArgumentException();
            }

            this.Name = "Ring";
            this.Centre = new Point(x, y);
            this.Outer_radius = outer_rad;
            this.inner_radius = inner_rad;
        }

        public Ring(double outer_rad, double inner_rad, Point center)
        {
            if (inner_rad > outer_rad)
            {
                throw new ArgumentException();
            }

            this.Name = "Ring";
            this.Centre = new Point(center);
            this.Outer_radius = outer_rad;
            this.inner_radius = inner_rad;
        }

        /// <summary>
        /// Внутренний радиус
        /// </summary>
        public double Inner_radius

        {
            get
            {
                return inner_radius;
            }

            set
            {
                if (value <= this.Radius)
                {
                    this.inner_radius = value;
                }
            }
        }

        /// <summary>
        /// Внешний радиус
        /// </summary>
        public double Outer_radius
        {
            get
            {
                return this.Radius;
            }

            set
            {
                if (value >= inner_radius)
                {
                    this.Radius = value;
                }
            }
        }

        /// <summary>
        /// Площадь
        /// </summary>
        public override double Area
        {
            get
            {
                var pow_outer = Math.Pow(this.Radius, 2);
                var pow_inner = Math.Pow(this.inner_radius, 2);
                return (this.Pi * pow_outer) - (this.Pi * pow_inner);
            }
        }

        /// <summary>
        /// Проверяет, входит ли точка во внутренную окружность
        /// </summary>
        /// <param name="a"> Точка</param>
        /// <returns></returns>
        public new bool Crosses(Point a)
        {
            if (object.ReferenceEquals(a, null))
            {
                throw new NullReferenceException();
            }

            return (Point.Distance(this.Centre, a) <= this.Inner_radius) ? true : false;
        }

        public override string ToString()
        {
            return string.Format("{0} :with center on {1} with outer radius {2:0.##} and inner radius {3:0.##},"
                + "\nсircumference equals to {4:0.##}, area equals to {5:0.##}",
                this.Name, this.Centre, this.Radius, this.Inner_radius, this.Circumference, this.Area);
        }
    }
}