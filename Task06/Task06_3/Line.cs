namespace Task06_3
{
    using System;

    /// <summary>
    /// Представляет отрезок, заданный точкой начала и конца
    /// </summary>
    internal class Line : Figure
    {
        public Point start;
        public Point end;

        public Line()
        {
            this.Name = "Line";
            this.start = new Point();
            this.end = new Point(1, 1);
        }

        public Line(Point start, Point end)
        {
            if (object.ReferenceEquals(start, end) || object.ReferenceEquals(start, null))
            {
                throw new ArgumentException();
            }

            this.Name = "Line";
            this.start = start;
            this.end = end;
        }

        public Line(int x1, int y1, int x2, int y2)
        {
            if (x1 == x2 && y1 == y2)
            {
                throw new ArgumentException();
            }

            this.Name = "Line";
            this.start = new Point(x1, y1);
            this.end = new Point(x2, y2);
        }

        public Point Start_point
        {
            get
            {
                return this.start;
            }

            set
            {
                if (object.ReferenceEquals(value, null) || object.ReferenceEquals(value, this.end))
                {
                    throw new ArgumentException();
                }

                this.start = value;
            }
        }

        public Point End_point
        {
            get
            {
                return this.end;
            }

            set
            {
                if (object.ReferenceEquals(value, null) || object.ReferenceEquals(value, this.start))
                {
                    throw new ArgumentException();
                }

                this.end = value;
            }
        }

        /// <summary>
        /// Расстояние от начала до конца
        /// </summary>
        public double Length
        {
            get
            {
                return Point.Distance(this.Start_point, this.End_point);
            }
        }

        public override string ToString()
        {
            return string.Format("{0} with the origin at {1} and end at {2}, length = {3}", this.Name, this.Start_point, this.End_point, this.Length);
        }
    }
}