using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NorthwindPL.Models
{
    /// <summary>
    /// Help to add new order : for make products - DropDownList
    /// </summary>
    public class ProductModel
    {
        public ProductModel(string prodName, int prodId, int? supplId)
        {
            this.ProductName = prodName;
            this.ProductID = prodId;
            this.SupplierID = supplId;
        }
        public string ProductName
        {
            get;
            set;
        }

        public int ProductID
        {
            get;
            set;
        }

        public int? SupplierID
        {
            get;
            set;
        }

        // rest fields are not needed
    }
}