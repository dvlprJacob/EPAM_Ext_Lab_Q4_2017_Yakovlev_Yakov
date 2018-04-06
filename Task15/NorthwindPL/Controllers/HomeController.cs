using NorthwindDAL;
using NorthwindDAL.Helpers.MVC_Models;
using NorthwindPL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NorthwindPL.Controllers
{
    public class HomeController : Controller
    {
        //private OrdersGeneralDataViewModel ordersData { get; set; }
        private OrderRepository repos { get; set; }
        private OrdersGeneralDataViewModel orders { get; set; }

        public HomeController()
        {
            this.repos = new OrderRepository();
        }

        /// <summary>
        /// Return orders general data listener
        /// </summary>
        /// <param name="offset"></param>
        /// <param name="recCount">Records count on page</param>
        /// <returns></returns>
        public ActionResult Index(int offset = 0, int recCount=12)
        {
            if(recCount<=0)
            {
                recCount = 12;
            }

            if(offset<=0)
            {
                offset = 0;
            }

            ViewBag.offset = offset + 12;
            ViewBag.currRecords = recCount;
            this.orders = new OrdersGeneralDataViewModel();
            this.orders.Orders = this.repos.GetOrdersGeneralData(offset,recCount);

            if(this.orders.Orders==null)
            {
                HttpStatusCodeResult resp = new HttpStatusCodeResult(System.Net.HttpStatusCode.NotFound, this.repos.UnresolvedExceptions.Last().Message);
                return resp;
            }

            return View(this.orders);
        }
    }
}