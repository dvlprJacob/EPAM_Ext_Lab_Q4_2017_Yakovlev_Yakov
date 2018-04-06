namespace NorthwindDAL.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Orders
    {
        public int OrderID
        {
            get;
            set;
        }

        public string CustomerID
        {
            get;
            set;
        }

        [DisplayFormat(NullDisplayText = "No entry")]
        public int? EmployeeID
        {
            get;
            set;
        }

        [DisplayFormat(DataFormatString ="{0:d}", NullDisplayText = "No entry")]
        public DateTime? OrderDate
        {
            get;
            set;
        }

        [DisplayFormat(DataFormatString = "{0:d}", NullDisplayText = "No entry")]
        public DateTime? RequiredDate
        {
            get;
            set;
        }

        [DisplayFormat(DataFormatString = "{0:d}", NullDisplayText = "No entry")]
        public DateTime? ShippedDate
        {
            get;
            set;
        }

        [DisplayFormat(NullDisplayText = "No entry")]
        public int? ShipVia
        {
            get;
            set;
        }

        [DisplayFormat(NullDisplayText = "No entry")]
        public decimal? Freight
        {
            get;
            set;
        }

        [DisplayFormat(NullDisplayText = "No entry")]
        public string ShipName
        {
            get;
            set;
        }

        [DisplayFormat(NullDisplayText = "No entry")]
        public string ShipAddress
        {
            get;
            set;
        }

        [DisplayFormat(NullDisplayText = "No entry")]
        public string ShipCity
        {
            get;
            set;
        }

        [DisplayFormat(NullDisplayText = "No entry")]
        public string ShipRegion
        {
            get;
            set;
        }

        [DisplayFormat(NullDisplayText = "No entry")]
        public string ShipPostalCode
        {
            get;
            set;
        }

        [DisplayFormat(NullDisplayText = "No entry")]
        public string ShipCountry
        {
            get;
            set;
        }
    }
}