using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Task2
{
    /// <summary>
    /// Для выделения текстовой надписи можно использовать выделение жирным, курсивом и подчёркиванием.
    /// Предложите способ хранения информации о выделении надписи и напишите программу,
    /// которая позволяет назначать и удалять текстовой надписи выделение
    /// </summary>
    internal class Subtask6
    {
        // Массив доступных выделений
        private readonly string[] Accentuation = { "Bold", "Italic", "Underline" };

        // Поле для хранения статуса [on/off]
        private readonly bool[] AccentuationStatus = { false, false, false };

        public Subtask6()
        {
        }

        /// <summary>
        /// Конструктор с перечнем включенных выделений
        /// </summary>
        /// <param name="activeAccents"> Список выделений</param>
        public Subtask6(string[] activeAccents)
        {
            if (activeAccents.Length == 3)
            {
                for (int i = 0; i < 3; i++)
                    if (Accentuation.Contains(activeAccents[i]))
                        AccentuationStatus[i] = true;
            }
        }

        /// <summary>
        /// Проверка на наличие параметра в списке выделений
        /// </summary>
        /// <param name="accent"> Выделение</param>
        /// <returns></returns>
        public bool IsExist(string accent)
        {
            if (Accentuation.Where(acc => acc == accent).Count() > 0)
                return true;
            return false;
        }

        /// <summary>
        /// Активация выделения
        /// </summary>
        /// <param name="accent"> Выделение</param>
        public void ActivateAccent(string accent)
        {
            for (int i = 0; i < 3; i++)
            {
                if (Accentuation[i] == accent)
                    AccentuationStatus[i] = true;
            }
        }

        /// <summary>
        /// Деактивация выделения
        /// </summary>
        /// <param name="accent"> Выделение</param>
        public void DeactivateAccent(string accent)
        {
            for (int i = 0; i < 3; i++)
            {
                if (Accentuation[i] == accent)
                    AccentuationStatus[i] = false;
            }
        }

        public override string ToString()
        {
            string tmp = "Accentuations : ";
            for (int i = 0; i < 3; i++)
            {
                if (AccentuationStatus[i] == true)
                    tmp += Accentuation[i] + " [ active ]; ";
                else
                    tmp += Accentuation[i] + " [ inactive ]; ";
            }
            return tmp;
        }
    }
}