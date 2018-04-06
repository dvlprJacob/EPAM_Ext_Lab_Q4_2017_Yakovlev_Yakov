using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NorthwindDAL.Helpers.TableFieldNames
{
    /// <summary>
    /// [Orders] table field names
    /// </summary>
    internal static class OrdersFields
    {
        public const string OrderID = "OrderID";
        public const string CustomerID = "CustomerID";
        public const string EmployeeID = "EmployeeID";
        public const string OrderDate = "OrderDate";
        public const string RequiredDate = "RequiredDate";
        public const string ShippedDate = "ShippedDate";
        public const string ShipVia = "ShipVia";
        public const string Freight = "Freight";
        public const string ShipName = "ShipName";
        public const string ShipAddress = "ShipAddress";
        public const string ShipCity = "ShipCity";
        public const string ShipRegion = "ShipRegion";
        public const string ShipPostalCode = "ShipPostalCode";
        public const string ShipCountry = "ShipCountry";

        public static bool Contains(string field)
        {
            return (field == CustomerID || field == EmployeeID || field == OrderDate || field == RequiredDate
                || field == ShippedDate || field == ShipVia || field == Freight || field == ShipName
                || field == ShipAddress || field == ShipCity || field == ShipRegion || field == ShipPostalCode
                || field == ShipCountry);
        }
    }
}