using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RestaurantApp.Models;

namespace RestaurantApp.ViewModel
{
    public class PlaceOrderViewModel
    {
        public decimal OrderNo { get; set; }

        public int CustomerId { get; set; }

        public int PaymentTypeId { get; set; }

        public decimal Total { get; set; }

        public decimal Discount { get; set; }

        public IEnumerable<OrderDetail> OrderDetail { get; set; }

    }
}