using NorthwindDAL;
using NorthwindPL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NorthwindPL.Controllers
{
    [ValidateInput(false)]
    public class OrdersController : Controller
    {
        private OrderRepository repos { get; set; }
        private OrdersDomainModel order { get; set; }

        public OrdersController()
        {
            this.repos = new OrderRepository();
        }
        // GET: Orders
        public ActionResult Index(int orderID)
        {
            this.order = new OrdersDomainModel();
            this.order.Order = this.repos.GetOrderInfo(orderID);

            if (this.order.Order == null)
            {
                HttpStatusCodeResult resp = new HttpStatusCodeResult(System.Net.HttpStatusCode.NotFound,this.repos.UnresolvedExceptions.Last().Message);
                return resp;
            }

            return View("Index",this.order);
        }

        [HttpGet]
        public PartialViewResult CreateOrder(CreateOrderViewModel model)
        {
            model.Customers = new CustomersViewModel(this.repos.GetCustomers());
            model.Products = new ProductsViewModel(this.repos.GetProducts());
            return PartialView("CreateOrder", model);
        }

        [HttpPost]
        public void CreateOrder(string custId, string prodId, string quant, string disc)
        {
            if (custId == "" || prodId == "" || quant == "" || disc == "")
            {
                RedirectToAction("Invalid", "Orders", "Argument exc");
            }

            var res = this.repos.CrateOrder(new NorthwindDAL.Models.CreateOrderFields(custId, Convert.ToInt32(prodId), Convert.ToInt32(quant), Convert.ToDouble(disc)));

            if(res==false)
            {
                RedirectToAction("Invalid", "Orders", "Create exc");
            }

            RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public PartialViewResult UpdateOrder(UpdateOrderViewModel model)
        {
            return PartialView("UpdateOrder", model);
        }

        /// <summary>
        /// Didn't work : view form not send field param
        /// </summary>
        /// <param name="recordId"></param>
        /// <param name="table"></param>
        /// <param name="field"></param>
        /// <param name="newValue"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult UpdateOrder(string recordId, string table, string field, string newValue)
        {
            if(recordId=="" || table=="" || field =="" || newValue == "")
            {
                RedirectToAction("Invalid", "Orders");
            }

            var res = this.repos.UpdateRecord(recordId, table, field, newValue);

            if (recordId == "" || table == "" || field == "" || newValue == "")
            {
                RedirectToAction("Invalid", "Orders", "Update exc");
            }

            return RedirectToAction("Index", "Home");
        }

        [ChildActionOnly]
        public PartialViewResult GetCustomers()
        {
            return PartialView("Customers", new CustomersViewModel(this.repos.GetCustomers()));
        }

        [ChildActionOnly]
        public PartialViewResult GetProducts()
        {
            return PartialView("Products", new ProductsViewModel(this.repos.GetProducts()));
        }

        [ChildActionOnly]
        public PartialViewResult GetSupplier(int id)
        {
            var result = this.repos.GetEmployee(id);

            if (result == null)
            {
                HttpStatusCodeResult resp = new HttpStatusCodeResult(System.Net.HttpStatusCode.NotFound, this.repos.UnresolvedExceptions.Last().Message);
                return null;
            }

            return PartialView("Supplier",new SupplierViewModel(result.EmployeeID, string.Concat(result.TitleOfCourtesy, ' ', result.FirstName, ' ', result.LastName),result.HomePhone));
        }
    }
}