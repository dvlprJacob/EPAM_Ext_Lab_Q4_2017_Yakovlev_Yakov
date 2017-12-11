namespace Task05_4
{
    using System;
    using System.Collections.Generic;

    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("Демонстрация использования класса MyString :\n"
                + "------------------------------------------------------\n Массив из 3 экземпляров :");
            string s1 = new string("abcbbrrttb".ToCharArray());
            List<char> s2 = new List<char>("12345");
            char[] s3 = new char[] { 'a', 'b', 'c', 'd', 'e', 'F', 'G', '1', '0', 'e', 'n', 'd' };
            MyString a = new MyString(s1);
            MyString b = new MyString(s2);
            MyString c = new MyString(s3);
            Console.WriteLine("a = {0}\nb = {1}\nc = {2}", a, b, c);

            Console.WriteLine("Результат вызова :");
            Console.WriteLine("c.Replace('b','B') : {0}\n", c.Replace('b', 'B'));
            Console.WriteLine("a.Insert_to_the_begining('R') : {0}\n", a.Insert_to_the_begining('R'));
            Console.WriteLine("a.Insert_in_the_end('E') : {0}\n", a.Insert_in_the_end('E'));
            b.Reverse();
            Console.WriteLine("b.Reverse() : {0}\n", b);
            Console.WriteLine("c + b : {0}\n", c + b);
            Console.WriteLine("c.Remove(8,2) : {0}\n", c.Remove(8, 2));
            Console.WriteLine("a = {0}\nb = {1}\nc = {2}", a, b, c);
            Console.WriteLine("Press any key for exit");
            Console.ReadKey();
        }
    }
}