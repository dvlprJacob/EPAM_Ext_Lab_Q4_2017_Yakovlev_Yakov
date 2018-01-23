namespace Task08_2
{
    using System;

    public class Person : IEmployee
    {
        public Person(string name)
        {
            this.Name = name;
        }

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
                if (person.CameIn.Hour < 12)//todo pn хардкод
                {
                    this.GreetString = string.Format("Good morning, {0}, said {1}", person.Name, this.Name);//todo pn хардкод
					return;
                }

                if (person.CameIn.Hour <= 17)//todo pn хардкод
				{
                    this.GreetString = string.Format("Good day, {0}, said {1}", person.Name, this.Name);//todo pn хардкод
					return;
                }
                else
                {
                    this.GreetString = string.Format("Good evening, {0}, said {1}", person.Name, this.Name);//todo pn хардкод
					return;
                }
            }
            catch (Exception ex)
            {
                this.GreetString += string.Format("{0} on Person.Greet", ex.Message);//todo pn хардкод +- 
				//(вообще, по-правильному генерировать собственное исключение и заполнять в нём строку сообщение, а ему передавать только параметры для этой строки, НО не всю строку целиком)
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
                this.BidFarewellString = string.Format("Goodbye,{0}, said {1}", person.Name, this.Name);//todo pn хардкод
			}
            catch (Exception ex)
            {
                this.BidFarewellString = string.Format("{0} on Person.BidFarewell", ex.Message);//todo pn хардкод +-
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
                this.Event = string.Format("[ {0} came at {1}]", this.Name, cameTime.Hour);//todo pn хардкод

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
                this.Event += string.Format("{0} on Person.Came", ex.Message);//todo pn хардкод +-
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
                    this.Event = string.Format("[ {0} gone home ]", this.Name);//todo pn хардкод
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
                this.Event += string.Format("{0} on Person.Leave", ex.Message);//todo pn хардкод
				return;
            }
        }
    }
}