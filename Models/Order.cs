namespace RestaurantApp.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Transaction.Orders")]
    public partial class Order
    {
        public int OrderId { get; set; }

        public decimal OrderNo { get; set; }

        public int CustomerId { get; set; }

        public int PaymentTypeId { get; set; }

        public decimal Total { get; set; }

        public decimal Discount { get; set; }

        public virtual Customer Customer { get; set; }

        public virtual PaymentType PaymentType { get; set; }
    }
}
