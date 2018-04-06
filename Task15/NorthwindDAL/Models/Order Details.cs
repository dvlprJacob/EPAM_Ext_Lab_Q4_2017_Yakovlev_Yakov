using System.ComponentModel.DataAnnotations;

namespace NorthwindDAL.Models
{
    public class OrderDetails
    {
        public int OrderID
        {
            get;
            set;
        }

        public int ProductID
        {
            get;
            set;
        }

        [DisplayFormat(NullDisplayText = "No entry")]
        public decimal UnitPrice
        {
            get;
            set;
        }

        [DisplayFormat(NullDisplayText = "No entry")]
        public int Quantity
        {
            get;
            set;
        }

        [DisplayFormat(DataFormatString ="{0:f2}", NullDisplayText = "No entry")]
        public double Discount
        {
            get;
            set;
        }
    }
}