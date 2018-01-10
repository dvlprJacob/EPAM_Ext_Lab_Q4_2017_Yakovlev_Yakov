namespace Task09
{
    using System.Collections.Generic;

    /// <summary>
    /// Реализует метод расширения для IEnumerable [ int, double, string ], вычисляющий сумму элементов перечисления,
    /// для IEnumerable [ string ] возвращает конкатинацию элементов
    /// </summary>
    public static class ExtensionClass
    {
        /// <summary>
        /// Вычисляет сумму элементов перечисления
        /// </summary>
        /// <typeparam name="TType"> int, double, string</typeparam>
        /// <param name="list"> Перечисление</param>
        /// <param name="result"> Возвращаемый результат сложения, default(TType), если сложение невозможно </param>
        /// <returns> Логический исход попытки сложения</returns>
        public static bool TryCalcSumOfElements<TType>(this IEnumerable<TType> list, out TType result)
        {
            //// without it don't work becouse result is null in first iteration when TType is string type
            //// [ result = default(TType), that is result = default(string), default(string) is null ]
            if (list is IEnumerable<string>)
            {
                result = (TType)(string.Empty as object);
            }
            ////
            else
            {
                result = default(TType);
            }

            if (!(list is IEnumerable<int> || list is IEnumerable<double> || list is IEnumerable<string>) || list == null)
            {
                return false;
            }

            foreach (var elem in list)
            {
                if (!TryAdd<TType>(result, elem, out result))
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Складывает два элемента
        /// </summary>
        /// <typeparam name="TType">Тип перечисления, ограничен множеством из int, double, string</typeparam>
        /// <param name="firstOperand"> Первый операнд</param>
        /// <param name="secondOperand"> Второй операнд</param>
        /// <param name="result"> Возвращаемый результат сложения</param>
        /// <returns> Логический исход попытки сложения</returns>
        private static bool TryAdd<TType>(TType firstOperand, TType secondOperand, out TType result)
        {
            result = default(TType);
            if (firstOperand is int)
            {
                int? temp1 = firstOperand as int?;
                int? temp2 = secondOperand as int?;
                var sum = (temp1 + temp2) as object;
                result = (TType)sum;

                return true;
            }

            if (firstOperand is double)
            {
                double? temp1 = firstOperand as double?;
                double? temp2 = secondOperand as double?;
                var sum = (temp1 + temp2) as object;
                result = (TType)sum;

                return true;
            }

            if (firstOperand is string)
            {
                string temp1 = firstOperand as string;
                string temp2 = secondOperand as string;
                var sum = string.Concat(temp1, temp2) as object;
                result = (TType)sum;

                return true;
            }

            return false;
        }
    }
}