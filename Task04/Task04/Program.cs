using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task04
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            string text = string.Empty;
            while (text != "0")
            {
                Console.WriteLine("Input text for calculate average length of words, 0 for exit");
                text = Console.ReadLine();
                switch (text)
                {
                    case "":
                        Console.WriteLine("You input empty string, try again");
                        break;

                    case "0":
                        Console.WriteLine("Press any key for exit");
                        Console.ReadKey();
                        return;

                    default:
                        MyString obj = new MyString(text);
                        Console.WriteLine("Average length of words :{0}", obj.Average_word_length());
                        break;
                }
            }
        }
    }
}