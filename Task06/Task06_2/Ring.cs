namespace Task06_2
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
            this.Inner_radius = 0.9;
        }

        public Ring(Round round, double inner_rad)
            : base(round)
        {
            /// Не уверен, но здесь и в остальных конструкторах скорее всего вызов конструктора базового класса нецелеесобразен,
            /// так как есть условие, что внутренний радиус должен быть меньше или равен внешнему.
            if (inner_rad > round.Radius)
            {
                throw new ArgumentException();
            }

            this.inner_radius = inner_rad;
        }

        public Ring(double outer_rad, double inner_rad, int x, int y)
            : base(new Point(x, y), outer_rad)
        {
            if (inner_rad > outer_rad)
            {
                throw new ArgumentException();
            }

            this.inner_radius = inner_rad;
        }

        public Ring(double outer_rad, double inner_rad, Point center)
            : base(center, outer_rad)
        {
            if (inner_rad > outer_rad)
            {
                throw new ArgumentException();
            }

            this.inner_radius = inner_rad;
        }

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

        public new double Circumference
        {
            get
            {
                var outer_length = 2 * (Math.PI * this.Radius);
                var inner_length = 2 * (Math.PI * this.inner_radius);
                return outer_length + inner_length;
            }
        }

        public new double Area
        {
            get
            {
                var pow_outer = Math.Pow(this.Radius, 2);
                var pow_inner = Math.Pow(this.inner_radius, 2);
                var pi = Math.PI;
                return (pi * pow_outer) - (pi * pow_inner);
            }
        }

        public override string ToString()
        {
            return string.Format("Ring :with center on {0} with outer radius {1:0.##} and inner radius {2:0.##}, сircumference equals to {3:0.##}, area equals to {4:0.##}",
                this.Centre, this.Radius, this.Inner_radius, this.Circumference, this.Area);
        }
    }
}