namespace Task09_3
{
    using System;
    using System.Diagnostics;

    /// <summary>
    /// Сравнение скорости работы поиска разными способами
    /// </summary>
    internal class Program
    {
        public static void Print(string[] array, string searchWay, Stopwatch sw)
        {
            Console.WriteLine("{0} , time {1} msec, elapsed {2} :", searchWay, sw.ElapsedMilliseconds, sw.Elapsed);
            foreach (var elem in array)
            {
                Console.Write("{0}  ", elem);
            }

            Console.WriteLine("\n");
        }

        private static void Main(string[] args)
        {
            MyStringArray temp = new MyStringArray(new string[] { "12", "-1", "S2213", "1.2", "32", "123442",
            "12e", "-1", "02213", "152", "322", "123-90sf",
            "123", "12", "02Fdg3", "152756", "fdgHHJBBKJHLdfjjj", "64" });

            // Изменим приоритет текущего процесса на высокий
            Process ps = Process.GetCurrentProcess();
            ps.PriorityClass = ProcessPriorityClass.RealTime;

            Stopwatch sw = new Stopwatch();
            sw.Start();
            var res1 = temp.FindAllPositiveNumberStrings();
            sw.Stop();
            Print(res1, "Search results by simple search", sw);

            sw.Restart();
            var res2 = temp.FindAllPositiveNumberByDelegate();
            Print(res1, "Search results by delegate", sw);

            sw.Restart();
            var res3 = temp.FindAllPositiveNumberByAnonymousDelegate();
            Print(res1, "Search results by anonymous delegate", sw);

            sw.Restart();
            var res4 = temp.FindAllPositiveNumberByLymbdaExtensionDelegate();
            Print(res1, "Search results by lymbda extension delegate", sw);

            sw.Restart();
            var res5 = temp.FindAllPositiveNumberStringsByLinq();
            sw.Stop();
            Print(res1, "Search results by LINQ", sw);
            Console.WriteLine("Press any key for exit . . . ");

            Console.ReadKey();
        }
    }
}