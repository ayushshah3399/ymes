namespace RestaurantApp.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Transaction.OrderTransaction")]
    public partial class OrderTransaction
    {
        [Key]
        public int TransactionId { get; set; }

        public int ItemId { get; set; }

        public int CustomerId { get; set; }

        public int PaymentTypeId { get; set; }

        public bool OrderType { get; set; }

        public decimal Quantity { get; set; }

        public virtual Customer Customer { get; set; }

        public virtual Item Item { get; set; }

        public virtual PaymentType PaymentType { get; set; }
    }
}
