namespace Task04
{
    using System;

    internal class Program
    {
        private const string exit = "0";
        private const string empty = "";

        private static void Main(string[] args)
        {
            string text = string.Empty;

            while (text != exit)
            {
                Console.WriteLine("Input text for calculate average length of words, 0 for exit");
                text = Console.ReadLine();
                switch (text)
                {
                    case empty:
                        Console.WriteLine("You input empty string, try again");
                        break;

                    case exit:
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

/*todo pn

------ StyleCop 5.0 (build 5.0.6419.0) started ------

Pass 1:   Task04 - \MyString.cs
Pass 1:   Task04 - \Program.cs
Pass 1:   Task04 - \Properties\AssemblyInfo.cs
Pass 1:   Task04_2 - \Program.cs
Pass 1:   Task04_2 - \Properties\AssemblyInfo.cs
Pass 1:   Task04_3 - \Program.cs
Pass 1:   Task04_3 - \Properties\AssemblyInfo.cs

------ StyleCop completed ------

========== Violation Count: 32 ==========
	 */