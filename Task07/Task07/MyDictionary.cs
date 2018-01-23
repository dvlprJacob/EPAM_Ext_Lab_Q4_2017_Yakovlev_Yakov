namespace Task07_1
{
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// Представляет пары элементов с порядковым номером 1 или 2
    /// </summary>
    public class MyDictionary
    {
        private List<int> serialNumbers;
        private List<int> values;

        public MyDictionary(int n)
        {
            this.serialNumbers = new List<int>(n);
            this.values = new List<int>(n);

            for (int i = 0; i < n; i++)
            {
                this.values.Add(i + 1);
                switch (i % 2 == 0)
                {
                    case true:
                        this.serialNumbers.Add(1);//todo pn немного не понял, а для чего тебе хранить порядковые номера, если ты в цикле вполне можешь оперировать индексами элементов в массиве
                        break;

                    case false:
                        this.serialNumbers.Add(2);
                        break;
                }
            }
        }

        public int Count
        {
            get
            {
                return this.values.Count;
            }
        }

        public int ElementAt(int index)
        {
            if (index < this.Count && index >= 0)
            {
                return this.values[index];
            }

            return 0;
        }

        public int SerialNumberAt(int index)
        {
            if (index < this.Count && index >= 0)
            {
                return this.serialNumbers[index];
            }

            return 0;
        }

        /// <summary>
        /// Удаляет пары с указанным порядковым номером ( 1 или 2 )
        /// </summary>
        /// <param name="serialNumber"></param>
        public void RemoveAny(int serialNumber)
        {
            if (!(serialNumber != 1 || serialNumber != 2))
            {
                return;
            }

            this.serialNumbers.RemoveAll(sn => sn == serialNumber);
            for (int i = 0; i < this.Count; i++)
            {
                if (i % serialNumber != 0)
                {
                    this.values[i] = default(int);
                }
            }

            this.values.RemoveAll(v => v == default(int));
            this.UpdateSerialNumbers();
        }

        public override string ToString()
        {
            StringBuilder temp = new StringBuilder();
            for (int i = 0; i < this.Count; i++)
            {
                switch (this.serialNumbers[i])
                {
                    case 1:
                        temp.Append(string.Format("{0} - first", this.values[i]));
                        break;

                    case 2:
                        temp.Append(string.Format("{0} - second", this.values[i]));
                        break;
                }

                temp.AppendLine();
            }

            return temp.ToString();
        }

        /// <summary>
        /// Пересчитывает пары на первый-второй
        /// </summary>
        private void UpdateSerialNumbers()
        {
            for (int i = 0; i < this.serialNumbers.Count; i++)
            {
                this.serialNumbers[i] = (i % 2 == 0) ? 1 : 2;
            }
        }
    }
}