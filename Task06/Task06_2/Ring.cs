namespace Task06_2
{
    using System;

    /// <summary>
    /// Представляет класс кольцо с внешним и внутренним радиусами
    /// </summary>
    internal class Ring : Round
    {
        private double innerRadius;

        public Ring()
            : base()
        {
            this.Inner_radius = 1;
        }

        public Ring(Round round, double inner_rad)
            : base(round)
        {
            if (inner_rad > round.Radius)
            {
                throw new ArgumentException();
            }

            this.innerRadius = inner_rad;
        }

        public Ring(double outer_rad, double inner_rad, int x, int y)
            : base(new Point(x, y), outer_rad)
        {
            if (inner_rad > outer_rad)
            {
                throw new ArgumentException();
            }

            this.innerRadius = inner_rad;
        }

        public Ring(double outer_rad, double inner_rad, Point center)
            : base(center, outer_rad)
        {
            if (inner_rad > outer_rad)
            {
                throw new ArgumentException();
            }

            this.innerRadius = inner_rad;
        }

        public double Inner_radius
        {
            get
            {
                return this.innerRadius;
            }

            set
            {
                if (value <= this.Radius)
                {
                    this.innerRadius = value;
                }
            }
        }

        public double Outer_radius
        {
            get
            {
                return this.Radius;
            }

            set
            {
                if (value >= this.innerRadius)
                {
                    this.Radius = value;
                }
            }
        }

        public new double Circumference
        {
            get
            {
                var outer_length = 2 * (Math.PI * this.Radius);
                var inner_length = 2 * (Math.PI * this.innerRadius);
                return outer_length + inner_length;
            }
        }

        public new double Area
        {
            get
            {
                var pow_outer = Math.Pow(this.Radius, 2);
                var pow_inner = Math.Pow(this.innerRadius, 2);
                var pi = Math.PI;
                return (pi * pow_outer) - (pi * pow_inner);
            }
        }

        public override string ToString()
        {
            return string.Format("Ring :with center on {0} with outer radius {1:0.##} and inner radius {2:0.##}, сircumference equals to {3:0.##}, area equals to {4:0.##}", this.Centre, this.Radius, this.innerRadius, this.Circumference, this.Area);
        }
    }
}