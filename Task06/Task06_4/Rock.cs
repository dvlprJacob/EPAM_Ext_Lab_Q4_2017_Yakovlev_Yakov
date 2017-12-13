namespace Task06_4
{
    internal class Rock : Barrier
    {
        public override int X { get; set; }

        public override int Y { get; set; }

        public string Name
        {
            get
            {
                return this.name;
            }
        }
    }
}