using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RestaurantApp.Models;

namespace RestaurantApp.ViewModel
{
    public class MultipleDataViewModel
    {
        public IEnumerable<Item> items { get; set; }

        public IEnumerable<Customer> customers { get; set; }

        public IEnumerable<PaymentType> paymentTypes { get; set; }
    }
}