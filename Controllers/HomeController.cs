using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RestaurantApp.Models;
using RestaurantApp.ViewModel;

namespace RestaurantApp.Controllers
{
    public class HomeController : Controller
    {

        Model1 db = new Model1();

        // GET: Home
        public ActionResult Index()
        {



            IEnumerable<Item> items = db.Items;
            IEnumerable<Customer> customers = db.Customers;
            IEnumerable<PaymentType> paymentTypes = db.PaymentTypes;

            var model = new MultipleDataViewModel { items = items.ToArray(), customers = customers.ToArray(), paymentTypes = paymentTypes.ToArray() };


            return View(model);
        }

        [HttpPost]
        public JsonResult getItemUnitPrice(int itemIdPss)
        {
            Decimal itemPrice = db.Items.Find(itemIdPss).ItemPrice;

                return  Json(itemPrice,JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult PlaceOrder(PlaceOrderViewModel objpPlaceOrderViewModel)
        {

            var OrderNo = Decimal.Parse(String.Format("{0:yyyyMMddHHmmss}", DateTime.Now));

            Order objOrder = new Order();


            objOrder.OrderNo = OrderNo;

            objOrder.CustomerId = objpPlaceOrderViewModel.CustomerId;

            objOrder.PaymentTypeId = objpPlaceOrderViewModel.PaymentTypeId;

            objOrder.Total = objpPlaceOrderViewModel.Total;

            objOrder.Discount = objpPlaceOrderViewModel.Discount;
            db.Orders.Add(objOrder);
            db.SaveChanges();

            foreach (OrderDetail objOrderDetails in objpPlaceOrderViewModel.OrderDetail)
            {

                OrderDetail objOrderDetail = new OrderDetail();
                objOrderDetail.OrderNo = OrderNo;
                objOrderDetail.ItemId = objOrderDetails.ItemId;
                objOrderDetail.Quantity = objOrderDetails.Quantity;
                objOrderDetail.Total = objOrderDetails.Total;
                db.OrderDetails.Add(objOrderDetail);
                db.SaveChanges();

                //OrderTransaction objOrderTransaction = new OrderTransaction();
                //objOrderTransaction.ItemId = objOrderDetails.ItemId;
                //objOrderTransaction.OrderType = true;
                //objOrderTransaction.CustomerId = objpPlaceOrderViewModel.CustomerId;
                //objOrderTransaction.PaymentTypeId = objpPlaceOrderViewModel.PaymentTypeId;
                //objOrderTransaction.Quantity = objOrderDetails.Total;
                //db.OrderTransactions.Add(objOrderTransaction);
                //db.SaveChanges();

            }

            return Json("", JsonRequestBehavior.AllowGet);
        }
    }

}