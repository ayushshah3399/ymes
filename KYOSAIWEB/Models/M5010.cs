namespace KYOSAI_WEB.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TELAS.M5010")]
    public partial class M5010
    {
        [Key]
        [StringLength(4)]
        public string CHARGECD { get; set; }

        [Required]
        [StringLength(50)]
        public string CHARGENM { get; set; }

        [StringLength(20)]
        public string MEMO { get; set; }

        [Required]
        [StringLength(1)]
        public string RETIREKBN { get; set; }

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
        [StringLength(2)]
        public string SECTIONCD { get; set; }

        [StringLength(100)]
        public string MAILADRS { get; set; }
    }
}
