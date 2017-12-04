using System;

namespace Task03
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            int n = 0;
            string command = string.Empty;
            try
            {
                while (command != "0")
                {
                    Console.WriteLine("Введите номер типа и размер массива, 0 чтобы выйти :\n1) Integer\n2) String\n3) Char");
                    command = Console.ReadLine();
                    var parsed = command.Split(' ');
                    if (parsed.Length == 2)
                    {
                        switch (parsed[0])
                        {
                            case "1":
                                switch (int.TryParse(parsed[1], out n))
                                {
                                    case true:
                                        if (n < 0)
                                        {
                                            Console.WriteLine("Длина массива должна быть больше нуля");
                                            break;
                                        }

                                        var array = MyArray<char>.Integer_array(n);
                                        Console.WriteLine("Был сгенерирован целочисленный массив из {0} элементов :\n{1}", array.Length, array);
                                        Console.WriteLine("Максимальный элемент массива : {0}\nМинимальный элемент массива : {1}", array.Get_max_element(), array.Get_min_element());
                                        array.Sort();
                                        Console.WriteLine("Отсортированный по возрастанию :\n{0}", array);
                                        break;

                                    case false:
                                        Console.WriteLine("Введен некорректный размер массива");
                                        break;
                                }
                                break;

                            case "2":
                                switch (int.TryParse(parsed[1], out n))
                                {
                                    case true:
                                        if (n < 0)
                                        {
                                            Console.WriteLine("Длина массива должна быть больше нуля");
                                            break;
                                        }

                                        var array = MyArray<char>.String_array(n);
                                        Console.WriteLine("Был сгенерирован строковый массив из {0} элементов :\n{1}", array.Length, array);
                                        Console.WriteLine("Максимальный элемент массива : {0}\nМинимальный элемент массива : {1}", array.Get_max_element(), array.Get_min_element());
                                        array.Sort();
                                        Console.WriteLine("Отсортированный по возрастанию :\n{0}", array);
                                        break;
                                }

                                break;

                            case "3":
                                switch (int.TryParse(parsed[1], out n))
                                {
                                    case true:
                                        if (n < 0)
                                        {
                                            Console.WriteLine("Длина массива должна быть больше нуля");
                                            break;
                                        }

                                        var array = MyArray<char>.Character_array(n);
                                        Console.WriteLine("Был сгенерирован символьный массив из {0} элементов :\n{1}", array.Length, array);
                                        Console.WriteLine("Максимальный элемент массива : {0}\nМинимальный элемент массива : {1}", array.Get_max_element(), array.Get_min_element());
                                        array.Sort();
                                        Console.WriteLine("Отсортированный по возрастанию :\n{0}", array);

                                        break;
                                }

                                break;

                            default:
                                Console.WriteLine("Неверно введен тип");
                                break;
                        }
                    }
                    else if (command != "0")
                    {
                        Console.WriteLine("Некорректный ввод");
                    }
                }
                Console.WriteLine("Нажмите на любую кнопку, чтобы выйти");
                Console.ReadKey();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message + "\nPress any key for exit");
                Console.ReadKey();
                Main(args);
            }
        }
    }
}