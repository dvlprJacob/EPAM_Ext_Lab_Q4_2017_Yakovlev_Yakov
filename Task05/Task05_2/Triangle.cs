namespace Task05_2
{
    using System;

    /// <summary>
    /// Представляет треугольник длинами сторон
    /// </summary>
    internal class Triangle
    {
        private double sideA;

        private double sideB;

        private double sideC;

        /// <summary>
        /// Инициализирует пустой экземпляр равнобедренного треугольника
        /// </summary>
        public Triangle()
        {
        }

        /// <summary>
        /// Инициализирует новый экземпляр с заданными сторонами
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="c"></param>
        public Triangle(double a, double b, double c)
        {
            if (a <= 0 || b <= 0 || c <= 0)
            {
                throw new ArgumentException("Sides must be more than zero");
            }

            if (a > b + c || b > a + c || c > a + b)
            {
                throw new ArgumentException("Sides do not satisfy the triangle inequality");
            }

            this.A = a;
            this.B = b;
            this.C = c;
        }

        public Triangle(Triangle abc)
        {
            if (object.ReferenceEquals(abc, null))
            {
                throw new NullReferenceException();
            }

            this.A = abc.A;
            this.B = abc.B;
            this.C = abc.C;
        }

        /// <summary>
        /// Сторона треугольника
        /// </summary>
        public double A
        {
            get
            {
                return this.sideA;
            }

            set
            {
                if ((value > 0) && (value <= this.B + this.C))
                {
                    this.sideA = value;
                }
            }
        }

        /// <summary>
        /// Сторона треугольника
        /// </summary>
        public double B
        {
            get
            {
                return this.sideB;
            }

            set
            {
                if ((value > 0) && (value <= this.A + this.C))
                {
                    this.sideB = value;
                }
            }
        }

        /// <summary>
        /// Сторона треугольника
        /// </summary>
        public double C
        {
            get
            {
                return this.sideC;
            }

            set
            {
                if ((value > 0) && (value <= this.A + this.B))
                {
                    this.sideC = value;
                }
            }
        }

        /// <summary>
        /// Периметр треугольника
        /// </summary>
        public double Perimeter
        {
            get
            {
                return this.A + this.B + this.C;
            }
        }

        /// <summary>
        /// Площадь треугольника
        /// </summary>
        public double Area
        {
            get
            {
                var p = this.Perimeter;
                return Math.Sqrt(p * (p - this.A) * (p - this.B) * (p - this.C));
            }
        }

        public override string ToString()
        {
            return string.Format("Triangle with sides : {0:0.##},{1:0.##},{2:0.##}", this.A, this.B, this.C);
        }
    }
}