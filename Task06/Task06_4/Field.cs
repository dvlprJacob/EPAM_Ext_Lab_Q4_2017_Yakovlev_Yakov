namespace Task06_4
{
    using System;

    internal class Field
    {
        private int width;

        private int heigth;

        private Player player;

        /// <summary>
        /// Для потомков
        /// </summary>
        private Monster[] monsters;

        private Bonus[] bonuses;

        private Barrier[] barriers;

        public Field(int width, int heigth, Player player, Monster[] monsters, Bonus[] bonuses, Barrier[] barriers)
        {
        }

        public Field()
        {
        }

        public int Width
        {
            get
            {
                return this.width;
            }
        }

        public int Heigth
        {
            get
            {
                return this.heigth;
            }
        }

        public Player Game_player
        {
            get
            {
                return this.player;
            }
        }

        public Monster[] Monsters
        {
            get
            {
                return this.monsters;
            }
        }

        public Bonus[] Bonuses
        {
            get
            {
                return this.bonuses;
            }
        }

        public Barrier[] Barriers
        {
            get
            {
                return this.barriers;
            }
        }

        public int[,] Monster_location
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

        public int[,] Barriers_location
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

        public int[,] Bonuses_location
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

        public int Remaining_bonuses()
        {
            int remaining_bonuses = this.bonuses.Length;
            foreach (var bonus in this.bonuses)
            {
                if (bonus.I_used)
                {
                    remaining_bonuses--;
                }
            }

            return remaining_bonuses;
        }

        public bool Game_over
        {
            get
            {
                return (this.player.Health == 0) ? true : false;
            }
        }

        public bool Player_win
        {
            get
            {
                return (!this.Game_over && this.Remaining_bonuses() == 0) ? true : false;
            }
        }
    }
}