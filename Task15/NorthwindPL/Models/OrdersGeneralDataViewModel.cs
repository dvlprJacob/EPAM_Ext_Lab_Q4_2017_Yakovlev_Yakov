using NorthwindDAL.Helpers;
using NorthwindDAL.Helpers.MVC_Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NorthwindPL.Models
{
    public class OrdersGeneralDataViewModel
    {
        public IEnumerable<OrderGeneralDataHelper> Orders { get; set; }
    }
}