namespace Task05_3
{
    using System;

    /// <summary>
    /// Представляет сущность работник, представленный экземпляром User, должностью, зароботной платой и датой принятия на работу
    /// </summary>
    internal class Worker// В ТЗ не было задания создания класса Worker или Employee, я по своей инициативе реализовал данный класс для демонстрации использования класса User (что было  в ТЗ)  посредством агрегации
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

            this.Person = new User(user);
            this.Position = position;
            this.Salary = salary;
            this.Employment_date = empl_date;
        }

        /// <summary>
        /// Представляет экземпляр класса User
        /// </summary>
        public User Person { get; set; }

        /// <summary>
        /// Должность
        /// </summary>
        private string Position { get; set; }

        /// <summary>
        /// Зароботная плата
        /// </summary>
        private int Salary { get; set; }

        /// <summary>
        /// Дата принятия на работу
        /// </summary>
        private DateTime Employment_date { get; set; }

        public User Get_user()
        {
            return this.Person;
        }

        public void Set_user(User user)
        {
            if (object.ReferenceEquals(user, null))
            {
                throw new NullReferenceException();
            }

            this.Person = user;
        }

        public int Get_salary()
        {
            return this.Salary;
        }

        public void Set_salary(int salary)
        {
            if (salary > 0)
            {
                this.Salary = salary;
            }
        }

        public DateTime Get_employment_date()
        {
            return this.Employment_date;
        }

        public void Set_employment_date(DateTime empl_date)
        {
            if (object.ReferenceEquals(empl_date, null))
            {
                throw new NullReferenceException();
            }

            if (empl_date.CompareTo(this.Person.Get_birth_date()) <= 0)
            {
                throw new ArgumentException();
            }

            this.Employment_date = empl_date;
        }

        public override string ToString()
        {
            return string.Format("{0}\nEmployment date : {1:D}\nPosition : {2}\nSalary : {3}", this.Person, this.Employment_date, this.Position, this.Salary);
        }
    }
}