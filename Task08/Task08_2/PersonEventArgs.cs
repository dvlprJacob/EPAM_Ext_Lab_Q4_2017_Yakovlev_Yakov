namespace Task08_2
{
    using System;

    public class PersonEventArgs : EventArgs
    {
        private DateTime cameIn;

        public PersonEventArgs(string name, DateTime cameTime)
        {
            this.Name = name;
            this.CameIn = cameTime;
        }

        public string Name { get; set; }

        public DateTime CameIn
        {
            get
            {
                return this.cameIn;
            }

            set
            {
                if (DateTime.Compare(value, DateTime.Now) <= 0)
                {
                    this.cameIn = value;
                }
            }
        }
    }
}