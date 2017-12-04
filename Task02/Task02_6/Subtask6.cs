namespace Task02_6
{
    using System.Collections.Generic;

    /// <summary>
    /// Для выделения текстовой надписи можно использовать выделение жирным, курсивом и подчёркиванием.
    /// Предложите способ хранения информации о выделении надписи и напишите программу,
    /// которая позволяет назначать и удалять текстовой надписи выделение
    /// </summary>
    internal class Subtask6
    {
        // Массив доступных выделений
        private List<string> Accentuation;

        public Subtask6()
        {
            this.Accentuation = new List<string>();
        }

        public bool Exist(string acc)
        {
            if (this.Accentuation.Exists(a => a == acc))
            {
                return true;
            }

            return false;
        }

        public void AddAccent(string accent)
        {
            if (!this.Accentuation.Exists(a => a == accent))
            {
                this.Accentuation.Add(accent);
            }
        }

        public void PopAccent(string accent)
        {
            this.Accentuation.Remove(accent);
        }

        public override string ToString()
        {
            string tmp = "Параметры надписи : ";
            if (this.Accentuation.Count == 0)
            {
                return string.Format("{0}None", tmp);
            }

            for (int i = 0; i < this.Accentuation.Count; i++)
            {
                tmp += this.Accentuation[i] + " ";
            }

            return tmp;
        }
    }
}