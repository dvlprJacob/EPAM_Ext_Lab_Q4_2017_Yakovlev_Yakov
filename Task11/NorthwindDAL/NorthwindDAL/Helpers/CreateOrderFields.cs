namespace NorthwindDAL.Models
{
    public class CreateOrderFields
    {
        public string CustomerId
        {
            get;
            set;
        }

        public int ProductID
        {
            get;
            set;
        }

        public int Quantity
        {
            get;
            set;
        }

        /// <summary>
        /// must be less than 1 and more than 0
        /// </summary>
        public double Discount
        {
            get;
            set;
        }
    }
}