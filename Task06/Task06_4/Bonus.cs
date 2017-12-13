namespace Task06_4
{
    using System;

    abstract internal class Bonus : FieldObject, IInfluenceThePlayer
    {
        public abstract int X { get; set; }

        public abstract int Y { get; set; }

        public abstract bool I_used { get; }

        /// <summary>
        /// Для реализации I_fresh
        /// </summary>
        public abstract DateTime Time_of_appearance { get; set; }

        public abstract bool I_fresh();

        public abstract void Affect(Player player);
    }
}