namespace NorthwindDAL.Helpers
{
    using System.Collections.Generic;

    public class CustOrderHist
    {
        public string CustomerID
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
        /// SUM(Quantity)
        /// </summary>
        public List<int> Total
        {
            get;
            set;
        }
    }
}