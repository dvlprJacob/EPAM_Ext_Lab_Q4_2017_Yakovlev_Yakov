namespace Task05_3
{
    using System;

    /// <summary>
    /// Представляет сущность работник, представленный экземпляром User, должностью, зароботной платой и датой принятия на работу
    /// </summary>
    internal class Worker
    {
        public Worker()
        {
        }

        /// <summary>
        /// Инициализирует новый экземпляр заданным экземпляром User, должностью, зароботной платой и датой принятия на работу
        /// </summary>
        /// <param name="user"> "Экземпляр класса User</param>
        /// <param name="position"> Должность</param>
        /// <param name="salary"> Зароботная плата</param>
        /// <param name="empl_date"> Дата принятия на работу</param>
        public Worker(User user, string position, int salary, DateTime empl_date)
        {
            if (object.ReferenceEquals(user, null) || object.ReferenceEquals(empl_date, null))
            {
                throw new NullReferenceException();
            }

            if (string.IsNullOrEmpty(position) || (salary <= 0)
                || empl_date.CompareTo(user.Get_birth_date()) <= 0)
            {
                throw new ArgumentException();
            }

            this.person = new User(user);
            this.position = position;
            this.salary = salary;
            this.employment_date = empl_date;
        }

        /// <summary>
        /// Представляет экземпляр класса User
        /// </summary>
        public User person { get; set; }

        /// <summary>
        /// Должность
        /// </summary>
        private string position { get; set; }

        /// <summary>
        /// Зароботная плата
        /// </summary>
        private int salary { get; set; }

        /// <summary>
        /// Дата принятия на работу
        /// </summary>
        private DateTime employment_date { get; set; }

        public User Get_user()
        {
            return this.person;
        }

        public void Set_user(User user)
        {
            if (object.ReferenceEquals(user, null))
            {
                throw new NullReferenceException();
            }

            this.person = user;
        }

        public int Get_salary()
        {
            return this.salary;
        }

        public void Set_salary(int salary)
        {
            if (salary <= 0)
            {
                throw new ArgumentException();
            }

            this.salary = salary;
        }

        public DateTime Get_employment_date()
        {
            return this.employment_date;
        }

        public void Set_employment_date(DateTime empl_date)
        {
            if (object.ReferenceEquals(empl_date, null))
            {
                throw new NullReferenceException();
            }

            this.employment_date = empl_date;
        }

        public override string ToString()
        {
            return string.Format("{0}\nEmployment date : {1:D}\nPosition : {2}\nSalary : {3}", this.person, this.employment_date, this.position, this.salary);
        }
    }
}