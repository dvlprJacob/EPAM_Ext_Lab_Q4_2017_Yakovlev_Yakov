using System;

namespace Task03
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            string command = string.Empty;
            try
            {
                var arrayF = MyArray<int>.MyArray_with_random_int_values(29);//todo pn хардкод
                Console.WriteLine("Был сгенерирован целочисленный массив из 29 элементов :\n{0}", arrayF);//todo pn хардкод
				Console.WriteLine("Максимальный элемент массива : {0}\nМинимальный элемент массива : {1}", arrayF.Find_max_value(), arrayF.Find_min_value());
                arrayF.Sort();
                Console.WriteLine("Отсортированный по возрастанию :\n{0}\n\n", arrayF);

                var arrayS = MyArray<string>.MyArray_with_random_string_values(15);//todo pn хардкод
				Console.WriteLine("Был сгенерирован строковый массив из 15 элементов:\n{0}", arrayS);//todo pn хардкод
				Console.WriteLine("Максимальный элемент массива : {0}\nМинимальный элемент массива : {1}", arrayS.Find_max_value(), arrayS.Find_min_value());
                arrayS.Sort();
                Console.WriteLine("Отсортированный по возрастанию :\n{0}\n\n", arrayS);

                var arrayT = MyArray<char>.MyArray_with_random_char_values(26);//todo pn хардкод
				Console.WriteLine("Был сгенерирован символьный массив из 26 элементов :\n{0}", arrayT);//todo pn хардкод
				Console.WriteLine("Максимальный элемент массива : {0}\nМинимальный элемент массива : {1}", arrayT.Find_max_value(), arrayT.Find_min_value());
                arrayT.Sort();
                Console.WriteLine("Отсортированный по возрастанию :\n{0}", arrayT);

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