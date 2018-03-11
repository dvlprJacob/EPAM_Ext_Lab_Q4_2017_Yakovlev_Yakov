namespace NorthwindDAL.Helpers
{
    using NorthwindDAL.Models;

    public class OrdersList
    {
        public Orders AllOrders
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