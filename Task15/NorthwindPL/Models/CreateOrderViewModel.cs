using NorthwindDAL.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NorthwindPL.Models
{
    public class CreateOrderViewModel
    {
        public CreateOrderViewModel() { }

        [Display(Name ="For customer")]
        public CustomersViewModel Customers
        {
            get;
            set;
        }

        [Display(Name = "Product")]
        public ProductsViewModel Products
        {
            get;
            set;
        }

        [Display(Name = "Quantity")]
        public int? Quantity
        {
            get;
            set;
        }

        [Display(Name = "Discount")]
        public double? Discount
        {
            get;
            set;
        }
    }
}