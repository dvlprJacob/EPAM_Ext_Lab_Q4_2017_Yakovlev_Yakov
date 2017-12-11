using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task05_3
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("Демонстрация использования класса User :\n"
                + "------------------------------------------------------\n Массив из 4 экземпляров :");
            User[] users = new User[3];
            users[0] = new User("Jack", "Carter", "Second", new DateTime(1990, 12, 20));
            users[1] = new User("Liam", "Mitch", "Metiew", new DateTime(1950, 10, 3));
            users[2] = new User(users[1]);

            // не пройдет
            users[0].Set_birth_date(new DateTime(2020, 1, 1));

            foreach (var user in users)
            {
                Console.WriteLine("{0}\n", user);
            }

            Console.WriteLine("------------------------------------------------------\n"
                + "Агрегирование User в классе Worker :");
            Worker[] workers = new Worker[2];
            workers[0] = new Worker(users[0], "Driver", 12331, new DateTime(2010, 10, 10));
            workers[1] = new Worker(users[1], "HR-manager", 21344, new DateTime(2009, 6, 20));

            foreach (var worker in workers)
            {
                Console.WriteLine("{0}\n", worker);
            }

            Console.WriteLine("Press any key for exit");
            Console.ReadKey();
        }
    }
}