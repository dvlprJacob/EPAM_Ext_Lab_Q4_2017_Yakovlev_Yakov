namespace Task08_2
{
    using System;

    /// <summary>
    /// Тест Task08_2
    /// </summary>
    internal class Program
    {
        private static void Main(string[] args)
        {
            Person p1 = new Person("Sam");
            Person p2 = new Person("Tom");
            Person p3 = new Person("Shon");

            p1.Came(new DateTime(2016, 12, 11, 9, 35, 0));
            Console.WriteLine(p1.Event);

            p2.Came(new DateTime(2016, 12, 11, 13, 15, 0));
            Console.WriteLine(p2.Event);
            Console.WriteLine(p1.GreetString);

            p3.Came(new DateTime(2016, 12, 11, 19, 0, 0));
            Console.WriteLine(p3.Event);
            Console.WriteLine(p1.GreetString);
            Console.WriteLine(p2.GreetString);

            p1.Leave();
            Console.WriteLine(p1.Event);
            Console.WriteLine(p2.BidFarewellString);
            Console.WriteLine(p3.BidFarewellString);

            p3.Leave();
            Console.WriteLine(p3.Event);
            Console.WriteLine(p2.BidFarewellString);

            p2.Leave();
            Console.WriteLine(p2.Event);
            // Empty strings
            Console.WriteLine(p2.GreetString);
            Console.WriteLine(p2.BidFarewellString);

            Console.ReadKey();
        }
    }
}