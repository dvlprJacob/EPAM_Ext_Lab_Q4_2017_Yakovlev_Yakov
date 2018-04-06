using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NorthwindDAL.Helpers.MVC_Models
{
    public class OrderGeneralDataHelper
    {
        public int OrderID;

        [Display(Name = "Заказчик")]
        [DisplayFormat(NullDisplayText = "No entry")]
        public string Customer;

        [Display(Name = "Статус заказа")]
        [DisplayFormat(NullDisplayText = "No entry")]
        public DeliveryStatus ShippedStatus;

        [Display(Name = "Дата")]
        [DisplayFormat(DataFormatString = "{0:d}", NullDisplayText = "No entry")]
        public DateTime? OrderDate;

        [Display(Name = "Сумма")]
        [DisplayFormat(NullDisplayText = "No entry")]
        public decimal? Cost;
    }
}
