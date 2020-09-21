namespace KYOSAI_WEB.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TELAS.D4020")]
    public partial class D4020
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(1)]
        public string DATAKBN { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(10)]
        public string ORDERNO { get; set; }

        [Key]
        [Column(Order = 2)]
        public byte ACTUALCNT { get; set; }

        public DateTime INSPECDT { get; set; }

        public DateTime ORDERDT { get; set; }

        [Required]
        [StringLength(6)]
        public string SUPPCD { get; set; }

        [StringLength(20)]
        public string HMNO { get; set; }

        public decimal ACTUALQTY { get; set; }

        [Required]
        [StringLength(2)]
        public string ACTUALUNIT { get; set; }

        public decimal PRICE { get; set; }

        [Required]
        [StringLength(2)]
        public string PRICEUNIT { get; set; }

        public decimal AMOUNT { get; set; }

        [Required]
        [StringLength(1)]
        public string PRICEKBN { get; set; }

        public decimal TAXAMOUNT { get; set; }

        [Required]
        [StringLength(1)]
        public string TAXKBN { get; set; }

        [Required]
        [StringLength(1)]
        public string ADDUPKBN { get; set; }

        public DateTime? ADDUPDT { get; set; }

        public DateTime? ADDUPEXDT { get; set; }

        [StringLength(65)]
        public string APPLY { get; set; }

        [StringLength(10)]
        public string CHECKNO { get; set; }

        [StringLength(3)]
        public string DEACCCD { get; set; }

        [StringLength(3)]
        public string DEAUACCCD { get; set; }

        [StringLength(3)]
        public string CRACCCD { get; set; }

        [StringLength(3)]
        public string CRAUACCCD { get; set; }

        [StringLength(2)]
        public string DESECTIONCD { get; set; }

        [StringLength(2)]
        public string CRSECTIONCD { get; set; }

        [StringLength(21)]
        public string LOTNO { get; set; }

        [StringLength(15)]
        public string MAKERLOTNO { get; set; }

        [StringLength(3)]
        public string STOCKCD { get; set; }

        public decimal? STOCKQTY { get; set; }

        [StringLength(2)]
        public string STOCKUNIT { get; set; }

        [StringLength(1)]
        public string PRTKBN1 { get; set; }

        [StringLength(40)]
        public string REMARK { get; set; }

        [StringLength(6)]
        public string REPSUPPCD { get; set; }

        [StringLength(5)]
        public string BADREACD { get; set; }

        [StringLength(10)]
        public string UORDERNO { get; set; }

        [StringLength(3)]
        public string TAXTYPE { get; set; }

        public decimal? RATE { get; set; }

        [StringLength(10)]
        public string REMARK2 { get; set; }

        [Required]
        [StringLength(64)]
        public string INSTID { get; set; }

        public DateTime INSTDT { get; set; }

        [Required]
        [StringLength(50)]
        public string INSTTERM { get; set; }

        [Required]
        [StringLength(50)]
        public string INSTPRGNM { get; set; }

        [Required]
        [StringLength(64)]
        public string UPDTID { get; set; }

        public DateTime UPDTDT { get; set; }

        [Required]
        [StringLength(50)]
        public string UPDTTERM { get; set; }

        [Required]
        [StringLength(50)]
        public string UPDTPRGNM { get; set; }

        [StringLength(4)]
        public string RSHCHARGE { get; set; }

        public decimal? RSHQTY { get; set; }

        public DateTime? ANSDLDT { get; set; }
    }
}
