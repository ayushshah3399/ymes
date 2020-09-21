namespace KYOSAI_WEB.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TELAS.MC0060")]
    public partial class MC0060
    {
        [Key]
        [StringLength(4)]
        public string JITANCD { get; set; }

        [Required]
        [StringLength(20)]
        public string JITANNM { get; set; }

        [Required]
        [StringLength(4)]
        public string CHARGECD { get; set; }

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
    }
}
