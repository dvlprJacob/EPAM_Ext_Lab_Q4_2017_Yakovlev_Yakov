namespace Task06_4
{
    using System;

    internal class Orange : Bonus
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

        public override DateTime Time_of_appearance
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public override void Affect(Player player)
        {
            throw new NotImplementedException();
        }

        public override bool I_fresh()
        {
            throw new NotImplementedException();
        }

        public override bool I_used
        {
            get
            {
                throw new NotImplementedException();
            }
        }
    }
}