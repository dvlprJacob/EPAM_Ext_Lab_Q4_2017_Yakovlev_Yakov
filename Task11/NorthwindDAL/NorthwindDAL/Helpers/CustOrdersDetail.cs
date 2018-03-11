namespace NorthwindDAL.Helpers
{
    using System.Collections.Generic;

    public class CustOrdersDetail
    {
        public int OrderId
        {
            get;
            set;
        }

        public List<string> ProductName
        {
            get;
            set;
        }

        /// <summary>
        /// ROUND(Od.UnitPrice, 2)
        /// </summary>
        public List<decimal> UnitPrice
        {
            get;
            set;
        }

        public List<short> Quantity
        {
            get;
            set;
        }

        /// <summary>
        /// CONVERT(int, Discount * 100), in percentages
        /// </summary>
        public List<int> Discount
        {
            get;
            set;
        } // int, т.к. в бд уже есть CK_Discount

        /// <summary>
        /// ROUND(CONVERT(money, Quantity * (1 - Discount) * Od.UnitPrice), 2)
        /// </summary>
        public List<decimal> ExtendedPrice
        {
            get;
            set;
        }
    }
}