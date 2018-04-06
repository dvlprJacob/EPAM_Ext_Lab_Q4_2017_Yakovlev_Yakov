using System.ComponentModel.DataAnnotations;

namespace NorthwindDAL.Models
{
    public class Products
    {
        public int ProductID
        {
            get;
            set;
        }

        [DisplayFormat(NullDisplayText = "No entry")]
        [Display(Name = "Product")]
        public string ProductName
        {
            get;
            set;
        }

        [DisplayFormat(NullDisplayText = "No entry")]
        public int? SupplierID
        {
            get;
            set;
        }

        [DisplayFormat(NullDisplayText = "No entry")]
        public int? CategoryID
        {
            get;
            set;
        }

        [DisplayFormat(NullDisplayText = "No entry")]
        public string QuantityPerUnit
        {
            get;
            set;
        }

        [Display(Name = "Price")]
        [DisplayFormat(NullDisplayText = "No entry")]
        public decimal? UnitPrice
        {
            get;
            set;
        }

        [DisplayFormat(NullDisplayText = "No entry")]
        public int? UnitsInStock
        {
            get;
            set;
        }

        [DisplayFormat(NullDisplayText = "No entry")]
        public int? UnitsOnOrder
        {
            get;
            set;
        }

        [DisplayFormat(NullDisplayText = "No entry")]
        public short? ReorderLevel
        {
            get;
            set;
        }

        [DisplayFormat(NullDisplayText = "No entry")]
        public bool Discontinued
        {
            get;
            set;
        }
    }
}