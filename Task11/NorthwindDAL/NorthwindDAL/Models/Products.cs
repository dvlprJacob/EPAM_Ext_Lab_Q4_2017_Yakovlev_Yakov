﻿namespace NorthwindDAL.Models
{
    public class Products
    {
        public int ProductID
        {
            get;
            set;
        }

        public string ProductName
        {
            get;
            set;
        }

        public int? SupplierID
        {
            get;
            set;
        }

        public int? CategoryID
        {
            get;
            set;
        }

        public string QuantityPerUnit
        {
            get;
            set;
        }

        public decimal? UnitPrice
        {
            get;
            set;
        }

        public int? UnitsInStock
        {
            get;
            set;
        }

        public int? UnitsOnOrder
        {
            get;
            set;
        }

        public short? ReorderLevel
        {
            get;
            set;
        }

        public bool Discontinued
        {
            get;
            set;
        }
    }
}