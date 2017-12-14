namespace Task06_4
{
    using System;

    internal class Bear : Monster
    {
        public int Health
        {
            get;
        }

        public override int X
        {
            get;
            set;
        }

        public override int Y { get; set; }

        public string Name
        {
            get
            {
                return this.name;
            }
        }

        public override void Deal_great_damage(Player player)
        {
            throw new NotImplementedException();
        }

        public override void Inflicts_small_damage(Player palyer)
        {
            throw new NotImplementedException();
        }

        public override void MoveDown()
        {
            throw new NotImplementedException();
        }

        public override void MoveLeft()
        {
            throw new NotImplementedException();
        }

        public override void MoveRight()
        {
            throw new NotImplementedException();
        }

        public override void MoveUp()
        {
            throw new NotImplementedException();
        }

        public override bool I_alive()
        {
            throw new NotImplementedException();
        }
    }
}