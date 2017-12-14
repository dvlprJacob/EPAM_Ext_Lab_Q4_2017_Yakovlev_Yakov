namespace Task06
{
    using System;

    /// <summary>
    /// Представляет сущность Человек, представленный именем, фамилией, отчеством, датой рождения
    /// </summary>
    internal class User
    {
        private string f_name;

        private string s_name;

        private string patronymic;

        private DateTime b_date;

        public User()
        {
        }

        /// <summary>
        /// Инициализирует новый экземпляр с заданным именем, фамилией, отчеством и датой рождения
        /// </summary>
        /// <param name="f_name"> Имя</param>
        /// <param name="s_name"> Фамилия</param>
        /// <param name="patronymic"> Отчество</param>
        /// <param name="b_date"> Дата рождения</param>
        public User(string f_name, string s_name, string patronymic, DateTime b_date)
        {
            if (string.IsNullOrEmpty(f_name) || string.IsNullOrEmpty(s_name) || string.IsNullOrEmpty(patronymic) || object.ReferenceEquals(b_date, null))
            {
                throw new ArgumentNullException();
            }

            this.First_name = f_name;
            this.Second_name = s_name;
            this.Patronymic = patronymic;
            this.b_date = b_date;
        }

        public User(User user)
        {
            if (object.ReferenceEquals(user, null))
            {
                throw new NullReferenceException();
            }

            this.First_name = user.First_name;
            this.Second_name = user.Second_name;
            this.Patronymic = user.Patronymic;
            this.b_date = user.b_date;
        }

        /// <summary>
        /// Имя
        /// </summary>
        public string First_name
        {
            get
            {
                return this.f_name;
            }

            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    this.f_name = value;
                }
            }
        }

        /// <summary>
        /// Фамилия
        /// </summary>
        public string Second_name
        {
            get
            {
                return this.s_name;
            }

            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    this.s_name = value;
                }
            }
        }

        /// <summary>
        /// Отчество
        /// </summary>
        public string Patronymic
        {
            get
            {
                return this.patronymic;
            }

            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    this.patronymic = value;
                }
            }
        }

        /// <summary>
        /// Возраст
        /// </summary>
        public int Age
        {
            get
            {
                DateTime date_now = DateTime.Now;
                if (date_now.Year.CompareTo(this.b_date.Year) == 0)
                {
                    return 0;
                }

                int year = date_now.Year - this.b_date.Year;
                if (date_now.Month < this.b_date.Month
                    || (date_now.Month == this.b_date.Month
                    && date_now.Day < this.b_date.Day))
                {
                    year++;
                }

                return year;
            }
        }

        public DateTime Get_birth_date()
        {
            return this.b_date;
        }

        public override string ToString()
        {
            return string.Format("{0} {1} {2}\nBirth date : {3:D}\nAge : {4}", this.First_name, this.Second_name, this.Patronymic, this.b_date.Date, this.Age);
        }

        protected void Set_birth_date(DateTime bdate)
        {
            if (bdate.CompareTo(DateTime.Now) == -1)
            {
                this.b_date = bdate;
            }
        }
    }
}