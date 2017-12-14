namespace Task06_3
{
    using System;

    /// <summary>
    /// Представляет класс круг с центром в точке Centre и радиусом Radius
    /// </summary>
    internal class Round : Circle
    {
        public Round()
            : base()
        {
        }

        public Round(Point centre, double radius)
            : base(centre, radius)
        {
        }

        public Round(double radius)
            : base(radius)
        {
        }

        public Round(Round sample)
            : base(sample)
        {
        }

        public new string Name
        {
            get
            {
                return "Round";
            }
        }

        /// <summary>
        /// Площадь круга
        /// </summary>
        public override double Area
        {
            get
            {
                var pow = Math.Pow(this.Radius, 2);
                return this.Pi * pow;
            }
        }

        public override string ToString()
        {
            return string.Format("{0} :\nCenter : {1}\nRadius  : {2}\nCircumference : {3:0.##}\nArea : {4:0.##}", this.Name, this.Centre, this.Radius, this.Circumference, this.Area);
        }
    }
}