namespace KYOSAI_WEB.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Web.Mvc;

    [Table("TELAS.D3010")]
    public partial class D3010
    {
        [Required]
        [StringLength(1)]
        public string ORDERKBN { get; set; }

        [Key]
        [StringLength(10)]
        public string ORDERNO { get; set; }

       
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}")]
        public DateTime? ORDERDT { get; set; }

        [Required]
        [StringLength(6)]
        public string SUPPCD { get; set; }

        [Required]
        [StringLength(6)]
        public string DELICD { get; set; }

        [StringLength(20)]
        public string HMNO { get; set; }

        public decimal ORDERQTY { get; set; }

        [Required]
        [StringLength(2)]
        public string ORDERUNIT { get; set; }

        [Required]
        [StringLength(1)]
        public string PRICEKBN { get; set; }

        public decimal PRICE { get; set; }

        [Required]
        [StringLength(2)]
        public string PRICEUNIT { get; set; }

        public decimal AMOUNT { get; set; }

        [Required]
        [StringLength(1)]
        public string TAXKBN { get; set; }

        public DateTime AGREED { get; set; }

        public DateTime? REPLY { get; set; }

        [Required]
        [StringLength(1)]
        public string STATE { get; set; }

        [StringLength(40)]
        public string REMARK { get; set; }

        [Required]
        [StringLength(1)]
        public string PRTKBN1 { get; set; }

        [Required]
        [StringLength(1)]
        public string PRTKBN2 { get; set; }

        [Required]
        [StringLength(1)]
        public string DECIKBN { get; set; }

        [Required]
        [StringLength(1)]
        public string REPLANKBN { get; set; }

        [StringLength(10)]
        public string SORDERNO { get; set; }

        [Required]
        [StringLength(1)]
        public string EXCEKBN { get; set; }

        public decimal? RATE { get; set; }

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

        [Required]
        [StringLength(3)]
        public string KYOTENCD { get; set; }

        [Required]
        [StringLength(4)]
        public string JITANCD { get; set; }

        [StringLength(10)]
        public string JORDERNO { get; set; }

        [StringLength(40)]
        public string SZCMT { get; set; }

        [AllowHtml]
        [StringLength(40)]
        public string BCCMT { get; set; }

        [StringLength(10)]
        public string SHEETNO { get; set; }

        public byte? ROWNO { get; set; }

        public DateTime? PRTDT1 { get; set; }

        public DateTime? REPLYDLDT { get; set; }

        public DateTime? REQDELIDT { get; set; }

        [Required]
        [StringLength(1)]
        public string ADJREQKBN { get; set; }

        [StringLength(15)]
        public string MORDERNO { get; set; }

        [Required]
        [StringLength(1)]
        public string GAITNKBN { get; set; }

        [StringLength(5)]
        public string PROCECD { get; set; }

        public byte? STRSEQ { get; set; }

        public byte? PRICELOT { get; set; }
        public virtual M0010 M0010 { get; set; }

        //public virtual M1010 M1010 { get; set; }

        public virtual M1050 M1050 { get; set; }

        //public virtual M5010 M5010 { get; set; }

        //public virtual MC0030 MC0030 { get; set; }

        [NotMapped]
        public decimal DELIQTY { get; set; }

        [NotMapped]
        public decimal BKLOGQTY { get; set; }

        [NotMapped]
        [StringLength(1)]
        public string PAYMENTKBN { get; set; }

        [AllowHtml]
        [NotMapped]
        public DateTime? ANSDT { get; set; }

        [AllowHtml]
        [NotMapped]
        public decimal REPLYCNT { get; set; }

        [NotMapped]
        [StringLength(4)]
        public string UNITNM { get; set; }

        [NotMapped]
        [StringLength(50)]
        public string CHARGENM { get; set; }

        [NotMapped]
        [StringLength(40)]
        public string SUPPNM { get; set; }
    }
}
