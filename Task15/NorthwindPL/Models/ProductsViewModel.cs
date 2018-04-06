using NorthwindDAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NorthwindDAL.Helpers;

namespace NorthwindPL.Models
{
    public class ProductsViewModel
    {
        public ProductsViewModel(IEnumerable<ProductModelHelper> queryResult)
        {
            if(queryResult==null)
            {
                return;
            }

            this.products = new List<ProductModelHelper>();
            foreach(var record in queryResult)
            {
                this.products.Add(new ProductModelHelper(record.ProductName,record.ProductID,record.SupplierID));
            }
        }

        private List<ProductModelHelper> products
        {
            get;
            set;
        }

        /// <summary>
        /// Key = CustomerID, Value = Customer
        /// </summary>
        public IEnumerable<SelectListItem> Products
        {
            get
            {
                return this.products.Select(c => new SelectListItem { Value = Convert.ToString(c.ProductID), Text = c.ProductName });
            }
        }
    }
}