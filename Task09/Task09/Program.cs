namespace Task09
{
    using System;

    /// <summary>
    /// Проверяет результат сложения элементов массива
    /// </summary>
    internal class Program
    {
        private static void Main(string[] args)
        {
            int[] intArray = new int[] { 2, 1, 3 };
            int sum = default(int);
            var resStatus = intArray.TryCalcSumOfElements(out sum);
            Console.WriteLine("{0}   {1}", resStatus, sum);

            double[] doubleArray = new double[] { 2.3, 1, 3.65 };
            double sum2 = default(double);
            resStatus = doubleArray.TryCalcSumOfElements(out sum2);
            Console.WriteLine("{0}   {1}", resStatus, sum2);

            string[] stringArray = new string[] { "a", "b", "cd" };
            string sum3 = string.Empty;
            resStatus = stringArray.TryCalcSumOfElements(out sum3);
            Console.WriteLine("{0}   {1}", resStatus, sum3);
            Console.ReadKey();
        }
    }
}