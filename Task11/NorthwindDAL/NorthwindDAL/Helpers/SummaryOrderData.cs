namespace NorthwindDAL.Models
{
    public class SummaryOrderData
    {
        public Orders Order
        {
            get;
            set;
        }

        public OrderDetails Deatails
        {
            get;
            set;
        }

        public Products Product
        {
            get;
            set;
        }
    }
}