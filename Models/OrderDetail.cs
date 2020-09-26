namespace RestaurantApp.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Transaction.OrderDetails")]
    public partial class OrderDetail
    {
        [Key]
        public int OrderDetailsId { get; set; }

        public decimal OrderNo { get; set; }

        public int ItemId { get; set; }

        public decimal Quantity { get; set; }

        public decimal Total { get; set; }

        public virtual Item Item { get; set; }
    }
}
