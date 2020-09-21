namespace KYOSAI_WEB.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TELAS.DC0060")]
    public partial class DC0060
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(10)]
        public string ORDERNO { get; set; }

        [Key]
        [Column(Order = 1)]
        public byte DELISCHECNT { get; set; }

        public DateTime? DELISCHEDT { get; set; }

        public decimal? DELISCHEQTY { get; set; }

        public DateTime? REPLYDT { get; set; }

        public DateTime? RECEIVEDT { get; set; }

        [Required]
        [StringLength(1)]
        public string RECEIVEKBN { get; set; }

       [StringLength(4)]
        public string UKECHARGECD { get; set; }

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

        [NotMapped]
        public long UPDTLNG { get; set; }
      
    }
}
