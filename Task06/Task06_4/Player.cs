namespace Task06_4
{
    using System;

    internal class Player : FieldObject, IMove, IAttack
    {
        private int health;
        private Player player;

        public Player(Player player)
        {
            this.player = player;
        }

        public int Health { get; set; }
        public int X { get; set; }

        public int Y { get; set; }

        public string Name
        {
            get
            {
                return this.name;
            }
        }

        public void Use(Bonus bonus)
        { }

        void IAttack.Deal_great_damage(Player player)
        {
            throw new NotImplementedException();
        }

        void IAttack.Inflicts_small_damage(Player palyer)
        {
            throw new NotImplementedException();
        }

        void IMove.MoveDown()
        {
            throw new NotImplementedException();
        }

        void IMove.MoveLeft()
        {
            throw new NotImplementedException();
        }

        void IMove.MoveRight()
        {
            throw new NotImplementedException();
        }

        void IMove.MoveUp()
        {
            throw new NotImplementedException();
        }
    }
}