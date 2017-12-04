namespace Task02_6
{
    using System;

    internal class Program
    {
        public static void Main(string[] args)//todo pn реализовать таким образом, чтобы нужно было вводить только 1-3. Ну, и ключ для выхода.
        {
            try
            {
<<<<<<< HEAD
                Subtask6 tmp = new Subtask6();
                string command = string.Empty;
                while (command != "0")
=======
                var parsed = commmand.Split(' ');

                switch (parsed.Last())//todo pn не работает логика, даже если вводишь "on"
>>>>>>> deba0da69d17d4647861bb597c490fe854665633
                {
                    string[] accents = new string[] { "Bold", "Italic", "Underline" };
                    Console.WriteLine(tmp);
                    Console.WriteLine("Введите :\n1: {0}\n2: {1}\n3: {2}\n0 для выхода", accents[0], accents[1], accents[2]);
                    command = Console.ReadLine();
                    switch (command)
                    {
                        case "1":
                            if (tmp.Exist(accents[0]))
                            {
                                tmp.PopAccent(accents[0]);
                            }
                            else
                            {
                                tmp.AddAccent(accents[0]);
                            }

                            break;

                        case "2":
                            if (tmp.Exist(accents[1]))
                            {
                                tmp.PopAccent(accents[1]);
                            }
                            else
                            {
                                tmp.AddAccent(accents[1]);
                            }

                            break;

                        case "3":
                            if (tmp.Exist(accents[2]))
                            {
                                tmp.PopAccent(accents[2]);
                            }
                            else
                            {
                                tmp.AddAccent(accents[2]);
                            }

                            break;

                        case "0":
                            Console.WriteLine("Press any key for exit");
                            Console.ReadKey();
                            return;

                        default:
                            Console.WriteLine("Incorrect command, try again");
                            break;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message + "\nPress any key for exit");
                Console.ReadKey();
                return;
            }
        }
    }
}