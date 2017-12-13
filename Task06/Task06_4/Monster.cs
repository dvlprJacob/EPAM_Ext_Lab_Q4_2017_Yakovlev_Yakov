namespace Task06_4
{
    abstract internal class Monster : FieldObject, IAttack, IMove
    {
        public int health;
        public abstract int X { get; set; }

        public abstract int Y { get; set; }

        public abstract bool I_alive();

        public abstract void Deal_great_damage(Player player);

        public abstract void Inflicts_small_damage(Player palyer);

        public abstract void MoveDown();

        public abstract void MoveLeft();

        public abstract void MoveRight();

        public abstract void MoveUp();
    }
}