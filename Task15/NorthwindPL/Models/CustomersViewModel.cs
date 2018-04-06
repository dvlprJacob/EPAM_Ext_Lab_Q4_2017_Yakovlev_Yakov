using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NorthwindPL.Models
{
    /// <summary>
    /// Help make customers - DropDownList
    /// </summary>
    public class CustomersViewModel
    {
        private Dictionary<string, string> customers;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="queryResult">Key = CustomerID, Value = Customer</param>
        public CustomersViewModel(Dictionary<string,string> queryResult)
        {
            this.customers = queryResult;
        }

        /// <summary>
        /// Key = CustomerID, Value = Customer
        /// </summary>
        public IEnumerable<SelectListItem> Customers
        {
            get
            {
                return customers.Select(c => new SelectListItem { Value=c.Key,Text=c.Value });
            }
        }
    }
}