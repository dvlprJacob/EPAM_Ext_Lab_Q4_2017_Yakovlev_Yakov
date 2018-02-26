namespace Task08_2
{
    using System;
    using NLog;

    public class Person : IEmployee
    {
        public Person(string name)
        {
            this.Name = name;
        }

        private static Logger logger = LogManager.GetCurrentClassLogger();

        public static event EventHandler<PersonEventArgs> GreetEvent;

        public static event EventHandler<PersonEventArgs> BidFarewellEvent;

        public string Name { get; set; }

        /// <summary>
        /// Свойство представляет действие [пришел/ушел]
        /// </summary>
        public string Event { get; set; }

        /// <summary>
        /// Строка приветствия
        /// </summary>
        public string GreetString { get; private set; }

        /// <summary>
        /// Строка прощания
        /// </summary>
        public string BidFarewellString { get; private set; }

        /// <summary>
        /// Формирует строку приветствия в зависимости от времени прибытия на работу
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="person"> Пришедший работник</param>
        public void Greet(object sender, PersonEventArgs person)
        {
            try
            {
                if (person.CameIn.Hour < Convert.ToInt32(Resources.MorningTimeMax))
                {
                    int b = 1 - 1;
                    int a = 10 / b;
                    this.GreetString = $"{Resources.GreetingMorning}, {person.Name}, {Resources.SaidVerb} {this.Name}";
                    return;
                }

                if (person.CameIn.Hour <= Convert.ToInt32(Resources.DayTimeMax))
                {
                    this.GreetString = $"{Resources.GreetingDay}, {person.Name}, {Resources.SaidVerb} {this.Name}";
                    return;
                }
                else
                {
                    int b = 1 - 1;
                    int a = 10 / b;
                    this.GreetString = $"{Resources.GreetingEvening}, {person.Name}, {Resources.SaidVerb} {this.Name}";
                    return;
                }
            }
            catch (Exception ex)
            {
                logger.Debug(ex.ToString());
                return;
            }
        }

        /// <summary>
        /// Формирует строку прощания
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="person"> Ушедший домой работник</param>
        public void BidFarewell(object sender, PersonEventArgs person)
        {
            try
            {
                this.BidFarewellString = $"{Resources.BidFarewell} {person.Name}, {Resources.SaidVerb} {this.Name}";
            }
            catch (Exception ex)
            {
                logger.Debug(ex.ToString());
            }
        }

        /// <summary>
        /// Работник пришел на работу
        /// </summary>
        /// <param name="cameTime"> Время прибытия</param>
        public void Came(DateTime cameTime)
        {
            try
            {
                this.Event = string.Format($"{this.Name} {Resources.CameEvent} {cameTime.Hour}");//todo pn хардкод

                if (GreetEvent != null)
                {
                    PersonEventArgs personEvent = new PersonEventArgs(this.Name, cameTime);
                    GreetEvent(this, personEvent);
                }

                GreetEvent += this.Greet;
                BidFarewellEvent += this.BidFarewell;
            }
            catch (Exception ex)
            {
                logger.Debug(ex.ToString());
                return;
            }
        }

        /// <summary>
        /// Работник ушел домой
        /// </summary>
        public void Leave()
        {
            try
            {
                if (BidFarewellEvent != null)
                {
                    this.Event = string.Format($"{this.Name} {Resources.GoneHomeEvent}");

                    // Очищаем строки приветствия и прощания
                    this.GreetString = string.Empty;
                    this.BidFarewellString = string.Empty;

                    PersonEventArgs personEvent = new PersonEventArgs(this.Name, DateTime.Now);

                    GreetEvent -= this.Greet;
                    BidFarewellEvent -= this.BidFarewell;

                    BidFarewellEvent?.Invoke(this, personEvent);
                }
            }
            catch (Exception ex)
            {
                logger.Debug(ex.ToString());
                return;
            }
        }
    }
}