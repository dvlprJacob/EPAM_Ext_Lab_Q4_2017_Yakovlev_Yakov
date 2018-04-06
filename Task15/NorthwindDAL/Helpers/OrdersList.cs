namespace NorthwindDAL.Helpers
{
    using NorthwindDAL.Models;

    public class OrdersList
    {
        public Orders Order
        {
            get;
            set;
        }

        public DeliveryStatus OrderStatus
        {
            get;
            set;
        }
    }
}