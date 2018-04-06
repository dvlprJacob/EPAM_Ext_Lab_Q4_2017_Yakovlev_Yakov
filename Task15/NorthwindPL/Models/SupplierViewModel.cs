using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NorthwindDAL.Models;

namespace NorthwindPL.Models
{
    /// <summary>
    /// Help to display order detail data
    /// </summary>
    public class SupplierViewModel
    {
        public SupplierViewModel(int id, string supplier, string phone)
        {
            this.EmployeeID = id;
            this.Supplier = supplier;
            this.HomePhone = phone;
        }

        public int EmployeeID { get; set; }
        public string Supplier { get; set; }
        public string HomePhone { get; set; }

        // rest fields are not needed
    }
}