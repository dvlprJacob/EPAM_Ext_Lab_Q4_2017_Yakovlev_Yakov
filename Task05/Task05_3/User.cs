namespace Task05_3
{
    using System;

    /// <summary>
    /// Представляет сущность Человек, представленный именем, фамилией, отчеством, датой рождения
    /// </summary>
    internal class User
    {
        private string fName;

        private string sName;

        private string patronymic;

        private DateTime bDate;

        public User()
        {
        }

        /// <summary>
        /// Инициализирует новый экземпляр с заданным именем, фамилией, отчеством и датой рождения
        /// </summary>
        /// <param name="fName"> Имя</param>
        /// <param name="sName"> Фамилия</param>
        /// <param name="patronymic"> Отчество</param>
        /// <param name="bDate"> Дата рождения</param>
        public User(string fName, string sName, string patronymic, DateTime bDate)
        {
            if (string.IsNullOrEmpty(fName) || string.IsNullOrEmpty(sName) || string.IsNullOrEmpty(patronymic) || object.ReferenceEquals(bDate, null))
            {
                throw new ArgumentNullException();
            }

            this.First_name = fName;
            this.Second_name = sName;
            this.Patronymic = patronymic;
            this.bDate = bDate;
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
            this.bDate = user.bDate;
        }

        /// <summary>
        /// Имя
        /// </summary>
        public string First_name
        {
            get
            {
                return this.fName;
            }

            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    this.fName = value;
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
                return this.sName;
            }

            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    this.sName = value;
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
                if (date_now.Year.CompareTo(this.bDate.Year) == 0)
                {
                    return 0;
                }

                int year = date_now.Year - this.bDate.Year;
                if (date_now.Month < this.bDate.Month
                    || (date_now.Month == this.bDate.Month
                    && date_now.Day < this.bDate.Day))
                {
                    year++;
                }

                return year;
            }
        }

        public void Set_birth_date(DateTime bdate)
        {
            if (bdate.CompareTo(DateTime.Now) == -1)
            {
                this.bDate = bdate;
            }
        }

        public DateTime Get_birth_date()
        {
            return this.bDate;
        }

        public override string ToString()
        {
            return string.Format("{0} {1} {2}\nBirth date : {3:D}\nAge :    {4}", this.First_name, this.Second_name, this.Patronymic, this.bDate.Date, this.Age);
        }
    }
}