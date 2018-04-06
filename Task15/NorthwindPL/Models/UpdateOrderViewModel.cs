using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NorthwindPL.Models
{
    public class UpdateOrderViewModel
    {
        /// <summary>
        /// Value = table field name, text = table name
        /// </summary>
        private Dictionary<string, string> tableFields = new Dictionary<string, string>()
                {
                    {"CustomerID", "Orders" },
                    {"SupplierID", "Orders" },
                    {"OrderDate", "Orders" },
                    {"RequiredDate", "Orders" },
                    {"ShippedDate", "Orders" },
                    {"ShipVia", "Orders" },
                    {"Freight", "Orders" },
                    {"ShipName", "Orders" },
                    {"ShipAddress", "Orders" },
                    {"ShipCity", "Orders" },
                    {"ShipRegion", "Orders" },
                    {"ShipPostalCode", "Orders" },
                    {"ShipCountry", "Orders" },
                    {"UnitPrice", "[Order Details]" },
                    {"Quantity", "[Order Details]" },
                    {"Discount", "[Order Details]" }};

        private Dictionary<string, string> values;

        public UpdateOrderViewModel()
        { }

        [AllowHtml]
        public int OrderID
        {
            get;
            set;
        }

        [AllowHtml]
        public int ProductID
        {
            get;
            set;
        }

        [Display(Name = "Field")]
        /// <summary>
        /// Value = table, text = field
        /// </summary>
        public IEnumerable<SelectListItem> Field
        {
            /// Tables and their fields that you can update
            get
            {
                var res = this.tableFields.Select(f => new SelectListItem { Value = f.Value, Text = f.Key, Selected = true }).Take(1);
                return res.Concat(this.tableFields.Select(f => new SelectListItem { Value = f.Value, Text = f.Key, Disabled = false }).Skip(1));
            }
        }

        /// <summary>
        /// Value = table, text = field, didn't work : not get on view edit form
        /// </summary>
        [AllowHtml]
        [Display(Name = "New Value")]
        public string NewValue
        {
            /// Get selected field current value
            get
            {
                var selected = this.Field.Where(f => f.Selected == true).Count();
                if (this.values == null || selected == 0)
                {
                    return string.Empty;
                }
                var res = string.Empty;
                foreach (var item in this.values)
                {
                    var field = this.Field.Where(f => f.Selected == true).First().Text;
                    if (field == item.Key)
                    {
                        res = item.Value;
                        break;
                    }
                }
                return res;
            }
        }

        [AllowHtml]
        public string OldValue
        {
            get;
            set;
        }

        public static explicit operator UpdateOrderViewModel(OrdersDomainModel model)
        {
            UpdateOrderViewModel res = new UpdateOrderViewModel();
            res.OrderID = model.Order.Order.OrderID;
            res.ProductID = model.Order.Deatails.ProductID;
            res.values = new Dictionary<string, string>()
                {
                    {"CustomerID", model.Order.Order.CustomerID },
                    {"SupplierID", Convert.ToString(model.Order.Order.EmployeeID)},
                    {"OrderDate", Convert.ToString(model.Order.Order.OrderDate)},
                    {"RequiredDate", Convert.ToString(model.Order.Order.RequiredDate)},
                    {"ShippedDate", Convert.ToString(model.Order.Order.ShippedDate)},
                    {"ShipVia", Convert.ToString(model.Order.Order.ShipVia)},
                    {"Freight", Convert.ToString(model.Order.Order.Freight)},
                    {"ShipName", model.Order.Order.ShipName},
                    {"ShipAddress", model.Order.Order.ShipAddress},
                    {"ShipCity", model.Order.Order.ShipCity},
                    {"ShipRegion", model.Order.Order.ShipRegion},
                    {"ShipPostalCode", model.Order.Order.ShipPostalCode},
                    {"ShipCountry", model.Order.Order.ShipCountry },
                    {"UnitPrice", Convert.ToString(model.Order.Deatails.UnitPrice)},
                    {"Quantity", Convert.ToString(model.Order.Deatails.Quantity)},
                    {"Discount", Convert.ToString(model.Order.Deatails.Discount)}};

            return res;
        }
    }
}