namespace Task06
{
    using System;
    using Itenso.TimePeriod;

    /// <summary>
    /// Класс представляет сущность Сотрудник, представленный агрегированным экземпляром класса User,
    /// должностью, датой принятия на работу и стажем работы
    /// </summary>
    internal class Employee : User
    {
        /// <summary>
        /// Должность
        /// </summary>
        private string position;

        /// <summary>
        /// Дата принятия на работу
        /// </summary>
        private DateTime employment_date;

        /// <summary>
        /// Инициализирует новый экземпляр заданным объектом User, должностью и датой принятия на работу
        /// </summary>
        /// <param name="user"> Экземпляр класса User</param>
        /// <param name="position"> Должность</param>
        /// <param name="empl_date"> Дата принятия на работу</param>
        public Employee(User user, string position, DateTime empl_date)
            : base(user)
        {
            if (object.ReferenceEquals(empl_date, null))
            {
                throw new NullReferenceException();
            }

            this.Set_position(position);
            this.Set_employment_date(empl_date);
        }

        /// <summary>
        /// Инициализирует новый экземпляр с заданными параметрами
        /// </summary>
        /// <param name="f_name"> Имя</param>
        /// <param name="s_name"> Фамилия</param>
        /// <param name="patronymic"> Отчество</param>
        /// <param name="b_date"> Дата рождения</param>
        /// <param name="position"> Должность</param>
        /// <param name="empl_date"> Дата принятия на работу</param>
        public Employee(string f_name, string s_name, string patronymic, DateTime b_date, string position, DateTime empl_date)
            : base(f_name, s_name, patronymic, b_date)
        {
            this.Set_position(position);
            this.Set_employment_date(empl_date);
        }

        /// <summary>
        /// Стаж работы
        /// </summary>
        public string Experience
        {
            get
            {
                DateDiff date_diff = new DateDiff(this.employment_date, DateTime.Now);
                return string.Format("{0:D}", date_diff.GetDescription(3).ToString());
            }
        }

        /// <summary>
        /// Изменяет дату принятия на заданную
        /// </summary>
        /// <param name="empl_date"> Дата принятия на работу</param>
        public void Set_employment_date(DateTime empl_date)
        {
            if (empl_date.CompareTo(this.Get_birth_date()) <= 0)
            {
                throw new ArgumentException();
            }

            this.employment_date = empl_date;
        }

        /// <summary>
        /// Возвращает дату принятия на работу
        /// </summary>
        /// <returns></returns>
        public DateTime Get_employment_date()
        {
            return this.employment_date;
        }

        /// <summary>
        /// Изменяет должность на заданную
        /// </summary>
        /// <param name="position"> Должность</param>
        public void Set_position(string position)
        {
            if (string.IsNullOrEmpty(position))
            {
                throw new ArgumentException();
            }

            this.position = position;
        }

        /// <summary>
        /// Возвращает должность
        /// </summary>
        /// <returns></returns>
        public string Get_position()
        {
            return this.position;
        }

        public override string ToString()
        {
            return string.Format("{0}\nPosition : {1}\nEmploument date : {2:D}\nExperience : {3}", base.ToString(),
                this.position, this.employment_date, this.Experience);
        }
    }
}